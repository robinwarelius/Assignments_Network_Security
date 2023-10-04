using IoT_Frontend.Services.IServices;
using IoT_FrontEnd.Services.IServices;
using IoT_FrontEnd.Services;
using IoT_FrontEnd.Utilities;
using Microsoft.AspNetCore.Authentication.Cookies;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

// HTTP Client
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
builder.Services.AddHttpClient<IAuthService, AuthService>();
builder.Services.AddHttpClient<IAdvertService, AdvertService>();

// SignalR
builder.Services.AddSignalR();

// Static Details
SD.AuthApiUrl = builder.Configuration["ServiceUrls:AuthApi"];

// Services
builder.Services.AddTransient<IBaseService, BaseService>();
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenProvider, TokenProvider>();
builder.Services.AddTransient<IAdvertService, AdvertService>();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromHours(10); 
        options.LoginPath = "/Auth/Login"; 
        options.AccessDeniedPath = "/Auth/AccessDenied"; 
    });


var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Unit}/{action=Index}/{id?}");
app.Run();
