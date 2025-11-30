using ReportingService.Repositories.Inventory;
using ReportingService.Repositories.Compliance;
using ReportingService.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
// 7. JWT Authentication
// =============================================================
var jwtKey = builder.Configuration["Jwt:Key"];
// Allowed roles for this service
var allowedRoles = new[] { "Admin", "Finance", "Auditor", "ReadOnly" };

if (!string.IsNullOrEmpty(jwtKey))
{
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
            };
        });
}

// Always add authorization policies so they are available even if JWT is not configured
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdministratorsOnly", policy =>
        policy.RequireRole(allowedRoles));
});

// =============================================================
// 8. CORS Policy
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
// 9. Middleware
// =============================================================
app.UseCors("AllowAll");

// Ensure authentication/authorization run before Swagger/UI so authorization challenges have a default scheme
if (!string.IsNullOrEmpty(jwtKey))
{
    app.UseAuthentication();
}
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Authentication & Authorization middleware
if (!string.IsNullOrEmpty(jwtKey))
{
    app.UseAuthentication();
}
app.UseAuthorization();

app.MapControllers();
app.Run();
