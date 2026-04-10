using MiProyectoMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MiProyectoMVC.Repositories
{
    public class ReporteRepository : IReporteRepository
    {
        private static List<ReporteMensual> _reportes = new List<ReporteMensual>();
        private static int _idCounter = 1;

        public List<ReporteMensual> ObtenerTodos()
        {
            return _reportes.OrderByDescending(r => r.FechaReporte).ToList();
        }

        public void Guardar(ReporteMensual reporte)
        {
            reporte.IdReporte = _idCounter++;
            _reportes.Add(reporte);
        }

        public void Actualizar(ReporteMensual reporte)
        {
            var existente = _reportes.FirstOrDefault(r => r.IdReporte == reporte.IdReporte);
            if (existente != null)
            {
                existente.NombreComercio = reporte.NombreComercio;
                existente.CantidadDeCajas = reporte.CantidadDeCajas;
                existente.MontoTotalRecaudado = reporte.MontoTotalRecaudado;
                existente.CantidadDeSINPES = reporte.CantidadDeSINPES;
                existente.MontoTotalDeComision = reporte.MontoTotalDeComision;
                existente.FechaReporte = reporte.FechaReporte;
            }
        }

        public ReporteMensual? ObtenerPorComercioYMes(int idComercio, int mes, int anio)
        {
            return _reportes.FirstOrDefault(r =>
                r.IdComercio == idComercio &&
                r.FechaReporte.Month == mes &&
                r.FechaReporte.Year == anio
            );
        }
    }
}