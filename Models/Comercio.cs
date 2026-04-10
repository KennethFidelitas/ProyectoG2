namespace MiProyectoMVC.Models;

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class Comercio
{
    [JsonPropertyName("idComercio")]
    public int IdComercio { get; set; }

    [StringLength(100)]
    [JsonPropertyName("nombre")]
    public string? Nombre { get; set; }

    [StringLength(200)]
    [JsonPropertyName("direccion")]
    public string? Direccion { get; set; }

    [StringLength(20)]
    [JsonPropertyName("telefono")]
    public string? Telefono { get; set; }

    [StringLength(100)]
    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("activo")]
    public bool Activo { get; set; } = true;
}