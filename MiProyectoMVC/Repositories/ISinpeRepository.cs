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
}