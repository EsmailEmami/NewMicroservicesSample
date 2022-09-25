using Application.Core;
using Application.Core.Repositories.EfCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Databases.EntityFrameworkCore;

public static class EntityFrameworkCoreConfig
{
    public static IServiceCollection AddDbContext<TContext>(this IServiceCollection services, IConfiguration configuration, Type implementationRepositoryByPrimaryKey, Type implementationRepositoryWithoutPrimaryKey) where TContext : DbContext
    {
        services.AddDbContext<TContext>(options =>
           options.UseSqlServer(configuration.GetConnectionString("SqlConnectionString")));

        services.AddScoped(typeof(IUnitOfWork), typeof(TContext));

        services.AddScoped(typeof(IEfCoreRepository<,>), implementationRepositoryByPrimaryKey);
        services.AddScoped(typeof(IEfCoreRepository<>), implementationRepositoryWithoutPrimaryKey);

        return services;
    }

    public static IServiceCollection AddDbContextSeed<T>(this IServiceCollection services, IConfiguration configuration) where T : DbContext
    {
        services.AddDbContext<T>(options =>
           options.UseSqlServer(configuration.GetConnectionString("SqlConnectionString")));

        return services;
    }
}