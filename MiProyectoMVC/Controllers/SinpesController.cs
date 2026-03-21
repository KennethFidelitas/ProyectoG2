namespace MiProyectoMVC.Controllers;

using Microsoft.AspNetCore.Mvc;
using MiProyectoMVC.Business;
using MiProyectoMVC.Models;

public class SinpesController : Controller
{
    private readonly SinpeBusiness _business;

    public SinpesController(SinpeBusiness business)
    {
        _business = business;
    }

    // GET
    public IActionResult Create()
    {
        return View();
    }

    // POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Sinpe sinpe)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return View(sinpe);
            }

            _business.RegistrarSinpe(sinpe);

            TempData["Success"] = "SINPE realizado correctamente.";

            return RedirectToAction("Create");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View(sinpe);
        }
    }
}