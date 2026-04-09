using MiProyectoMVC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiProyectoMVC.Repositories;

public interface IBitacoraRepository
{
    Task RegistrarEvento(BitacoraEvento evento);

    Task<List<BitacoraEvento>> ObtenerEventos();
}