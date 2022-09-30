using Application.Extensions;
using BuildingBlocks.CatalogService.Product;
using Infrastructure.Consul;
using Infrastructure.Core;
using Infrastructure.Databases.Redis;
using Infrastructure.Events;
using Infrastructure.MessageBrokers;
using Infrastructure.Outbox;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Basket.Infrastructure;

public static class ServiceRegistration
{
    public static IServiceCollection AddServiceRegistration(this IServiceCollection services, IConfiguration configuration, params Type[] types)
    {
        types = typeof(ProductAddedEvent).PrependToParamArray(types);
        services.AddConsul(configuration);
        services.AddMessageBroker(configuration);
        services.AddOutbox(configuration);
        services.AddCore(types);

        services.AddRedis(configuration);

        return services;
    }
    public static IApplicationBuilder UseCoreRegistration(this IApplicationBuilder app)
    {
        app.UseSubscribeAllEvents(typeof(ProductAddedEvent));

        return app;
    }
}
