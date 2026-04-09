using MiProyectoMVC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiProyectoMVC.Repositories;

public interface IComercioRepository
{
    Task<List<Comercio>> ObtenerTodos();
    Task<Comercio?> ObtenerPorId(int id);
    Task Registrar(Comercio comercio);
    Task Editar(Comercio comercio);
    Task Eliminar(int id);
}