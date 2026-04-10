namespace MiProyectoMVC.Business;

using MiProyectoMVC.Models;
using MiProyectoMVC.Repositories;
using System;
using System.Collections.Generic;
using System.Text.Json;

public class UsuarioBusiness
{
    private readonly IUsuarioRepository _repo;
    private readonly IBitacoraRepository _bitacoraRepository;

    public UsuarioBusiness(IUsuarioRepository repo, IBitacoraRepository bitacoraRepository)
    {
        _repo = repo;
        _bitacoraRepository = bitacoraRepository;
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
        try
        {
            if (_repo.ObtenerPorIdentificacion(usuario.Identificacion) != null)
                throw new Exception("Ya existe un usuario con esa identificación.");

            _repo.Crear(usuario);

            RegistrarBitacora("Usuarios", "Registrar",
                $"Usuario '{usuario.Nombres}' registrado correctamente.",
                null, null, JsonSerializer.Serialize(usuario));
        }
        catch (Exception ex)
        {
            RegistrarBitacora("Usuarios", "Error", ex.Message, ex.StackTrace, null, null);
            throw;
        }
    }

    public void Editar(Usuario usuario)
    {
        try
        {
            var anterior = _repo.ObtenerPorId(usuario.IdUsuario)
                ?? throw new Exception("Usuario no existe.");

            _repo.Editar(usuario);

            RegistrarBitacora("Usuarios", "Editar",
                $"Usuario ID {usuario.IdUsuario} editado correctamente.",
                null, JsonSerializer.Serialize(anterior), JsonSerializer.Serialize(usuario));
        }
        catch (Exception ex)
        {
            RegistrarBitacora("Usuarios", "Error", ex.Message, ex.StackTrace, null, null);
            throw;
        }
    }

    private void RegistrarBitacora(
        string tabla, string tipo, string descripcion,
        string? stackTrace, string? datosAnteriores, string? datosPosteriores)
    {
        _bitacoraRepository.RegistrarEvento(new BitacoraEvento
        {
            TablaDeEvento = tabla,
            TipoDeEvento = tipo,
            FechaDeEvento = DateTime.Now,
            DescripcionDeEvento = descripcion,
            StackTrace = stackTrace ?? "",
            DatosAnteriores = datosAnteriores,
            DatosPosteriores = datosPosteriores
        });
    }
}