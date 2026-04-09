namespace MiProyectoMVC.Repositories;

using MiProyectoMVC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICajaRepository
{
    Task Registrar(Caja caja);
    Task Editar(Caja caja);
    Task Eliminar(int id);
    Task CerrarCaja(int id, decimal montoFinal);
    Task<Caja?> ObtenerPorId(int id);
    Task<List<Caja>> ObtenerTodos();
}