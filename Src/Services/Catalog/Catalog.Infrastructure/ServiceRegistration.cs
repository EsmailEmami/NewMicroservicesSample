using Catalog.Application.Services;
using Catalog.Infrastructure.Context;
using Catalog.Infrastructure.Services;
using Infrastructure.Consul;
using Infrastructure.Core;
using Infrastructure.Databases.EntityFrameworkCore;
using Infrastructure.Databases.MongoDb;
using Infrastructure.Databases.Redis;
using Infrastructure.MessageBrokers;
using Infrastructure.Outbox;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Infrastructure;

public static class ServiceRegistration
{
    public static IServiceCollection AddServiceRegistration(this IServiceCollection services, IConfiguration configuration, params Type[] types)
    {
        services.AddConsul(configuration);
        services.AddMessageBroker(configuration);
        services.AddOutbox(configuration);
        services.AddCore(types);

        services.AddMongoDb(configuration);
        services.AddDbContext<CatalogDbContext>(configuration);
        services.AddRedis(configuration);

        services.AddScoped<IProductService, ProductService>();

        return services;
    }
}
