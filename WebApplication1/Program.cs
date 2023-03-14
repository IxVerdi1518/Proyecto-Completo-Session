using Microsoft.AspNetCore.Authentication.Cookies;
using WebApplication1.Servicios;
using WebApplication1.Sesion;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option =>
    {
        option.LoginPath = "/Auth/Login";
        option.AccessDeniedPath = "/Auth/AccessDenied";
        option.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    });
//builder.Services.AddSession( x => x.IdleTimeout = TimeSpan.FromSeconds(10));
builder.Services.AddSingleton<ServicioSingleton>();
builder.Services.AddTransient<ServicioTrasient>();
builder.Services.AddScoped<ServicioScope>();
builder.Services.AddScoped<ServicioCliente>();
builder.Services.AddSession();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}");

app.Run();
