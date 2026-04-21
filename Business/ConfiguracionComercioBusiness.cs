using MiProyectoMVC.Models;
using MiProyectoMVC.Repositories;

namespace MiProyectoMVC.Business
{
    public class ConfiguracionComercioBusiness
    {
        private readonly IConfiguracionComercioRepository _repo;

        public ConfiguracionComercioBusiness(IConfiguracionComercioRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<ConfiguracionComercio>> ObtenerTodos()
        {
            return await _repo.ObtenerTodos();
        }

        public async Task<ConfiguracionComercio> ObtenerPorComercio(int id)
        {
            return await _repo.ObtenerPorComercio(id);
        }

        public async Task Crear(ConfiguracionComercio config)
        {
            await _repo.Crear(config);
        }

        public async Task Editar(ConfiguracionComercio config)
        {
            await _repo.Editar(config);
        }
    }
}