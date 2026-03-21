using MiProyectoMVC.Models;
using System.Collections.Generic;

namespace MiProyectoMVC.Repositories;

public interface IBitacoraRepository
{
    void RegistrarEvento(BitacoraEvento evento);

    List<BitacoraEvento> ObtenerEventos();
}