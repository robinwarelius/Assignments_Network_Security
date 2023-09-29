

using IoT_BackEnd.Data;
using IoT_BackEnd.Hubs;
using IoT_BackEnd.Repositories;
using IoT_BackEnd.Repositories.IRepository;
using IoT_BackEnd.Services;
using IoT_BackEnd.Services.IServices;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Db
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Service
builder.Services.AddTransient<IUnitRepository, UnitRepository>();
builder.Services.AddTransient<IUnitService, UnitService>();



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

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.UseCors("AllowSpecificOrigin");

// Route to UnitHub
app.MapHub<UnitHub>("/hubs/unit");

app.Run();
