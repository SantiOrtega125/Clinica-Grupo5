using System.ComponentModel.DataAnnotations;

namespace Clinica.Models
{
    public class Medico
    {
        public int Id { get; set; }

        [Display(Name = "Nombre del médico")]
        [Required(ErrorMessage = "El nombre del médico es obligatorio.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 100 caracteres.")]
        public string Nombre { get; set; } = string.Empty;

        [Display(Name = "Número de licencia")]
        [Required(ErrorMessage = "El número de licencia es obligatorio.")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "La licencia debe tener entre 5 y 20 caracteres.")]
        public string NumeroLicencia { get; set; } = string.Empty;

        [Display(Name = "Años de experiencia")]
        [Required(ErrorMessage = "Ingrese los años de experiencia.")]
        [Range(0, 60, ErrorMessage = "Los años de experiencia deben estar entre 0 y 60.")]
        public int AniosExperiencia { get; set; }

        // Clave foránea: el Id de la especialidad a la que pertenece
        [Display(Name = "Especialidad")]
        [Required(ErrorMessage = "Seleccione una especialidad.")]
        public int EspecialidadId { get; set; }

        // Navegación: el objeto Especialidad completo (el ? es obligatorio)
        public Especialidad? Especialidad { get; set; }
    }
}