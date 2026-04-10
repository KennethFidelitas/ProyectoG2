using System.Collections.Generic;
using System.Text.Json;
using System;
using MiProyectoMVC.Models;
using MiProyectoMVC.Repositories;

namespace MiProyectoMVC.Business
{
    public class SinpeBusiness
    {
        private readonly ISinpeRepository _repo;
        private readonly IBitacoraRepository _bitacoraRepository;

        public SinpeBusiness(ISinpeRepository repo, IBitacoraRepository bitacoraRepository)
        {
            _repo = repo;
            _bitacoraRepository = bitacoraRepository;
        }

        public void Registrar(Sinpe sinpe)
        {
            _repo.Registrar(sinpe);
        }

        public List<Sinpe> ObtenerTodos()
        {
            return _repo.ObtenerTodos();
        }

        public Sinpe ObtenerPorId(int id)
        {
            return _repo.ObtenerPorId(id);
        }

        public void Editar(Sinpe sinpe)
        {
            _repo.Editar(sinpe);
        }

        public void Eliminar(int id)
        {
            _repo.Eliminar(id);
        }

        public List<Sinpe> ObtenerPorCaja(int idCaja)
        {
            return _repo.ObtenerPorCaja(idCaja);
        }

        public void SincronizarSinpe(int id)
        {
            try
            {
                var sinpe = _repo.ObtenerPorId(id)
                    ?? throw new Exception($"SINPE con ID {id} no encontrado.");

                if (sinpe.Estado)
                    throw new Exception("El SINPE ya se encuentra sincronizado.");

                _repo.SincronizarSinpe(id);

                RegistrarBitacora(
                    "Sinpes",
                    "Sincronizar",
                    $"SINPE ID {id} sincronizado correctamente.",
                    null,
                    JsonSerializer.Serialize(sinpe),
                    null);
            }
            catch (Exception ex)
            {
                RegistrarBitacora("Sinpes", "Error", ex.Message, ex.StackTrace, null, null);
                throw;
            }
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
}