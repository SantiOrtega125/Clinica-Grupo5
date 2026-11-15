namespace Clinica.Models
{
    public class Medico
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string NumeroLicencia { get; set; } = string.Empty;
        public int AniosExperiencia { get; set; }
    }
}