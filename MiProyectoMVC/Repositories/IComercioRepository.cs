using MiProyectoMVC.Models;
using System.Collections.Generic;

namespace MiProyectoMVC.Repositories
{
    public interface IComercioRepository
    {
        List<Comercio> ObtenerTodos();

        Comercio? ObtenerPorId(int id);

        void Registrar(Comercio comercio);

        void Editar(Comercio comercio);

        void Eliminar(int id);
    }
}