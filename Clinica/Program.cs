using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Clinica.Data;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ClinicaContext")
    ?? throw new InvalidOperationException("No se encontró la cadena de conexión 'ClinicaContext'.");

builder.Services.AddDbContext<ClinicaContext>(options => options.UseSqlServer(connectionString));

// Identity: usuarios guardados en ClinicaContext, sin confirmación de correo
builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ClinicaContext>();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();                 // CAMBIO 2

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();                          // CAMBIO 3: lee la cookie de sesión
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages();                              // CAMBIO 4: activa las páginas de login y registro

app.Run();