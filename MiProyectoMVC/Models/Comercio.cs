using System.ComponentModel.DataAnnotations;

namespace MiProyectoMVC.Models
{
    public class Comercio
    {
        public int IdComercio { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "La dirección es obligatoria")]
        [StringLength(200)]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "El teléfono es obligatorio")]
        [StringLength(20)]
        public string Telefono { get; set; }

        [StringLength(100)]
        public string Email { get; set; }

        public bool Activo { get; set; } = true;
    }
}