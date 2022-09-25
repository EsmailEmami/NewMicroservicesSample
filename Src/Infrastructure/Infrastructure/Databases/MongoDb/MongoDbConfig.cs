using Application.Core.MongoOptions;
using Application.Core.Repositories;
using Application.Core.Repositories.Mongo;
using Infrastructure.Repositories.Mongo;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastructure.Databases.MongoDb;

public static class MongoDbConfig
{
    public static IServiceCollection AddMongoDb(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MongoDbSettings>(configuration.GetSection("MongoDbSettings"));

        services.AddSingleton<IMongoDbSettings>(serviceProvider =>
            serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value);

        services.AddScoped(typeof(IMongoRepository<,>), typeof(MongoMainRepository<,>));
        services.AddScoped(typeof(IMongoRepository<>), typeof(MongoMainRepository<>));

        return services;
    }
}