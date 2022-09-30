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

builder.Services.AddCustomizedAuthorization(builder.Configuration);
builder.Services.AddOcelot()
    .AddCacheManager(x => x.WithDictionaryHandle());

builder.Services.AddSwaggerForOcelot(builder.Configuration);
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});


var app = builder.Build();

app.UseRouting();
app.UseSwagger();

app.UseCustomizedAuthentication(builder.Configuration);

app.UseSwaggerForOcelotUI();

app.UseOcelot().Wait();
app.Run();
