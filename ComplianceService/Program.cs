using ComplianceService.Models;
using ComplianceService.Repositories;
using ComplianceService.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
        throw new Exception("InventoryService URL is missing. Add it in appsettings.json under ServiceUrls!");

    client.BaseAddress = new Uri(url);
});

// DI
builder.Services.AddScoped<IComplianceEventRepository, ComplianceEventRepository>();
builder.Services.AddScoped<MatchingService>();
builder.Services.AddScoped<ComplianceEngineService>();

// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// JWT Authentication
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

// Add authorization policies (available even if JWT key not configured)
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdministratorsOnly", policy =>
        policy.RequireRole(allowedRoles));

    options.AddPolicy("EmployeesOnly", policy =>
        policy.RequireClaim("EmployeeNumber"));
});

var app = builder.Build();

// Middleware
// Ensure authentication/authorization run before Swagger/UI so authorization challenges have a default scheme
if (!string.IsNullOrEmpty(jwtKey))
{
    app.UseAuthentication();
}
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

// Authentication & Authorization middleware
if (!string.IsNullOrEmpty(jwtKey))
{
    app.UseAuthentication();
}
app.UseAuthorization();

app.MapControllers();
app.Run();
