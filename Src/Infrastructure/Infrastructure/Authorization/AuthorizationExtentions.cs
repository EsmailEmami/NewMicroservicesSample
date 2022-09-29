using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using JwtBearerOptions = Application.Authorization.JwtBearerOptions;

namespace Infrastructure.Authorization;

public static class AuthorizationExtentions
{
    public static IServiceCollection AddCustomizedAuthorization(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();

        var options = new JwtBearerOptions();
        configuration.GetSection(nameof(JwtBearerOptions)).Bind(options);
        var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(options.SecretKey));

        services.Configure<JwtBearerOptions>(config =>
        {
            config = options;
            config.SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
        });


        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = options.ValidateIssuer,
            ValidIssuer = options.Issuer,
            ValidateAudience = options.ValidateAudience,
            ValidAudience = options.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = signingKey,
            RequireExpirationTime = false,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };

        var signalRJwtEvent = new JwtBearerEvents()
        {
            OnMessageReceived = context =>
            {
                StringValues accessToken = context.Request.Query["access_token"];

                // If the request is for our hub...
                var path = context.HttpContext.Request.Path;
                if (!string.IsNullOrEmpty(accessToken))
                {
                    foreach (var route in options.RoutesToValidate)
                    {
                        // Read the token out of the query string
                        context.Token = accessToken;
                    }
                }
                return Task.CompletedTask;
            }
        };

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

        }).AddJwtBearer(options.ProviderKey, configureOptions =>
        {
            configureOptions.ClaimsIssuer = options.Issuer;
            configureOptions.TokenValidationParameters = tokenValidationParameters;
            configureOptions.SaveToken = true;
            configureOptions.Events = signalRJwtEvent;
        });

        services.AddAuthorization();

        services.AddCors(corsOptions =>
        {
            corsOptions.AddPolicy(options.CorsName, builder =>
            {
                builder.AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .WithOrigins(options.OriginAccess)
                    .Build();

            });
        });

        return services;
    }
    public static IApplicationBuilder UseCustomizedAuthentication(this IApplicationBuilder app, IConfiguration configuration)
    {
        app.UseCors(configuration.GetValue<string>("JwtBearerOptions:CorsName"));

        app.UseAuthentication();
        app.UseAuthorization();
        return app;
    }
}