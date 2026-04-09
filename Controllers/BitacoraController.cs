using Microsoft.AspNetCore.Mvc;
using MiProyectoMVC.Repositories;
using System.Threading.Tasks;

namespace MiProyectoMVC.Controllers;

public class BitacoraController : Controller
{
    private readonly IBitacoraRepository _repository;

    public BitacoraController(IBitacoraRepository repository)
    {
        _repository = repository;
    }

    public async Task<IActionResult> Index()
    {
        var eventos = await _repository.ObtenerEventos();
        return View(eventos);
    }
}