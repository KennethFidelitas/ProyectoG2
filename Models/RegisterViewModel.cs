using System.ComponentModel.DataAnnotations;

namespace MiProyectoMVC.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "Correo inválido")]
        public string Correo { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Mínimo 6 caracteres")]
        public string Contrasena { get; set; } = string.Empty;

        [Required(ErrorMessage = "Confirme la contraseña")]
        [DataType(DataType.Password)]
        [Compare("Contrasena", ErrorMessage = "Las contraseñas no coinciden")]
        public string ConfirmarContrasena { get; set; } = string.Empty;

        [Required(ErrorMessage = "Seleccione un rol")]
        public string Rol { get; set; } = string.Empty;
    }
}