using Application.MessageBrokers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit.DependencyInjection.ServiceCollection;
using RawRabbit.Instantiation;

namespace Infrastructure.MessageBrokers.RabbitMq;

public static class RabbitMqExtensions
{
    public static IServiceCollection AddRabbitMq(this IServiceCollection services, IConfiguration configuration, params Type[] types)
    {
        var options = new RabbitMqOptions();
        configuration.GetSection(nameof(MessageBrokersOptions)).Bind(options);
        services.Configure<RabbitMqOptions>(configuration.GetSection(nameof(MessageBrokersOptions)));

        services.AddRawRabbit(new RawRabbitOptions
        {
            ClientConfiguration = options
        });

        services.AddSingleton<IEventListener, RabbitMqListener>();

        return services;
    }
}