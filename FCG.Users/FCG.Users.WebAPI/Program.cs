using FCG.Users.Application.Interface.Repository;
using FCG.Users.Application.UseCases.Feature.User.Commands.AddUserSeed;
using FCG.Users.Application.UseCases.Interceptor;
using FCG.Users.Application.UseCases.Registration;
using FCG.Users.Application.UseCases.Service;
using FCG.Users.Infrastructure.Context;
using FCG.Users.Infrastructure.Repository;
using FCG.Users.WebAPI.Middleware;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using NSwag;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configura o ILogger para ler o appsettings.json
//builder.AddLogging();

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddOpenApiDocument(options =>
{
    options.Title = "Api Users - Fiap Cloud Game";
    options.Version = "1.0";
    options.AddSecurity("Bearer", new NSwag.OpenApiSecurityScheme
    {
        Description = "Bearer token authorization header",
        Type = NSwag.OpenApiSecuritySchemeType.Http,
        In = NSwag.OpenApiSecurityApiKeyLocation.Header,
        Name = "Authorization",
        Scheme = "Bearer"
    });

    options.OperationProcessors.Add(
        new NSwag.Generation.Processors.Security.AspNetCoreOperationSecurityScopeProcessor("Bearer"));
});

var sqlConn = builder.Configuration.GetConnectionString("ConnectionStrings");
builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var connectionString = builder.Configuration["Mongodbsql:ConnectionString"];
    return new MongoClient(connectionString);
});

builder.Services.AddSingleton<MongoAuditService>();
builder.Services.AddScoped<AuditInterceptor>();

builder.Services.AddDbContext<ApplicationDbContext>((sp, options) =>
{
    options.UseSqlServer(sqlConn);
    options.AddInterceptors(sp.GetRequiredService<AuditInterceptor>());
});

#region [JWT]
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});
#endregion

#region Exception Global
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
#endregion

#region Repository
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserGroupRepository, UserGroupRepository>();
#endregion

builder.Services.AddProblemDetails();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ADMINISTRADOR", policy => policy.RequireRole("ADMINISTRADOR"));
});

var app = builder.Build();

var forwardedOptions = new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor
                     | ForwardedHeaders.XForwardedProto
                     | ForwardedHeaders.XForwardedHost
};
forwardedOptions.KnownNetworks.Clear();
forwardedOptions.KnownProxies.Clear();
app.UseForwardedHeaders(forwardedOptions);

var pathBase = Environment.GetEnvironmentVariable("PATH_BASE");
if (!string.IsNullOrWhiteSpace(pathBase))
{
    if (!pathBase.StartsWith("/")) pathBase = "/" + pathBase;
    pathBase = pathBase.TrimEnd('/');
    app.UsePathBase(pathBase);
}

app.UseOpenApi(settings =>
{
    settings.PostProcess = (document, request) =>
    {
        document.Servers.Clear();
        document.Servers.Add(new OpenApiServer
        {
            Url = $"{request.Scheme}://{request.Host.Value}{request.PathBase}"
        });
    };

    settings.CreateDocumentCacheKey = request =>
        request.Headers["X-Forwarded-Host"].FirstOrDefault()
        + request.PathBase
        + request.IsHttps;
});

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("/health");

app.UseExceptionHandler();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();

    var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
    await mediator.Send(new AddUserSeedCommand());
}

app.Run();
