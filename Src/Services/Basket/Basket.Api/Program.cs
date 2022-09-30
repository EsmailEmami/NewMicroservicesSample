using Application.Filters;
using Basket.Application.EventHandlers;
using Basket.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the 
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddServiceRegistration(builder.Configuration, typeof(Program), typeof(BasketEventHandler));
builder.Services.AddControllers(opt =>
{
    opt.Filters.Add<ExceptionFilter>();
});
builder.Services.AddHealthChecks();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCoreRegistration();
app.MapControllers();
app.Run();