using System.Collections.Generic;
using MiProyectoMVC.Models;
using MiProyectoMVC.Repositories;

namespace MiProyectoMVC.Business
{
    public class SinpeBusiness
    {
        private readonly ISinpeRepository _repo;

        public SinpeBusiness(ISinpeRepository repo)
        {
            _repo = repo;
        }

        public void Registrar(Sinpe sinpe)
        {
            _repo.Registrar(sinpe);
        }

        public List<Sinpe> ObtenerTodos()
        {
            return _repo.ObtenerTodos();
        }

        public Sinpe ObtenerPorId(int id)
        {
            return _repo.ObtenerPorId(id);
        }

        public void Editar(Sinpe sinpe)
        {
            _repo.Editar(sinpe);
        }

        public void Eliminar(int id)
        {
            _repo.Eliminar(id);
        }

        public List<Sinpe> ObtenerPorCaja(int idCaja)
        {
            return _repo.ObtenerPorCaja(idCaja);
        }
    }
}