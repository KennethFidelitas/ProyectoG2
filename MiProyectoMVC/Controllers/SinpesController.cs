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

    
    public IActionResult Create()
    {
        return View();
    }

    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Sinpe sinpe)
    {
        try
        {
            _business.RegistrarSinpe(sinpe);

            TempData["Success"] = "Pago SINPE registrado correctamente.";

            return RedirectToAction("Create");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            return View(sinpe);
        }
    }
}