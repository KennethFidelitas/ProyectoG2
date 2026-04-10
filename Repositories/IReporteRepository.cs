using MiProyectoMVC.Models;
using System.Collections.Generic;

namespace MiProyectoMVC.Repositories
{
    public interface IReporteRepository
    {
        List<ReporteMensual> ObtenerTodos();
        void Guardar(ReporteMensual reporte);
        void Actualizar(ReporteMensual reporte);
        ReporteMensual? ObtenerPorComercioYMes(int idComercio, int mes, int anio);
    }
}