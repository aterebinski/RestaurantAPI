using NLog.Web;
using RestaurantAPI;
using RestaurantAPI.Entities;
using RestaurantAPI.Middleware;
using RestaurantAPI.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// NLog: Setup NLog for Dependency injection
builder.Logging.ClearProviders();
builder.Host.UseNLog();

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<RestaurantDbContext>();
builder.Services.AddScoped<RestaurantSeeder>();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<RestaurantService>();
builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IDishService, DishService>();

var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();

var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<RestaurantSeeder>();
seeder.Seed();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "RestaurantAPi Swagger");
});

app.UseAuthorization();

app.MapControllers();

app.Run();
