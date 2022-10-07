using Application.Extensions;
using BuildingBlocks.CatalogService.Product;
using Infrastructure.ApiVersioning;
using Infrastructure.Consul;
using Infrastructure.Core;
using Infrastructure.Databases.EntityFrameworkCore;
using Infrastructure.Events;
using Infrastructure.MessageBrokers;
using Infrastructure.Outbox;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using User.Application.Core.User;
using User.Application.Services.User;
using User.Application.Services.User.MappingProfiles;
using User.Infrastructure.Context;

namespace User.Infrastructure;

public static class ServiceRegistration
{
    public static IServiceCollection AddServiceRegistration(this IServiceCollection services, IConfiguration configuration, params Type[] types)
    {
        types = typeof(ProductAddedEvent).PrependToParamArray(types);

        services.AddConsul(configuration);
        services.AddMessageBroker(configuration);
        services.AddOutbox(configuration);
        services.AddCore(types);

        services.AddAutoMapper(typeof(CreateUserDtoMap).Assembly);

        services.AddDbContext<UserDbContext>(configuration);

        services.AddApiVersioning(configuration);

        services.AddScoped<IUserService, UserService>();

        return services;
    }

    public static IApplicationBuilder UseCoreRegistration(this IApplicationBuilder app)
    {
        app.UseSubscribeAllEvents(typeof(ProductAddedEvent));

        return app;
    }
}
