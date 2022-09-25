using System.Reflection;
using Application.Behaviors;
using Application.Events;
using Application.Filters;
using Domain.Commands;
using Domain.Queries;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Core;

public static class CoreExtensions
{
    public static IServiceCollection AddCore(this IServiceCollection services, params Type[] types)
    {
        var assemblies = types.Select(type => type.GetTypeInfo().Assembly);

        foreach (var assembly in assemblies)
            services.AddMediatR(assembly);

        services.AddScoped<ICommandBus, CommandBus>();
        services.AddScoped<IQueryBus, QueryBus>();
        services.AddScoped<IEventBus, EventBus>();
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddOptions();
        return services;
    }

    public static IApplicationBuilder UseCore(this IApplicationBuilder app)
    {
        return app;
    }
}