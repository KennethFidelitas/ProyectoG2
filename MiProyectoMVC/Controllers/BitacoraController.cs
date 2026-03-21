using Microsoft.AspNetCore.Mvc;
using MiProyectoMVC.Repositories;

namespace MiProyectoMVC.Controllers;

public class BitacoraController : Controller
{
    private readonly IBitacoraRepository _repository;

    public BitacoraController(IBitacoraRepository repository)
    {
        _repository = repository;
    }

    public IActionResult Index()
    {
        var eventos = _repository.ObtenerEventos();
        return View(eventos);
    }
}