using System.Reflection;
using Application.Events;
using Application.Filters;
using Application.Security;
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
        var assemblies = types.Select(type => type.GetTypeInfo().Assembly).ToArray();

        foreach (var assembly in assemblies)
            services.AddMediatR(assembly);

        services.AddControllers(opt => opt.Filters.Add<ExceptionFilter>());

        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        services.AddScoped<ICommandBus, CommandBus>();
        services.AddScoped<IQueryBus, QueryBus>();
        services.AddScoped<IEventBus, EventBus>();

        services.AddOptions();
        services.AddHealthChecks();

        return services;
    }

    public static IApplicationBuilder UseCore(this IApplicationBuilder app)
    {
        return app;
    }
}