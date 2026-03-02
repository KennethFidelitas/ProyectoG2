namespace MiProyectoMVC.Business;

using MiProyectoMVC.Models;
using MiProyectoMVC.Repositories;
using System;
using System.Text.Json;

public class SinpeBusiness
{
    private readonly ISinpeRepository _sinpeRepository;
    private readonly IBitacoraRepository _bitacoraRepository;

    public SinpeBusiness(ISinpeRepository sinpeRepository,
                         IBitacoraRepository bitacoraRepository)
    {
        _sinpeRepository = sinpeRepository;
        _bitacoraRepository = bitacoraRepository;
    }

    public void RegistrarSinpe(Sinpe sinpe)
    {
        try
        {
            if (sinpe.Monto <= 0)
                throw new Exception("El monto debe ser mayor a 0.");

            if (sinpe.TelefonoOrigen.Length != 8 ||
                sinpe.TelefonoDestinatario.Length != 8)
                throw new Exception("El teléfono debe tener 8 dígitos.");

            sinpe.FechaDeRegistro = DateTime.Now;
            sinpe.Estado = false;

            _sinpeRepository.Registrar(sinpe);

            RegistrarBitacora("Sinpes", "Registrar",
                "Registro exitoso",
                null,
                null,
                JsonSerializer.Serialize(sinpe));
        }
        catch (Exception ex)
        {
            RegistrarBitacora("Sinpes", "Error",
                ex.Message,
                ex.StackTrace,
                null,
                null);

            throw;
        }
    }

    public void EditarSinpe(Sinpe sinpe)
    {
        try
        {
            var anterior = _sinpeRepository.ObtenerPorId(sinpe.IdSinpe);

            _sinpeRepository.Editar(sinpe);

            RegistrarBitacora("Sinpes", "Editar",
                "Edición exitosa",
                null,
                JsonSerializer.Serialize(anterior),
                JsonSerializer.Serialize(sinpe));
        }
        catch (Exception ex)
        {
            RegistrarBitacora("Sinpes", "Error",
                ex.Message,
                ex.StackTrace,
                null,
                null);

            throw;
        }
    }

    public void EliminarSinpe(int id)
    {
        try
        {
            var anterior = _sinpeRepository.ObtenerPorId(id);

            _sinpeRepository.Eliminar(id);

            RegistrarBitacora("Sinpes", "Eliminar",
                "Eliminación exitosa",
                null,
                JsonSerializer.Serialize(anterior),
                null);
        }
        catch (Exception ex)
        {
            RegistrarBitacora("Sinpes", "Error",
                ex.Message,
                ex.StackTrace,
                null,
                null);

            throw;
        }
    }

    private void RegistrarBitacora(string tabla,
                                   string tipo,
                                   string descripcion,
                                   string? stackTrace,
                                   string? datosAnteriores,
                                   string? datosPosteriores)
    {
        var evento = new BitacoraEvento
        {
            TablaDeEvento = tabla,
            TipoDeEvento = tipo,
            FechaDeEvento = DateTime.Now,
            DescripcionDeEvento = descripcion,
            StackTrace = stackTrace ?? "",
            DatosAnteriores = datosAnteriores,
            DatosPosteriores = datosPosteriores
        };

        _bitacoraRepository.RegistrarEvento(evento);
    }
}