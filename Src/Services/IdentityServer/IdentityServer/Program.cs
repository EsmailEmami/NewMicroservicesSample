using IdentityServer.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddICustomizeddentityServer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

app.UseStaticFiles();
app.UseRouting();
app.UseCustomizedIdentityServer();

app.Use(async (context, next) =>
{
    if (context.Request.Path == "/.well-known/openid-configuration")
    {
        context.Request.Path = "/.well-known/openid-configuration.json";
    }

    await next();
});
;
app.MapGet("/", () => "hello world!");

app.Run();
