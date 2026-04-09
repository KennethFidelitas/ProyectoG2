namespace MiProyectoMVC.Models;

using System;
using System.Text.Json.Serialization;

public class Sinpe
{
    [JsonPropertyName("idSinpe")]
    public int IdSinpe { get; set; }

    [JsonPropertyName("telefonoOrigen")]
    public string TelefonoOrigen { get; set; } = string.Empty;

    [JsonPropertyName("nombreOrigen")]
    public string NombreOrigen { get; set; } = string.Empty;

    [JsonPropertyName("telefonoDestinatario")]
    public string TelefonoDestinatario { get; set; } = string.Empty;

    [JsonPropertyName("nombreDestinatario")]
    public string NombreDestinatario { get; set; } = string.Empty;

    [JsonPropertyName("monto")]
    public decimal Monto { get; set; }

    [JsonPropertyName("fechaDeRegistro")]
    public DateTime FechaDeRegistro { get; set; }

    [JsonPropertyName("descripcion")]
    public string? Descripcion { get; set; }

    [JsonPropertyName("estado")]
    public bool Estado { get; set; }

    [JsonPropertyName("idCaja")]
    public int IdCaja { get; set; }
}