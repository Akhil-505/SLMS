using ReportingService.Repositories.Inventory;
using ReportingService.Repositories.Compliance;
using ReportingService.Services;

var builder = WebApplication.CreateBuilder(args);

// =============================================================
// 1. Add Controllers
// =============================================================
builder.Services.AddControllers()
    .AddJsonOptions(o =>
    {
        o.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

// =============================================================
// 2. Swagger
// =============================================================
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// =============================================================
// 3. HttpClientFactory (required for Inventory + Compliance)
// =============================================================
builder.Services.AddHttpClient();

// =============================================================
// 4. Register Inventory Repositories
// =============================================================
builder.Services.AddScoped<ILicenseRepository, LicenseRepository>();
builder.Services.AddScoped<IEntitlementRepository, EntitlementRepository>();
builder.Services.AddScoped<IInstallationRepository, InstallationRepository>();

// =============================================================
// 5. Register Compliance Repository
// =============================================================
builder.Services.AddScoped<IComplianceReportRepository, ComplianceReportRepository>();

// =============================================================
// 6. Register Business Services (Analytics Layer)
// =============================================================
builder.Services.AddScoped<IReportingService, ReportingService.Services.ReportingService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddScoped<ITrendService, TrendService>();

// =============================================================
// 7. CORS Policy
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
// 8. Middleware
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
