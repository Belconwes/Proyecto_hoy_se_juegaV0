using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ProyectoHsj_alpha.Models;
using Microsoft.AspNetCore.Authorization;
using System.Net.Mail;
using FluentEmail.Core;
using FluentEmail.Smtp;
using System.Net;
using Microsoft.AspNetCore.Authentication.Google;
using ProyectoHsj_alpha.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<ReservaService>();
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<HoySeJuegaContext>(Options =>
{
    Options.UseSqlServer(builder.Configuration.GetConnectionString("Conexion"));
});

// Contraseña de aplicacion smtp : dzur havz dgxu vtgd 
builder.Services.AddFluentEmail(builder.Configuration["Smtp:Username"], "HoySeJuega")
    .AddSmtpSender(new SmtpClient(builder.Configuration["Smtp:Host"])
    {
        Port = int.Parse(builder.Configuration["Smtp:Port"]),
        Credentials = new NetworkCredential(
            builder.Configuration["Smtp:Username"],
            builder.Configuration["smtp:Password"] // Asegúrate de que este nombre coincida con el usado en User Secrets
        ),
        EnableSsl = true
    });

// Configurar Identity

// Configuracion de autenticacion
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
  .AddCookie(options =>
   {
       options.LoginPath = "/Acces/Login"; //Ruta de la página de inicio de sesión
       options.AccessDeniedPath = "/Acces/AccessDenied"; // Ruta de la página de acceso denegado
       options.ExpireTimeSpan = TimeSpan.FromMinutes(30);// Tiempo de expiración de la cookie

   }
    )
.AddGoogle(options =>
{

    options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
    options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
    options.CallbackPath = new PathString("/Acces/ExternalLoginCallback");
});

// Configguracion de politicas de autorizacion
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("2"));
    options.AddPolicy("UserOnly", policy => policy.RequireRole("1"));
    options.AddPolicy("EmployedOnly", policy => policy.RequireRole("3"));
    options.AddPolicy("AdminOrEmployed", policy =>
       policy.RequireRole("2", "3")); // Admin (2) y Employed (3)
});

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
app.UseAuthentication(); // Ensure authentication middleware is added
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
