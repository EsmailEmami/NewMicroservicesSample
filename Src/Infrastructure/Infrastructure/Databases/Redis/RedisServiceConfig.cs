using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Infrastructure.Databases.Redis;

public static class RedisServiceConfig
{
    public static IServiceCollection AddRedis(this IServiceCollection services, IConfiguration configuration)
    {
        ConfigurationOptions configurationOptions = new ConfigurationOptions();
        configuration.GetSection("CacheSettings:Options").Bind(configurationOptions);
        services.Configure<ConfigurationOptions>(configuration.GetSection("CacheSettings:Options"));

        configuration.GetSection("CacheSettings:Options").Bind(configurationOptions);
        services.Configure<ConfigurationOptions>(configuration.GetSection("CacheSettings:Options"));

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetValue<string>("CacheSettings:ConnectionString");
            options.ConfigurationOptions = configurationOptions;
        });
        return services;
    }
}
