using Domain.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using IdentityOptions = Application.Authorization.IdentityOptions;

namespace Infrastructure.Authorization;

public static class AuthorizationExtentions
{
    public static IServiceCollection AddJwtIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();

        services.AddScoped<IUser, AspNetUser>();
        var options = new IdentityOptions();
        configuration.GetSection(nameof(IdentityOptions)).Bind(options);

        services.Configure<IdentityOptions>(configuration.GetSection(nameof(IdentityOptions)));

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = options.TokenParameters.ValidateIssuer,
            ValidIssuers = options.TokenParameters.ValidIssuers,
            ValidateAudience = options.TokenParameters.ValidateAudience,
            ValidAudiences = options.TokenParameters.ValidAudiences,
            RequireExpirationTime = options.TokenParameters.RequireExpirationTime,
            ValidateLifetime = options.TokenParameters.ValidateLifetime,
            ClockSkew = options.TokenParameters.ClockSkew,
        };

        var signalRJwtEvent = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                StringValues accessToken = context.Request.Query["access_token"];

                // If the request is for our hub...
                var path = context.HttpContext.Request.Path;
                if (options.ValidEventPaths == null || string.IsNullOrEmpty(accessToken))
                    return Task.CompletedTask;

                foreach (var validPath in options.ValidEventPaths)
                {
                    if (path.StartsWithSegments(validPath))
                    {
                        // Read the token out of the query string
                        context.Token = accessToken;
                    }
                }
                return Task.CompletedTask;
            }
        };

        services.AddAuthentication(authenticationOptions =>
        {
            authenticationOptions.DefaultScheme = options.DefaultScheme;
            authenticationOptions.DefaultAuthenticateScheme = options.DefaultAuthenticateScheme;
            authenticationOptions.DefaultChallengeScheme = options.DefaultChallengeScheme;

        }).AddJwtBearer(options.DefaultScheme, configureOptions =>
        {
            configureOptions.RequireHttpsMetadata = options.RequireHttpsMetadata;
            configureOptions.Authority = options.Authority;
            configureOptions.Audience = options.Audience;
            configureOptions.ClaimsIssuer = options.ClaimsIssuer;
            configureOptions.TokenValidationParameters = tokenValidationParameters;
            configureOptions.SaveToken = options.SaveToken;
            configureOptions.Events = signalRJwtEvent;
        });

        services.AddAuthorization(authorizationOptions =>
        {
            foreach (var identityPolicy in options.Policies)
            {
                authorizationOptions.AddPolicy(identityPolicy.Name, policy => policy.RequireClaim(identityPolicy.ClaimType, identityPolicy.Values));
            }
        });

        return services;
    }
    public static IApplicationBuilder UseIdentity(this IApplicationBuilder app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
        return app;
    }
}