using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Clinica.Data;
using Clinica.Models;

public class ClinicaContext(DbContextOptions<ClinicaContext> options)
    : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Medico> Medico { get; set; } = default!;
    public DbSet<Especialidad> Especialidad { get; set; } = default!;
}