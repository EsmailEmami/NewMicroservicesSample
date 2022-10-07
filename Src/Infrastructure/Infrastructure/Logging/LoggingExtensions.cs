using Elastic.Apm.NetCoreAll;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Exceptions;
using Serilog.Core;

namespace Infrastructure.Logging;

public static class LoggingExtensions
{
    public static Logger AddLogging(IConfiguration Configuration)
    {
        var logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .Enrich.WithExceptionDetails()
            .Enrich.WithCorrelationIdHeader()
            .Filter.ByExcluding(c => c.Properties.Any(p => p.Value.ToString().Contains("swagger")))
            .ReadFrom.Configuration(Configuration)
            .CreateLogger();

        return logger;
    }

    public static IApplicationBuilder UseLogging(this IApplicationBuilder app, IConfiguration configuration, ILoggerFactory loggerFactory)
    {
        app.UseAllElasticApm(configuration);

        loggerFactory.AddSerilog();

        return app;
    }
}