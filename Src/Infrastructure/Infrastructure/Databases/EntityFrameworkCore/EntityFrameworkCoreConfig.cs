using System.CodeDom.Compiler;
using Application.Core.Repositories.EfCore;
using Microsoft.CSharp;
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

        using CSharpCodeProvider provider = new CSharpCodeProvider();
        var compParms = new CompilerParameters
        {
            GenerateExecutable = false,
            GenerateInMemory = true
        };
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