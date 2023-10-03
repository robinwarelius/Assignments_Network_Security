
using IoT_ServiceBusConsumer.Extensions;
using IoT_ServiceBusConsumer.Messaging;
using IoT_ServiceBusConsumer.Messaging.IMessaging;
using Microsoft.EntityFrameworkCore;
using ServiceBusConsumer.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Db
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddSingleton<IAzureServiceBusConsumer, AzureServiceBusConsumer>();


var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseAzureServiceBusConsumer();
app.Run();
