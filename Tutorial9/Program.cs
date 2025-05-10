using Tutorial8.Config;
using Tutorial8.Infrastructure;
using Tutorial9.Repository;
using Tutorial9.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<IWarehouseService, WarehouseService>();
builder.Services.AddScoped<IWarehouseRepository, WarehouseRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddSingleton<ISqlConnectionFactory, SqlConnectionFactory>();
builder.Services.AddSingleton<DatabaseInitializer>();

var app = builder.Build();

app.UseAuthorization();
app.MapControllers();
await app.RunAsync();