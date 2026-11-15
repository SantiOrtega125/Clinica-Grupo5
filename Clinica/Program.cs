using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Clinica.Data;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ClinicaContext")
    ?? throw new InvalidOperationException("No se encontró la cadena de conexión 'ClinicaContext'.");

builder.Services.AddDbContext<ClinicaContext>(options => options.UseSqlServer(connectionString));

// Identity con roles: usuarios y roles guardados en ClinicaContext, sin confirmación de correo
builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()                     // ← FASE 5 (paso 13): activa los roles. Va ANTES de AddEntityFrameworkStores
    .AddEntityFrameworkStores<ClinicaContext>();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// ← FASE 5 (pasos 14 y 15): crear roles y usuarios de prueba al iniciar la aplicación
using (var scope = app.Services.CreateScope())
{
    await DatosIniciales.CrearRolesYUsuariosAsync(scope.ServiceProvider);
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();   // lee la cookie de sesión (y los roles que trae)
app.UseAuthorization();    // aplica [Authorize]

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages();       // activa las páginas de login y registro

app.Run();