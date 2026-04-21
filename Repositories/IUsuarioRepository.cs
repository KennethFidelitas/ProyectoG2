using System.Collections.Generic;
using MiProyectoMVC.Models;

namespace MiProyectoMVC.Repositories
{
    public interface IUsuarioRepository
    {
        List<Usuario> ObtenerTodos();
        Usuario ObtenerPorId(int id);
        Usuario ObtenerPorIdentificacion(string identificacion);
        Usuario ObtenerPorCorreo(string correo);
        void Crear(Usuario usuario);
        void Editar(Usuario usuario);
        void Eliminar(int id);
        void ActualizarIdNetUser(int idUsuario, string idNetUser);
    }
}