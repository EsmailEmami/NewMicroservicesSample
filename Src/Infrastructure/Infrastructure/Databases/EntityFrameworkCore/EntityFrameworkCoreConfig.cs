using System.Reflection;
using Application.Core;
using Application.Core.Repositories.EfCore;
using Domain.Core.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Databases.EntityFrameworkCore;

public static class EntityFrameworkCoreConfig
{
    public static IServiceCollection AddDbContext<TContext>(this IServiceCollection services, IConfiguration configuration, Type implementationRepositoryByPrimaryKey, Type implementationRepositoryWithoutPrimaryKey) where TContext : DbContext
    {
        services.AddDbContext<TContext>(options =>
           options.UseSqlServer(configuration.GetConnectionString("SqlConnectionString"), options =>
           {
               options.EnableRetryOnFailure(5);
           }));

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

    public static void MigrateDatabase<TContext>(this IHost host,
        Action<TContext, IServiceProvider> seeder,
        int? retry = 5) where TContext : DbContext
    {
        if (retry != null)
        {
            int retryForAvailability = retry.Value;

            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            var logger = services.GetRequiredService<ILogger<TContext>>();
            var context = services.GetService<TContext>();

            try
            {
                logger.LogInformation("migrating started for sql server");
                InvokeSeeder(seeder, context, services);
                logger.LogInformation("migrating has been done for sql server");
            }
            catch (SqlException ex)
            {
                logger.LogError(ex, "an error occurred while migrating database");

                if (retryForAvailability < 50)
                {
                    retryForAvailability++;
                    Thread.Sleep(2000);
                    MigrateDatabase(host, seeder, retryForAvailability);
                }
                throw;
            }
        }
    }

    private static void InvokeSeeder<TContext>(
        Action<TContext, IServiceProvider> seeder,
        TContext context,
        IServiceProvider services)
        where TContext : DbContext?
    {
        context?.Database.Migrate();
        seeder(context, services);
    }

    public static void RegisterAllEntities(this ModelBuilder modelBuilder, params Assembly[] assemblies)
    {
        var types = assemblies.SelectMany(a => a.GetExportedTypes())
            .Where(c => c.IsClass && !c.IsAbstract && c.IsPublic && (typeof(Entity).IsAssignableFrom(c) || typeof(Entity<>).IsAssignableFrom(c)));

        foreach (var type in types)
            modelBuilder.Entity(type);
    }
}