using Catalog.Application.CommandHandlers;
using Catalog.Infrastructure;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "ASPNET Core v1",
            Description = "",
            Contact = new OpenApiContact { Name = "Esmail Emami", Email = "esmailemami84@gmail.com", Url = new Uri("https://github.com/EsmailEmami") }
        });

        //options.OperationFilter<ApiVersioningHeaderFilter>();

        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header,
                },
                new List<string>()
            }
        });
    });

builder.Services.AddServiceRegistration(builder.Configuration, typeof(Program), typeof(CatalogCommandHandler));

var app = builder.Build();

//app.MigrateDatabase<CatalogDbContext>((context, provider) =>
//{
//});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    });
}

app.UseRouting();
app.UseCoreRegistration(app.Configuration, app.Lifetime);
app.MapHealthChecks("/health");
app.MapControllers();
app.Run();
