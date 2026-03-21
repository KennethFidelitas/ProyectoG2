namespace MiProyectoMVC.Business;

using MiProyectoMVC.Models;
using MiProyectoMVC.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

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

    public void AbrirCaja(Caja caja)
    {
        try
        {
            var comercio = _comercioRepository.ObtenerPorId(caja.ComercioId)
                ?? throw new Exception("El comercio seleccionado no existe.");

            if (!comercio.Activo)
                throw new Exception("El comercio seleccionado no está activo.");

            var todas = _cajaRepository.ObtenerTodos();

            bool yaAbierta = todas.Any(c => c.ComercioId == caja.ComercioId && c.EstaAbierta);

            if (yaAbierta)
                throw new Exception("Ya existe una caja abierta para ese comercio.");

            caja.FechaApertura = DateTime.Now;
            caja.EstaAbierta = true;

            _cajaRepository.Registrar(caja);

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

    public void CerrarCaja(int id, decimal montoFinal)
    {
        try
        {
            var anterior = _cajaRepository.ObtenerPorId(id)
                ?? throw new Exception("La caja no existe.");

            if (!anterior.EstaAbierta)
                throw new Exception("La caja ya está cerrada.");

            _cajaRepository.CerrarCaja(id, montoFinal);

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

    public void EditarCaja(Caja caja)
    {
        try
        {
            var comercio = _comercioRepository.ObtenerPorId(caja.ComercioId)
                ?? throw new Exception("El comercio seleccionado no existe.");

            var anterior = _cajaRepository.ObtenerPorId(caja.IdCaja);

            _cajaRepository.Editar(caja);

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

    public void EliminarCaja(int id)
    {
        try
        {
            var anterior = _cajaRepository.ObtenerPorId(id)
                ?? throw new Exception("La caja no existe.");

            if (anterior.EstaAbierta)
                throw new Exception("No se puede eliminar una caja que está abierta. Ciérrela primero.");

            _cajaRepository.Eliminar(id);

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

    public List<Caja> ObtenerTodos() => _cajaRepository.ObtenerTodos();

    public Caja? ObtenerPorId(int id) => _cajaRepository.ObtenerPorId(id);

    public List<Comercio> ObtenerComercios() => _comercioRepository.ObtenerTodos();

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