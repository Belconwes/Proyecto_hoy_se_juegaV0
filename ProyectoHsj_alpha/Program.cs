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
using ProyectoHsj_alpha.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<ReservaService>();
builder.Services.AddScoped<IPermisoRepository, PermisoRepository>();
builder.Services.AddControllersWithViews();
builder.Services.AddControllers();
builder.Services.AddDbContext<HoySeJuegaContext>(Options =>
{
    Options.UseSqlServer(builder.Configuration.GetConnectionString("Conexion"));
});
// seteo de contrase�a de aplicacion
//  dotnet user-secrets set "smtp:Password" "dzur havz dgxu vtgd"
builder.Services.AddFluentEmail(builder.Configuration["Smtp:Username"], "HoySeJuega")
    .AddSmtpSender(new SmtpClient(builder.Configuration["Smtp:Host"])
    {
        Port = int.Parse(builder.Configuration["Smtp:Port"]),
        Credentials = new NetworkCredential(
            builder.Configuration["Smtp:Username"],
            builder.Configuration["smtp:Password"] // seteada en dotnet user-secrets
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
       options.LoginPath = "/Acces/Login"; //Ruta de la p�gina de inicio de sesi�n
       options.AccessDeniedPath = "/Acces/AccessDenied"; // Ruta de la p�gina de acceso denegado
       options.ExpireTimeSpan = TimeSpan.FromMinutes(30);// Tiempo de expiraci�n de la cookie

   }
    )
.AddGoogle(options =>
{
    // Setear los secrets para posteriormente usarlos en el builder (De lo posible hacerlo en la termianal de powershell )
    //dotnet user-secrets set "Authentication:Google:ClientId" "925616110531-4n85qcuq48afgvi06sdp0qos60g2q8ph.apps.googleusercontent.com "

    //dotnet user-secrets set "Authentication:Google:ClientSecret" "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 �
    
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

// Registrar rutas de MVC y API
app.UseEndpoints(endpoints =>
{
    // Rutas de los controladores MVC
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    // Rutas de los controladores API
    endpoints.MapControllers();
});


app.Run();
