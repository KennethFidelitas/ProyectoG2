using MiProyectoMVC.Models;
using MiProyectoMVC.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace MiProyectoMVC.Business
{
    public class ReporteBusiness
    {
        private readonly ICajaRepository _cajaRepo;
        private readonly ISinpeRepository _sinpeRepo;
        private readonly IReporteRepository _reporteRepo;
        private readonly IBitacoraRepository _bitacoraRepository;

        private const decimal PorcentajeDeComision = 0.05m;

        public ReporteBusiness(
            ICajaRepository cajaRepo,
            ISinpeRepository sinpeRepo,
            IReporteRepository reporteRepo,
            IBitacoraRepository bitacoraRepository)
        {
            _cajaRepo = cajaRepo;
            _sinpeRepo = sinpeRepo;
            _reporteRepo = reporteRepo;
            _bitacoraRepository = bitacoraRepository;
        }

        public List<ReporteMensual> GenerarReportes()
        {
            try
            {
                var cajas = _cajaRepo.ObtenerTodos().Result;
                var sinpes = _sinpeRepo.ObtenerTodos();

                var comercios = cajas
                    .Where(c => c.Comercio != null)
                    .GroupBy(c => c.Comercio!.IdComercio);

                var reportes = new List<ReporteMensual>();

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

                    var existente = _reporteRepo.ObtenerPorComercioYMes(idComercio, DateTime.Now.Month, DateTime.Now.Year);
                    if (existente != null)
                    {
                        reporte.IdReporte = existente.IdReporte;
                        _reporteRepo.Actualizar(reporte);
                        RegistrarBitacora("Reportes", "Editar",
                            $"Reporte del comercio ID {idComercio} actualizado.",
                            null, JsonSerializer.Serialize(existente), JsonSerializer.Serialize(reporte));
                    }
                    else
                    {
                        _reporteRepo.Guardar(reporte);
                        RegistrarBitacora("Reportes", "Registrar",
                            $"Reporte del comercio ID {idComercio} generado.",
                            null, null, JsonSerializer.Serialize(reporte));
                    }

                    reportes.Add(reporte);
                }

                return reportes;
            }
            catch (Exception ex)
            {
                RegistrarBitacora("Reportes", "Error", ex.Message, ex.StackTrace, null, null);
                throw;
            }
        }

        public List<ReporteMensual> ObtenerTodos()
        {
            return _reporteRepo.ObtenerTodos();
        }

        private void RegistrarBitacora(
            string tabla, string tipo, string descripcion,
            string? stackTrace, string? datosAnteriores, string? datosPosteriores)
        {
            _bitacoraRepository.RegistrarEvento(new BitacoraEvento
            {
                TablaDeEvento = tabla,
                TipoDeEvento = tipo,
                FechaDeEvento = DateTime.Now,
                DescripcionDeEvento = descripcion,
                StackTrace = stackTrace ?? "",
                DatosAnteriores = datosAnteriores,
                DatosPosteriores = datosPosteriores
            });
        }
    }
}