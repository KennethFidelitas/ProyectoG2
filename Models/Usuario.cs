namespace MiProyectoMVC.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public Comercio Comercio { get; set; }  // Objeto anidado
        public string IdNetUser { get; set; }
        public string Nombres { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string Identificacion { get; set; }
        public string CorreoElectronico { get; set; }
        public DateTime FechaDeRegistro { get; set; }
        public DateTime FechaDeModificacion { get; set; }
        public bool Estado { get; set; }

        // Propiedad de conveniencia para acceder fácilmente al IdComercio
        public int IdComercio
        {
            get => Comercio?.IdComercio ?? 0;
            set
            {
                if (Comercio == null)
                    Comercio = new Comercio();
                Comercio.IdComercio = value;
            }
        }
    }
}