namespace MiProyectoMVC.Models;

using System;
using System.Text.Json.Serialization;

public class BitacoraEvento
{
    [JsonPropertyName("idEvento")]
    public int IdEvento { get; set; }

    [JsonPropertyName("tablaDeEvento")]
    public string TablaDeEvento { get; set; } = string.Empty;

    [JsonPropertyName("tipoDeEvento")]
    public string TipoDeEvento { get; set; } = string.Empty;

    [JsonPropertyName("fechaDeEvento")]
    public DateTime FechaDeEvento { get; set; }

    [JsonPropertyName("descripcionDeEvento")]
    public string DescripcionDeEvento { get; set; } = string.Empty;

    [JsonPropertyName("stackTrace")]
    public string StackTrace { get; set; } = string.Empty;

    [JsonPropertyName("datosAnteriores")]
    public string? DatosAnteriores { get; set; }

    [JsonPropertyName("datosPosteriores")]
    public string? DatosPosteriores { get; set; }
}