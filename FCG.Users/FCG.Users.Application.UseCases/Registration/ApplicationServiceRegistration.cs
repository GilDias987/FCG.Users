using FCG.Users.Application.Interface.Service;
using FCG.Users.Application.UseCases.Behaviour;
using FCG.Users.Application.UseCases.Service;
using FluentValidation;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System.Reflection;

namespace FCG.Users.Application.UseCases.Registration
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddScoped<IEmailService, EmailService>();
            services.AddHttpContextAccessor();
            services.AddScoped<IUserService, UserService>();
            services.AddSingleton<ICacheService, CacheService>();
            services.AddMassTransit(x =>
            {
                x.UsingAzureServiceBus((context, cfg) =>
                {
                    cfg.Host(configuration["ServiceBus:ConnectionString"]);
                });
            });

            services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                var configurationRedis = configuration["Redis:ConnectionString"];

                var options = ConfigurationOptions.Parse(configurationRedis);

                options.ConnectTimeout = 30000;        // ⬅️ aumente para 30s
                options.SyncTimeout = 30000;
                options.AbortOnConnectFail = false;

                options.Ssl = true;                    // 🔴 CRÍTICO para porta 6380
                options.ReconnectRetryPolicy = new ExponentialRetry(5000);

                return ConnectionMultiplexer.Connect(options);
            });

            return services;
        }
    }
}
