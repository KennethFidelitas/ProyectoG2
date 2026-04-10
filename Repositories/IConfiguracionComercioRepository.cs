using MiProyectoMVC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiProyectoMVC.Repositories
{
    public interface IConfiguracionComercioRepository
    {
        Task<List<ConfiguracionComercio>> ObtenerTodos();
        Task<ConfiguracionComercio> ObtenerPorComercio(int idComercio);
        Task Crear(ConfiguracionComercio config);
        Task Editar(ConfiguracionComercio config);
    }
}