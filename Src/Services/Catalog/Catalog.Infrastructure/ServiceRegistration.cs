using Application.Extensions;
using BuildingBlocks.CatalogService.Product;
using Catalog.Application.Services;
using Catalog.Infrastructure.Context;
using Catalog.Infrastructure.Services;
using Infrastructure.Authorization;
using Infrastructure.Consul;
using Infrastructure.Core;
using Infrastructure.Databases.EntityFrameworkCore;
using Infrastructure.Databases.MongoDb;
using Infrastructure.Databases.Redis;
using Infrastructure.Events;
using Infrastructure.MessageBrokers;
using Infrastructure.Outbox;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Infrastructure;

public static class ServiceRegistration
{
    public static IServiceCollection AddServiceRegistration(this IServiceCollection services, IConfiguration configuration, params Type[] types)
    {
        types = typeof(ProductAddedEvent).PrependToParamArray(types);

        services.AddCustomizedAuthorization(configuration);

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

    public static IApplicationBuilder UseCoreReRegistration(this IApplicationBuilder app,IConfiguration configuration)
    {
        app.UseCustomizedAuthentication(configuration);
        app.UseSubscribeAllEvents(typeof(ProductAddedEvent));

        return app;
    }
}
