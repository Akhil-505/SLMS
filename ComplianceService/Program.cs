using ComplianceService.Models.Database;
using ComplianceService.Repositories.Database;
using ComplianceService.Repositories.Inventory;
using ComplianceService.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// =============================================================
// 1. Add Controllers
// =============================================================
builder.Services.AddControllers()
    .AddJsonOptions(o =>
    {
        o.JsonSerializerOptions.ReferenceHandler =
            System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

// =============================================================
// 2. Swagger (API Documentation)
// =============================================================
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// =============================================================
// 3. Database Configuration (SQL Server)
// =============================================================
builder.Services.AddDbContext<ComplianceDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"));
});

// =============================================================
// 4. HttpClientFactory (For calling InventoryService)
// =============================================================
builder.Services.AddHttpClient();

// =============================================================
// 5. Inventory HTTP Repositories
// =============================================================
builder.Services.AddScoped<ILicenseDataRepository, LicenseDataRepository>();
builder.Services.AddScoped<IEntitlementDataRepository, EntitlementDataRepository>();
builder.Services.AddScoped<IInstallationDataRepository, InstallationDataRepository>();
builder.Services.AddScoped<ICatalogDataRepository, CatalogDataRepository>();

// =============================================================
// 6. Compliance Database Repositories
// =============================================================
builder.Services.AddScoped<IComplianceEventRepository, ComplianceEventRepository>();
builder.Services.AddScoped<IComplianceRuleRepository, ComplianceRuleRepository>();
builder.Services.AddScoped<IRenewalRepository, RenewalRepository>();

// =============================================================
// 7. Business Services (Engine + Renewal Manager)
// =============================================================
builder.Services.AddScoped<IComplianceEngine, ComplianceEngine>();
builder.Services.AddScoped<IRenewalManager, RenewalManager>();

// =============================================================
// 8. Notification Sender (Email/Webhook/etc.)
// =============================================================
builder.Services.AddScoped<INotificationSender, EmailNotificationSender>();

// =============================================================
// 9. Background Scheduler (Runs every N minutes)
// =============================================================
builder.Services.AddHostedService<ComplianceSchedulerHostedService>();

// =============================================================
// 10. CORS Policy (Allow All for development)
// =============================================================
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});

var app = builder.Build();

// =============================================================
// 11. Apply Migrations Automatically (DEV ONLY)
// =============================================================
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ComplianceDbContext>();
    db.Database.Migrate(); // Creates DB if not exists
}

// =============================================================
// 12. Enable Middleware
// =============================================================
app.UseCors("AllowAll");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

app.Run();
