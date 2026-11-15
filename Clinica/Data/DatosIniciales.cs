using Microsoft.AspNetCore.Identity;

namespace Clinica.Data
{
    public static class DatosIniciales
    {
        public static async Task CrearRolesYUsuariosAsync(IServiceProvider services)
        {
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

            // 1. Los dos roles del grupo 4
            string[] roles = { "Director", "Paciente" };
            foreach (var rol in roles)
            {
                if (!await roleManager.RoleExistsAsync(rol))
                {
                    await roleManager.CreateAsync(new IdentityRole(rol));
                }
            }

            // 2. Un usuario de prueba por cada rol
            await CrearUsuarioAsync(userManager, "director@clinica.com", "Director123*", "Director");
            await CrearUsuarioAsync(userManager, "paciente@clinica.com", "Paciente123*", "Paciente");
        }

        private static async Task CrearUsuarioAsync(
            UserManager<ApplicationUser> userManager, string email, string password, string rol)
        {
            // Si el usuario no existe, se crea
            var usuario = await userManager.FindByEmailAsync(email);
            if (usuario == null)
            {
                usuario = new ApplicationUser { UserName = email, Email = email, EmailConfirmed = true };
                var resultado = await userManager.CreateAsync(usuario, password);
                if (!resultado.Succeeded)
                {
                    throw new Exception($"No se pudo crear {email}: " +
                        string.Join(", ", resultado.Errors.Select(e => e.Description)));
                }
            }

            // FUERA del if: el rol se asigna aunque el usuario ya existiera
            if (!await userManager.IsInRoleAsync(usuario, rol))
            {
                await userManager.AddToRoleAsync(usuario, rol);
            }
        }
    }
}