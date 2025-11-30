using ComplianceService.Models;
using ComplianceService.Repositories;
using ComplianceService.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// DB
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Inventory API Client
builder.Services.AddHttpClient<InventoryApiClient>(client =>
{
    var url = builder.Configuration["ServiceUrls:InventoryService"];

    if (string.IsNullOrEmpty(url))
        throw new Exception("❌ InventoryService URL is missing. Add it in appsettings.json under ServiceUrls!");

    client.BaseAddress = new Uri(url);
});

// DI
builder.Services.AddScoped<IComplianceEventRepository, ComplianceEventRepository>();
builder.Services.AddScoped<MatchingService>();
builder.Services.AddScoped<ComplianceEngineService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware
app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();
app.Run();
