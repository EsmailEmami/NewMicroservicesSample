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
using Infrastructure.Logging;
using Infrastructure.MessageBrokers;
using Infrastructure.Outbox;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Catalog.Infrastructure;

public static class ServiceRegistration
{
    public static IServiceCollection AddServiceRegistration(this IServiceCollection services, IConfiguration configuration, params Type[] types)
    {
        Log.Logger = LoggingExtensions.AddLogging(configuration);

        types = typeof(ProductAddedEvent).PrependToParamArray(types);
        services.AddConsul(configuration);
        services.AddMessageBroker(configuration);
        services.AddOutbox(configuration);
        services.AddCore(types);

        services.AddMongoDb(configuration);
        services.AddDbContext<CatalogDbContext>(configuration);
        services.AddRedis(configuration);

        services.AddJwtIdentity(configuration);

        services.AddScoped<IProductService, ProductService>();

        return services;
    }

    public static IApplicationBuilder UseCoreRegistration(this IApplicationBuilder app, IConfiguration configuration, IHostApplicationLifetime lifetime)
    {
        var loggerFactory = app.ApplicationServices.GetService<ILoggerFactory>();
        if (loggerFactory == null)
            throw new ArgumentNullException(nameof(loggerFactory));

        app.UseSubscribeAllEvents(typeof(ProductAddedEvent));
        app.UseLogging(configuration, loggerFactory);
        app.UseConsul(lifetime);
        app.UseIdentity();

        return app;
    }
}
