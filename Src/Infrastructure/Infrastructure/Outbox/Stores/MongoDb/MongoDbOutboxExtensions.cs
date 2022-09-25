using Application.Outbox;
using Application.Outbox.Stores;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Outbox.Stores.MongoDb;

public static class MongoDbOutboxExtensions
{
    public static IServiceCollection AddMongoDbOutbox(this IServiceCollection services, IConfiguration configuration)
    {
        var options = new MongoDbOutboxOptions();
        configuration.GetSection(nameof(OutboxOptions)).Bind(options);
        services.Configure<MongoDbOutboxOptions>(configuration.GetSection(nameof(OutboxOptions)));

        services.AddScoped<IOutboxStore, MongoDbOutboxStore>();

        return services;
    }
}