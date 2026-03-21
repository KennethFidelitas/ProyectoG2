namespace MiProyectoMVC.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MiProyectoMVC.Business;
using MiProyectoMVC.Models;
using MiProyectoMVC.Repositories;
using System;

public class CajasController : Controller
{
    private readonly CajaBusiness _business;
    private readonly ISinpeRepository _sinpeRepository;

    public CajasController(CajaBusiness business, ISinpeRepository sinpeRepository)
    {
        _business = business;
        _sinpeRepository = sinpeRepository;
    }


    public IActionResult Index()
    {
        var cajas = _business.ObtenerTodos();
        return View(cajas);
    }


    public IActionResult Create()
    {
        CargarDropdownComercios();
        return View();
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Caja caja)
    {
        try
        {
            _business.AbrirCaja(caja);
            TempData["Success"] = "Caja abierta correctamente.";
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            CargarDropdownComercios();
            return View(caja);
        }
    }

   
    public IActionResult Edit(int id)
    {
        var caja = _business.ObtenerPorId(id);
        if (caja == null) return NotFound();

        CargarDropdownComercios(caja.ComercioId);
        return View(caja);
    }

  
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Caja caja)
    {
        try
        {
            _business.EditarCaja(caja);
            TempData["Success"] = "Caja actualizada correctamente.";
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", ex.Message);
            CargarDropdownComercios(caja.ComercioId);
            return View(caja);
        }
    }

   
    public IActionResult Cerrar(int id)
    {
        var caja = _business.ObtenerPorId(id);
        if (caja == null) return NotFound();

        return View(caja);
    }

    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult CerrarConfirmado(int idCaja, decimal montoFinal)
    {
        try
        {
            _business.CerrarCaja(idCaja, montoFinal);
            TempData["Success"] = "Caja cerrada correctamente.";
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return RedirectToAction("Cerrar", new { id = idCaja });
        }
    }

  
    public IActionResult Delete(int id)
    {
        var caja = _business.ObtenerPorId(id);
        if (caja == null) return NotFound();

        return View(caja);
    }

    
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmado(int id)
    {
        try
        {
            _business.EliminarCaja(id);
            TempData["Success"] = "Caja eliminada correctamente.";
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return RedirectToAction("Delete", new { id });
        }
    }

    private void CargarDropdownComercios(int? seleccionado = null)
    {
        var comercios = _business.ObtenerComercios();
        ViewBag.Comercios = new SelectList(comercios, "IdComercio", "Nombre", seleccionado);
    }

 

    [HttpGet]
    public IActionResult ObtenerSinpes(int idCaja)
    {
        var sinpes = _sinpeRepository.ObtenerPorCaja(idCaja);
        return Json(sinpes);
    }
}