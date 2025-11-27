using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// =============================================================
// 1. Load ocelot.json
// =============================================================
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

// =============================================================
// 2. Add CORS (Allow All)
// =============================================================
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

// =============================================================
// 3. Swagger for Gateway UI
// =============================================================
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// =============================================================
// 4. JWT Authentication
// =============================================================

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("JwtBearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],

            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

// =============================================================
// 5. Add Ocelot
// =============================================================
builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();

// =============================================================
// 6. Enable CORS
// =============================================================
app.UseCors("AllowAll");

// =============================================================
// 7. Swagger
// =============================================================
app.UseSwagger();
app.UseSwaggerUI();

// =============================================================
// 8. Use Authentication & Authorization
// =============================================================
app.UseAuthentication();
app.UseAuthorization();

// =============================================================
// 9. Run Ocelot Middleware
// =============================================================
await app.UseOcelot();

app.Run();
