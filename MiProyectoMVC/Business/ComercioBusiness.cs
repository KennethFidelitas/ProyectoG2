namespace MiProyectoMVC.Business;

using MiProyectoMVC.Models;
using MiProyectoMVC.Repositories;
using System;
using System.Collections.Generic;

public class ComercioBusiness
{
    private readonly IComercioRepository _repository;
    private readonly IBitacoraRepository _bitacoraRepository;

    public ComercioBusiness(
        IComercioRepository repository,
        IBitacoraRepository bitacoraRepository)
    {
        _repository = repository;
        _bitacoraRepository = bitacoraRepository;
    }

    public List<Comercio> ObtenerTodos()
    {
        return _repository.ObtenerTodos();
    }

    public Comercio? ObtenerPorId(int id)
    {
        return _repository.ObtenerPorId(id);
    }

    public void CrearComercio(Comercio comercio)
    {
        if (string.IsNullOrWhiteSpace(comercio.Nombre))
            throw new Exception("El nombre es obligatorio.");

        _repository.Registrar(comercio);

        RegistrarBitacora("Comercios", "Registrar", $"Comercio creado: {comercio.Nombre}");
    }

    public void EditarComercio(Comercio comercio)
    {
        _repository.Editar(comercio);

        RegistrarBitacora("Comercios", "Editar", $"Comercio editado: {comercio.Nombre}");
    }

    public void EliminarComercio(int id)
    {
        _repository.Eliminar(id);

        RegistrarBitacora("Comercios", "Eliminar", $"Comercio eliminado ID: {id}");
    }

    private void RegistrarBitacora(string tabla, string tipo, string descripcion)
    {
        _bitacoraRepository.RegistrarEvento(new BitacoraEvento
        {
            TablaDeEvento = tabla,
            TipoDeEvento = tipo,
            FechaDeEvento = DateTime.Now,
            DescripcionDeEvento = descripcion,
            StackTrace = "",
            DatosAnteriores = null,
            DatosPosteriores = null
        });
    }
}