
/// <summary>
/// Entry point for the ASP.NET Core Web API application.
/// Configures services (DI), EF Core, JWT authentication, and middleware pipeline.
/// Connects backend to Angular frontend via CORS and exposes API endpoints.
/// </summary>
using System.Text;
using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add controllers (API endpoints)
builder.Services.AddControllers();

// Register EF Core DbContext with Sqlite connection from appsettings.json
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Allow cross-origin requests from Angular dev server
builder.Services.AddCors();

// Register TokenService for JWT creation (Dependency Injection)
builder.Services.AddScoped<ITokenService, TokenService>();

// Configure JWT authentication for secure endpoints
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        // Read secret key from config for signing JWTs
        var tokenKey = builder.Configuration["TokenKey"]
            ?? throw new Exception("Token key not found - Program.cs");
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey)),
            ValidateIssuer = false, // Not validating issuer for dev simplicity
            ValidateAudience = false // Not validating audience for dev simplicity
        };
    });

var app = builder.Build();

// Redirect HTTP to HTTPS for security
app.UseHttpsRedirection();

// Enable CORS for Angular dev server (http/https)
app.UseCors(x => x.AllowAnyMethod().AllowAnyHeader()
    .WithOrigins("http://localhost:4200", "https://localhost:4200"));

// Enable authentication/authorization middleware
app.UseAuthentication();
app.UseAuthorization();

// Map controller routes (e.g., /api/account)
app.MapControllers();

app.Run();
