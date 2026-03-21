namespace MiProyectoMVC.Models;

using System;

public class Sinpe
{
    public int IdSinpe { get; set; }
    public string TelefonoOrigen { get; set; } = string.Empty;
    public string NombreOrigen { get; set; } = string.Empty;
    public string TelefonoDestinatario { get; set; } = string.Empty;
    public string NombreDestinatario { get; set; } = string.Empty;
    public decimal Monto { get; set; }
    public DateTime FechaDeRegistro { get; set; }
    public string? Descripcion { get; set; }
    public bool Estado { get; set; }
    public int IdCaja { get; set; }
}