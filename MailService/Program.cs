using MailService.Data;
using MailService.Repositories;
using MailService.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add Controllers
builder.Services.AddControllers();

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register EF Core (in-memory or SQL)
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseInMemoryDatabase("EmailDb"));

// DI for Repository + Service
builder.Services.AddScoped<IEmailRepository, EmailRepository>();
builder.Services.AddScoped<IEmailService, EmailService>();

var app = builder.Build();

// Swagger
app.UseSwagger();
app.UseSwaggerUI();

// Map Controllers
app.MapControllers();

app.Run();
