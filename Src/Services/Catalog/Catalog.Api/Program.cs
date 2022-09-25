
using Application.Filters;
using Catalog.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the 
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddServiceRegistration(builder.Configuration, typeof(Program));
builder.Services.AddControllers(opt => { opt.Filters.Add<ExceptionFilter>(); });
builder.Services.AddHealthChecks();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
