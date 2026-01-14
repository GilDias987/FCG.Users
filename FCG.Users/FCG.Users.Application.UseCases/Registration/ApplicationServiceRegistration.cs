using FCG.Users.Application.UseCases.Behaviour;
using FCG.Users.Application.UseCases.Feature.User.Commands.AddUser;
using FluentValidation;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace FCG.Users.Application.UseCases.Registration
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddMassTransit(x =>
            {
                x.SetKebabCaseEndpointNameFormatter();
                x.AddConsumer<TesteConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(new Uri("amqp://localhost"), h =>
                    {
                        h.Username("admin");
                        h.Password("admin123");
                    });

                    cfg.ReceiveEndpoint("order-created-queue", e => // Nome da fila
                    {
                        e.ConfigureConsumer<TesteConsumer>(context);
                    });

                    // Configura automaticamente as filas com base nos consumidores registrados
                    cfg.ConfigureEndpoints(context);
                });
            });

            return services;
        }
    }
}
