namespace MiProyectoMVC.Models;

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class Comercio
{
    [JsonPropertyName("idComercio")]
    public int IdComercio { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(100)]
    [JsonPropertyName("nombre")]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "La dirección es obligatoria")]
    [StringLength(200)]
    [JsonPropertyName("direccion")]
    public string Direccion { get; set; } = string.Empty;

    [Required(ErrorMessage = "El teléfono es obligatorio")]
    [StringLength(20)]
    [JsonPropertyName("telefono")]
    public string Telefono { get; set; } = string.Empty;

    [StringLength(100)]
    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("activo")]
    public bool Activo { get; set; } = true;
}