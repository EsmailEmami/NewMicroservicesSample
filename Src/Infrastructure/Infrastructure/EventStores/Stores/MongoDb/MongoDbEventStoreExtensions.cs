using Application.EventStores;
using Application.EventStores.Stores;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.EventStores.Stores.MongoDb;

public static class MongoDbEventStoreExtensions
{
    public static IServiceCollection AddMongoDbEventStore(this IServiceCollection services, IConfiguration configuration)
    {
        var options = new MongoDbEventStoreOptions();
        configuration.GetSection(nameof(EventStoresOptions)).Bind(options);
        services.Configure<MongoDbEventStoreOptions>(configuration.GetSection(nameof(EventStoresOptions)));

        services.AddScoped<IStore, MongoDbEventStore>();

        return services;
    }
}