using System;

namespace MiProyectoMVC.Models
{
    public class ReporteMensual
    {
        public int IdReporte { get; set; } // opcional si lo guardas en BD
        public int IdComercio { get; set; }
        public string NombreComercio { get; set; } = string.Empty;
        public int CantidadDeCajas { get; set; }
        public decimal MontoTotalRecaudado { get; set; }
        public int CantidadDeSINPES { get; set; }
        public decimal MontoTotalDeComision { get; set; }
        public DateTime FechaReporte { get; set; }
    }
}