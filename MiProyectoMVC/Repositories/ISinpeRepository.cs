namespace MiProyectoMVC.Repositories;

using MiProyectoMVC.Models;
using System.Collections.Generic;

public interface ISinpeRepository
{
    void Registrar(Sinpe sinpe);
    void Editar(Sinpe sinpe);
    void Eliminar(int id);
    Sinpe? ObtenerPorId(int id);
    List<Sinpe> ObtenerTodos();
    List<Sinpe> ObtenerPorCaja(int idCaja);

    Comercio? ObtenerComercioPorTelefono(string telefono);
    Caja? ObtenerCajaAbierta(int comercioId);
    void AgregarMontoACaja(int idCaja, decimal monto);
}