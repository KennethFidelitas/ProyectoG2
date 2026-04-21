namespace MiProyectoMVC.Models
{
    public class UsuarioAuth
    {
        public string Correo { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
        public int IdComercio { get; set; }
    }
}