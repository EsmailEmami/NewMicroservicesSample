using Application.MessageBrokers;
using Application.MessageBrokers.Kafka;
using Domain.Core.Events;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.MessageBrokers.Kafka;

public static class KafkaExtensions
{
    public static IServiceCollection AddKafka(this IServiceCollection services, IConfiguration configuration)
    {
        var options = new KafkaOptions();
        configuration.GetSection(nameof(MessageBrokersOptions)).Bind(options);
        services.Configure<KafkaOptions>(c=> c = options);

        services.AddSingleton<IEventListener, KafkaListener>();

        return services;
    }

    public static IApplicationBuilder UseKafkaSubscribe<T>(this IApplicationBuilder app) where T : IEvent
    {
        app.ApplicationServices.GetRequiredService<IEventListener>().Subscribe<T>();

        return app;
    }
}