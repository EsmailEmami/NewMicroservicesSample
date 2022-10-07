namespace IdentityServer.Extensions;

public static class IdentityServerExtensions
{
    public static IServiceCollection AddICustomizeddentityServer(this IServiceCollection services)
    {
        services.AddIdentityServer()
            .AddInMemoryClients(Config.Clients)
            .AddInMemoryIdentityResources(Config.IdentityResources)
            .AddInMemoryApiResources(Config.ApiResources)
            .AddInMemoryApiScopes(Config.ApiScopes)
            .AddTestUsers(Config.TestUsers)
            .AddDeveloperSigningCredential();

        return services;
    }

    public static IApplicationBuilder UseCustomizedIdentityServer(this IApplicationBuilder app)
    {
        app.UseIdentityServer();
        return app;
    }
}