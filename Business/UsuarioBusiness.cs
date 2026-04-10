namespace MiProyectoMVC.Business;

using MiProyectoMVC.Models;
using MiProyectoMVC.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

public class UsuarioBusiness
{
    private readonly IUsuarioRepository _repo;

    public UsuarioBusiness(IUsuarioRepository repo)
    {
        _repo = repo;
    }

    public List<Usuario> Listar()
    {
        return _repo.ObtenerTodos();
    }

    public Usuario Obtener(int id)
    {
        return _repo.ObtenerPorId(id);
    }

    public void Crear(Usuario usuario)
    {
        if (_repo.ObtenerPorIdentificacion(usuario.Identificacion) != null)
            throw new Exception("Identificación repetida");

        _repo.Crear(usuario);
    }

    public void Editar(Usuario usuario)
    {
        var u = _repo.ObtenerPorId(usuario.IdUsuario);

        if (u == null)
            throw new Exception("Usuario no existe");

        _repo.Editar(usuario);
    }
}