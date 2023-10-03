

using IoT_BackEnd.Data;
using IoT_BackEnd.Hubs;
using IoT_BackEnd.Models;
using IoT_BackEnd.Repositories;
using IoT_BackEnd.Repositories.IRepository;
using IoT_BackEnd.Services;
using IoT_BackEnd.Services.IServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Db
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("ApiSettings:JwtOptions"));
        
// Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

// Services
builder.Services.AddTransient<IUnitRepository, UnitRepository>();
builder.Services.AddTransient<IUnitService, UnitService>();
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<ITokenJwtGenerator, TokenJwtGenerator>();
builder.Services.AddTransient<IAdminService, AdminService>();
builder.Services.AddTransient<IAdminRepository, AdminRepository>();

// Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
            builder => builder
                .WithOrigins("https://localhost:7231")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials());
});

// SignalR
builder.Services.AddSignalR();

// Configure Authorization
var secret = builder.Configuration.GetValue<string>("ApiSettings:JwtOptions:Secret");
var audience = builder.Configuration.GetValue<string>("ApiSettings:JwtOptions:Audience");
var issuer = builder.Configuration.GetValue<string>("ApiSettings:JwtOptions:Issuer");
var key = Encoding.ASCII.GetBytes(secret);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{   
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidIssuer = issuer,
        ValidateAudience = true,
        ValidAudience = audience
    };
});
builder.Services.AddAuthorization();


var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseCors("AllowSpecificOrigin");

// Route to UnitHub
app.MapHub<UnitHub>("/hubs/unit");
app.Run();
