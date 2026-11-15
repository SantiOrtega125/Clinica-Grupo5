using Microsoft.EntityFrameworkCore;

public class ClinicaContext(DbContextOptions<ClinicaContext> options) : DbContext(options)
{
    public DbSet<Clinica.Models.Medico> Medico { get; set; } = default!;
}
