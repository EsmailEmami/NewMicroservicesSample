using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ApiVersioningOptions = Application.ApiVersioning.ApiVersioningOptions;

namespace Infrastructure.ApiVersioning;

public static class ApiVersioningExtensions
{
    public static IServiceCollection AddApiVersioning(this IServiceCollection services, IConfiguration configuration)
    {
        var options = new ApiVersioningOptions();
        configuration.GetSection(nameof(ApiVersioningOptions)).Bind(options);
        services.Configure<ApiVersioningOptions>(configuration.GetSection(nameof(ApiVersioningOptions)));

        services.AddApiVersioning(o =>
        {
            o.AssumeDefaultVersionWhenUnspecified = options.AssumeDefaultVersionWhenUnspecified;
            o.DefaultApiVersion = new ApiVersion(options.DefaultVersion.MajorVersion, options.DefaultVersion.MinorVersion);
            o.ReportApiVersions = options.ReportApiVersions;

            o.ApiVersionReader = ApiVersionReader.Combine(new HeaderApiVersionReader(options.VersionHeaderReader));
        });

        return services;
    }
}