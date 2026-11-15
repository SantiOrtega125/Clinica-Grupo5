using System.ComponentModel.DataAnnotations;

namespace Clinica.Models
{
    public class Especialidad
    {
        public int Id { get; set; }

        [Display(Name = "Especialidad")]
        [Required(ErrorMessage = "El nombre de la especialidad es obligatorio.")]
        [StringLength(80, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 80 caracteres.")]
        public string Nombre { get; set; } = string.Empty;

        [Display(Name = "Descripción")]
        [StringLength(250, ErrorMessage = "La descripción no puede superar los 250 caracteres.")]
        public string? Descripcion { get; set; }
    }
}