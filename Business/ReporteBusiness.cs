using MiProyectoMVC.Models;
using MiProyectoMVC.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MiProyectoMVC.Business
{
    public class ReporteBusiness
    {
        private readonly ICajaRepository _cajaRepo;
        private readonly ISinpeRepository _sinpeRepo;
        private readonly IReporteRepository _reporteRepo;

        private const decimal PorcentajeDeComision = 0.05m; // 5% por ejemplo

        public ReporteBusiness(ICajaRepository cajaRepo, ISinpeRepository sinpeRepo, IReporteRepository reporteRepo)
        {
            _cajaRepo = cajaRepo;
            _sinpeRepo = sinpeRepo;
            _reporteRepo = reporteRepo;
        }

        public List<ReporteMensual> GenerarReportes()
        {
            var cajas = _cajaRepo.ObtenerTodos().Result;
            var sinpes = _sinpeRepo.ObtenerTodos();

            var comercios = cajas
                .Where(c => c.Comercio != null)
                .GroupBy(c => c.Comercio!.IdComercio);

            List<ReporteMensual> reportes = new List<ReporteMensual>();

            foreach (var grupo in comercios)
            {
                var idComercio = grupo.Key;
                var nombreComercio = grupo.First().Comercio!.Nombre;

                var cajasComercio = grupo.ToList();
                var sinpesComercio = sinpes
                    .Where(s => cajasComercio.Any(c => c.Comercio!.Telefono == s.TelefonoDestinatario))
                    .ToList();

                var reporte = new ReporteMensual
                {
                    IdComercio = idComercio,
                    NombreComercio = nombreComercio,
                    CantidadDeCajas = cajasComercio.Count,
                    CantidadDeSINPES = sinpesComercio.Count,
                    MontoTotalRecaudado = sinpesComercio.Sum(s => s.Monto),
                    MontoTotalDeComision = sinpesComercio.Sum(s => s.Monto) * PorcentajeDeComision,
                    FechaReporte = DateTime.Now
                };

                // Persistir: si ya existe, actualizar
                var existente = _reporteRepo.ObtenerPorComercioYMes(idComercio, DateTime.Now.Month, DateTime.Now.Year);
                if (existente != null)
                {
                    reporte.IdReporte = existente.IdReporte;
                    _reporteRepo.Actualizar(reporte);
                }
                else
                {
                    _reporteRepo.Guardar(reporte);
                }

                reportes.Add(reporte);
            }

            return reportes;
        }

        public List<ReporteMensual> ObtenerTodos()
        {
            return _reporteRepo.ObtenerTodos();
        }
    }
}