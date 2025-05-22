using Application;
using Infrastructure;
using Persistance;
using Persistance.Contexts;
using WebAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.RegisterWebAPIServices(builder.Configuration,builder.Host);
builder.Services.RegisterPersistanceServices(builder.Configuration);
builder.Services.RegisterInfrastructureServices(builder.Configuration);
builder.Services.RegisterApplicationServices();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<TradewndContext>();
        await context.SeedAsync(); // burada çaðýr
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Seedleme sýrasýnda hata oluþtu: {ex.Message}");
        throw;
    }
}

app.ConfigureHttpPipeline();