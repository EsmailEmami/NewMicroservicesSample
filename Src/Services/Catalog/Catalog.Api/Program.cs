using Application.ApiVersioning;
using Application.Filters;
using Application.Swagger;
using Catalog.Application.CommandHandlers;
using Catalog.Infrastructure;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the 
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1.0", new OpenApiInfo
        {
            Version = "v1.0",
            Title = "ASPNET Core v1.0",
            Description = "",
            Contact = new OpenApiContact { Name = "Esmail Emami", Email = "esmailemami84@gmail.com", Url = new Uri("https://github.com/EsmailEmami") }
        });

        options.OperationFilter<ApiVersioningHeaderFilter>();

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
builder.Services.AddControllers(opt =>
{
    opt.Filters.Add<ExceptionFilter>();
});
builder.Services.AddHealthChecks();

var app = builder.Build();

//app.MigrateDatabase<CatalogDbContext>((context, provider) =>
//{
//});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.UseSwagger(options =>
    {
        options.SerializeAsV2 = true;
    });
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1.0/swagger.json", "v1.0");
    });
}

app.UseCoreRegistration(builder.Configuration);
app.MapControllers();
app.Run();
