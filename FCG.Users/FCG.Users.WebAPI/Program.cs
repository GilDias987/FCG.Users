using FCG.Users.Application.Interface.Repository;
using FCG.Users.Application.UseCases.Feature.User.Commands.AddUserSeed;
using FCG.Users.Application.UseCases.Registration;
using FCG.Users.Infrastructure.Context;
using FCG.Users.Infrastructure.Repository;
using FCG.Users.WebAPI.Middleware;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using FCG.Users.WebAPI.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Configura o Serilog para ler o appsettings.json
builder.AddSerilogLogging();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();

Console.WriteLine("==== CONFIG DEBUG START ====");

var sqlConn = builder.Configuration.GetConnectionString("ConnectionStrings");
Console.WriteLine($"SQL ConnectionString NULL? {string.IsNullOrEmpty(sqlConn)}");

var sql = builder.Configuration.GetConnectionString("ConnectionStrings");
Console.WriteLine("SQL RAW:");
Console.WriteLine($"[{sql}]");
Console.WriteLine($"Length: {sql?.Length}");

var serviceBusConn = builder.Configuration["AzureServiceBus:ConnectionString"];
Console.WriteLine($"ServiceBus ConnectionString NULL? {string.IsNullOrEmpty(serviceBusConn)}");

Console.WriteLine("SQL ConnectionString Length: " + (sqlConn?.Length ?? 0));
Console.WriteLine("ServiceBus ConnectionString Length: " + (serviceBusConn?.Length ?? 0));

Console.WriteLine("==== CONFIG DEBUG END ====");

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

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionStrings"));
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

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddProblemDetails();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ADMINISTRADOR", policy => policy.RequireRole("ADMINISTRADOR"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseExceptionHandler();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    Console.WriteLine("START MIGRATION");
    db.Database.Migrate();

    Console.WriteLine("MIGRATION OK");

    Console.WriteLine("START SEED");

    var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
    await mediator.Send(new AddUserSeedCommand());

    Console.WriteLine("SEED OK");
}

app.Run();
