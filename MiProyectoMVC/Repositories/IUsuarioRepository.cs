using MiProyectoMVC.Models;

namespace MiProyectoMVC.Repositories
{
    public interface IUsuarioRepository
    {
        List<Usuario> GetAll();
        Usuario ObtenerPorId(int id);
        Usuario ObtenerPorIdentificacion(string identificacion);
        void Agregar(Usuario usuario);
        void Actualizar(Usuario usuario);
    }
}
