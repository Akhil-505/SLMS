using AuthService.Models.Database;
using AuthService.Repositories;
using AuthService.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// =============================================================
// 1. ADD CONTROLLERS
// =============================================================
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

// =============================================================
// 2. SWAGGER
// =============================================================
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// =============================================================
// 3. DATABASE - SQL Server
// =============================================================
builder.Services.AddDbContext<AuthDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"));
});

// HttpClient for Mail Service
builder.Services.AddHttpClient<HttpEmailClient>(client =>
{
    var url = builder.Configuration["ServiceUrls:MailService"];
    if (string.IsNullOrEmpty(url))
        throw new Exception("MailService URL missing from ServiceUrls configuration.");

    client.BaseAddress = new Uri(url);
});



// =============================================================
// 4. REGISTER REPOSITORIES
// =============================================================
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

// =============================================================
// 5. REGISTER AUTHENTICATION SERVICES
// =============================================================
builder.Services.AddScoped<IAuthService, AuthService.Services.AuthService>();
builder.Services.AddScoped<IPasswordHasher, BcryptPasswordHasher>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IRefreshTokenService, RefreshTokenService>();

// =============================================================
// 6. JWT AUTHENTICATION SETUP
// =============================================================
var jwtKey = builder.Configuration["Jwt:Key"];
// Roles used across services
var allowedRoles = new[] { "Admin", "Finance", "Auditor", "ReadOnly" };

if (!string.IsNullOrEmpty(jwtKey))
{
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
            };
        });
}

// Always add authorization policies so they are available even if JWT is not configured
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdministratorsOnly", policy =>
        policy.RequireRole(allowedRoles));

    options.AddPolicy("EmployeesOnly", policy =>
        policy.RequireClaim("EmployeeNumber"));
});

// =============================================================
// 7. CORS (Allow All for Dev)
// =============================================================
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});

// =============================================================
// 8. BUILD APP
// =============================================================
var app = builder.Build();

// =============================================================
// 9. AUTO APPLY MIGRATIONS (Dev Only)
// =============================================================
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AuthDbContext>();
    db.Database.Migrate();
}

// =============================================================
// 10. MIDDLEWARE
// =============================================================
app.UseCors("AllowAll");

// Ensure authentication/authorization run before Swagger/UI so authorization challenges have a default scheme
if (!string.IsNullOrEmpty(jwtKey))
{
    app.UseAuthentication();  // <--- IMPORTANT
}
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();