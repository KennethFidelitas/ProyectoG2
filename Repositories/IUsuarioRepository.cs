namespace MiProyectoMVC.Repositories;

using MiProyectoMVC.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

public interface IUsuarioRepository
{
    List<Usuario> ObtenerTodos();
    Usuario ObtenerPorId(int id);
    Usuario ObtenerPorIdentificacion(string identificacion);
    void Crear(Usuario usuario);
    void Editar(Usuario usuario);
    void Eliminar(int id);
}