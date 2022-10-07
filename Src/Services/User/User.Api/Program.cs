using BuildingBlocks.CatalogService.Product;
using User.Application.Core.User.Commands;
using User.Application.Services.User;
using User.Infrastructure;
using User.Infrastructure.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddServiceRegistration(builder.Configuration, typeof(Program), typeof(ProductAddedEvent), typeof(UserDbContext), typeof(UserCommandHandler), typeof(CreateUserCommand));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCoreRegistration(builder.Configuration);

app.UseAuthorization();

app.MapControllers();

app.Run();
