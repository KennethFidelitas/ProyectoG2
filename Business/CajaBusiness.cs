namespace MiProyectoMVC.Business;

using MiProyectoMVC.Models;
using MiProyectoMVC.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

public class CajaBusiness
{
    private readonly ICajaRepository _cajaRepository;
    private readonly IComercioRepository _comercioRepository;
    private readonly IBitacoraRepository _bitacoraRepository;

    public CajaBusiness(
        ICajaRepository cajaRepository,
        IComercioRepository comercioRepository,
        IBitacoraRepository bitacoraRepository)
    {
        _cajaRepository = cajaRepository;
        _comercioRepository = comercioRepository;
        _bitacoraRepository = bitacoraRepository;
    }

    public async Task AbrirCaja(Caja caja)
    {
        try
        {
            var comercio = await _comercioRepository.ObtenerPorId(caja.ComercioId)
                ?? throw new Exception("El comercio seleccionado no existe.");

            if (!comercio.Activo)
                throw new Exception("El comercio seleccionado no está activo.");

            var todas = await _cajaRepository.ObtenerTodos();

            bool yaAbierta = todas.Any(c => c.ComercioId == caja.ComercioId && c.EstaAbierta);

            if (yaAbierta)
                throw new Exception("Ya existe una caja abierta para ese comercio.");

            caja.FechaApertura = DateTime.Now;
            caja.EstaAbierta = true;

            await _cajaRepository.Registrar(caja);

            RegistrarBitacora(
                "Cajas",
                "Apertura",
                "Caja abierta correctamente",
                null,
                null,
                JsonSerializer.Serialize(caja));
        }
        catch (Exception ex)
        {
            RegistrarBitacora("Cajas", "Error", ex.Message, ex.StackTrace, null, null);
            throw;
        }
    }

    public async Task CerrarCaja(int id, decimal montoFinal)
    {
        try
        {
            var anterior = await _cajaRepository.ObtenerPorId(id)
                ?? throw new Exception("La caja no existe.");

            if (!anterior.EstaAbierta)
                throw new Exception("La caja ya está cerrada.");

            await _cajaRepository.CerrarCaja(id, montoFinal);

            RegistrarBitacora(
                "Cajas",
                "Cierre",
                "Caja cerrada correctamente",
                null,
                JsonSerializer.Serialize(anterior),
                null);
        }
        catch (Exception ex)
        {
            RegistrarBitacora("Cajas", "Error", ex.Message, ex.StackTrace, null, null);
            throw;
        }
    }

    public async Task EditarCaja(Caja caja)
    {
        try
        {
            var comercio = await _comercioRepository.ObtenerPorId(caja.ComercioId)
                ?? throw new Exception("El comercio seleccionado no existe.");

            var anterior = await _cajaRepository.ObtenerPorId(caja.IdCaja);

            await _cajaRepository.Editar(caja);

            RegistrarBitacora(
                "Cajas",
                "Editar",
                "Caja editada correctamente",
                null,
                JsonSerializer.Serialize(anterior),
                JsonSerializer.Serialize(caja));
        }
        catch (Exception ex)
        {
            RegistrarBitacora("Cajas", "Error", ex.Message, ex.StackTrace, null, null);
            throw;
        }
    }

    public async Task EliminarCaja(int id)
    {
        try
        {
            var anterior = await _cajaRepository.ObtenerPorId(id)
                ?? throw new Exception("La caja no existe.");

            if (anterior.EstaAbierta)
                throw new Exception("No se puede eliminar una caja que está abierta. Ciérrela primero.");

            await _cajaRepository.Eliminar(id);

            RegistrarBitacora(
                "Cajas",
                "Eliminación",
                "Caja eliminada correctamente",
                null,
                JsonSerializer.Serialize(anterior),
                null);
        }
        catch (Exception ex)
        {
            RegistrarBitacora("Cajas", "Error", ex.Message, ex.StackTrace, null, null);
            throw;
        }
    }

    public async Task<List<Caja>> ObtenerTodos()
{
    var cajas = await _cajaRepository.ObtenerTodos();
    var comercios = await _comercioRepository.ObtenerTodos();

    foreach (var caja in cajas)
    {
        var comercio = comercios.FirstOrDefault(c => c.IdComercio == caja.ComercioId);

        if (comercio != null)
        {
            caja.NombreComercio = comercio.Nombre;
        }
        else
        {
            caja.NombreComercio = "Sin comercio";
        }
    }

    return cajas;
}

    public async Task<Caja?> ObtenerPorId(int id)
    {
        return await _cajaRepository.ObtenerPorId(id);
    }

    public async Task<List<Comercio>> ObtenerComercios()
    {
        return await _comercioRepository.ObtenerTodos();
    }

    private void RegistrarBitacora(
        string tabla,
        string tipo,
        string descripcion,
        string? stackTrace,
        string? datosAnteriores,
        string? datosPosteriores)
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