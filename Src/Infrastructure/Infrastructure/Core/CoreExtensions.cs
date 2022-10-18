using System.Reflection;
using Application.Events;
using Application.Filters;
using Application.Middlewares;
using Application.PipeLines;
using Application.Security;
using Domain.Commands;
using Domain.Queries;
using FluentValidation;
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

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehavior<,>));
        services.AddValidatorsFromAssemblies(assemblies);

        //opt => opt.Filters.Add<ExceptionFilter>()
        services.AddControllers();

        services.AddTransient<ExceptionHandlingMiddleware>();

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
        app.UseMiddleware<ExceptionHandlingMiddleware>();
        return app;
    }
}