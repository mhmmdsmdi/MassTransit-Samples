using System.Reflection;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace Shared;

public static class CustomMassTransit
{
    public static void AddCustomMass(this IServiceCollection services)
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumers(Assembly.GetEntryAssembly());

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("localhost", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                cfg.ConfigureEndpoints(context);
            });
        });
    }
}