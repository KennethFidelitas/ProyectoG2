using System.Text.Json.Serialization;

namespace MiProyectoMVC.Models
{
    public class UsuarioAuth
    {
        [JsonPropertyName("correo")]
        public string Correo { get; set; } = string.Empty;

        [JsonPropertyName("passwordHash")]
        public string PasswordHash { get; set; } = string.Empty;

        [JsonPropertyName("rol")]
        public string Rol { get; set; } = string.Empty;

        [JsonPropertyName("idComercio")]
        public int IdComercio { get; set; }
    }
}