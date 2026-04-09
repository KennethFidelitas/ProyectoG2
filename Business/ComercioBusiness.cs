namespace MiProyectoMVC.Business;

using MiProyectoMVC.Models;
using MiProyectoMVC.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

    public async Task<List<Comercio>> ObtenerTodos()
    {
        return await _repository.ObtenerTodos();
    }

    public async Task<Comercio?> ObtenerPorId(int id)
    {
        return await _repository.ObtenerPorId(id);
    }

    public async Task CrearComercio(Comercio comercio)
    {
        if (string.IsNullOrWhiteSpace(comercio.Nombre))
            throw new Exception("El nombre es obligatorio.");

        await _repository.Registrar(comercio);

        RegistrarBitacora("Comercios", "Registrar", $"Comercio creado: {comercio.Nombre}");
    }

    public async Task EditarComercio(Comercio comercio)
    {
        await _repository.Editar(comercio);

        RegistrarBitacora("Comercios", "Editar", $"Comercio editado: {comercio.Nombre}");
    }

    public async Task EliminarComercio(int id)
    {
        await _repository.Eliminar(id);

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