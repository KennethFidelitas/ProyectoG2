using System;

namespace MiProyectoMVC.Models;

public class Caja
{
    public int IdCaja { get; set; }

    public int ComercioId { get; set; }

    public string NombreComercio { get; set; } = string.Empty;

    public decimal? MontoFinal { get; set; }

    public DateTime FechaApertura { get; set; }

    public DateTime? FechaCierre { get; set; }

    public bool EstaAbierta { get; set; }
}