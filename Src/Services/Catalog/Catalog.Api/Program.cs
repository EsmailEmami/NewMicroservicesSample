using Application.Filters;
using Catalog.Application.Events;
using Catalog.Infrastructure;
using Catalog.Infrastructure.Context;
using Infrastructure.Databases.EntityFrameworkCore;
using Infrastructure.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the 
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddServiceRegistration(builder.Configuration, typeof(Program), typeof(ProductAdded));
builder.Services.AddControllers(opt =>
{
    opt.Filters.Add<ExceptionFilter>();
});
builder.Services.AddHealthChecks();

var app = builder.Build();

app.MigrateDatabase<CatalogDbContext>((context, provider) =>
{
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.UseSubscribeAllEvents(typeof(ProductAdded));
app.MapControllers();
app.Run();
