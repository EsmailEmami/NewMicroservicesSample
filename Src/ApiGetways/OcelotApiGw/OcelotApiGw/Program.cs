using Infrastructure.Authorization;
using Microsoft.OpenApi.Models;
using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    config.AddJsonFile($"ocelot.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
        .AddEnvironmentVariables();
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddOcelot()
    .AddCacheManager(x => x.WithDictionaryHandle());

builder.Services.AddJwtIdentity(builder.Configuration);

builder.Services.AddSwaggerForOcelot(builder.Configuration);

var app = builder.Build();

app.UseRouting();

app.UseIdentity();
app.UseSwaggerForOcelotUI();

app.UseOcelot().Wait();
app.Run();
