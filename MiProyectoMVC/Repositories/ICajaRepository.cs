namespace MiProyectoMVC.Repositories;

using MiProyectoMVC.Models;
using System.Collections.Generic;

public interface ICajaRepository
{
    void Registrar(Caja caja);
    void Editar(Caja caja);
    void Eliminar(int id);
    void CerrarCaja(int id, decimal montoFinal);
    Caja? ObtenerPorId(int id);
    List<Caja> ObtenerTodos();
}