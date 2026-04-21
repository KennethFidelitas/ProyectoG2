using System;

namespace MiProyectoMVC.Models
{
    public class ConfiguracionComercio
    {
        public int IdConfiguracion { get; set; }
        public int IdComercio { get; set; }
        public int TipoConfiguracion { get; set; } 
        public int Comision { get; set; }
        public DateTime FechaDeRegistro { get; set; }
        public DateTime? FechaDeModificacion { get; set; }
        public bool Estado { get; set; }

        public string NombreComercio { get; set; }

        public string TipoConfiguracionTexto
        {
            get
            {
                return TipoConfiguracion switch
                {
                    1 => "Plataforma",
                    2 => "Externa",
                    3 => "Ambas",
                    _ => "Desconocido"
                };
            }
        }
    }
}
