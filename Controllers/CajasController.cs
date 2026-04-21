using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MiProyectoMVC.Business;
using MiProyectoMVC.Models;
using MiProyectoMVC.Repositories;
using System;
using System.Threading.Tasks;


namespace MiProyectoMVC.Controllers
{
    [Authorize(AuthenticationSchemes = "Cookies", Roles = "Administrador,Cajero")]
    public class CajasController : Controller
    {
        private readonly CajaBusiness _business;
        private readonly ISinpeRepository _sinpeRepository;
        private readonly SinpeBusiness _sinpeBusiness;

        public CajasController(CajaBusiness business, ISinpeRepository sinpeRepository, SinpeBusiness sinpeBusiness)
        {
            _business = business;
            _sinpeRepository = sinpeRepository;
            _sinpeBusiness = sinpeBusiness;
        }

       
        public async Task<IActionResult> Index()
        {
            var cajas = await _business.ObtenerTodos();
            return View(cajas);
        }

    
        public async Task<IActionResult> Create()
        {
            await CargarDropdownComercios();

            // 🔥 EVITA NULL
            return View(new Caja
            {
                FechaApertura = DateTime.Now,
                EstaAbierta = true
            });
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Caja caja)
        {
            if (!ModelState.IsValid)
            {
                await CargarDropdownComercios(caja?.ComercioId);
                return View(caja);
            }

            try
            {
                await _business.AbrirCaja(caja);
                TempData["Success"] = "Caja abierta correctamente.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                await CargarDropdownComercios(caja?.ComercioId);
                return View(caja);
            }
        }

       
        public async Task<IActionResult> Edit(int id)
        {
            var caja = await _business.ObtenerPorId(id);

            if (caja == null)
                return NotFound();

            await CargarDropdownComercios(caja.ComercioId);
            return View(caja);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Caja caja)
        {
            if (!ModelState.IsValid)
            {
                await CargarDropdownComercios(caja?.ComercioId);
                return View(caja);
            }

            try
            {
                await _business.EditarCaja(caja);
                TempData["Success"] = "Caja actualizada correctamente.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                await CargarDropdownComercios(caja?.ComercioId);
                return View(caja);
            }
        }

        
        public async Task<IActionResult> Cerrar(int id)
        {
            var caja = await _business.ObtenerPorId(id);

            if (caja == null)
                return NotFound();

            return View(caja);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CerrarConfirmado(int idCaja, decimal montoFinal)
        {
            try
            {
                await _business.CerrarCaja(idCaja, montoFinal);
                TempData["Success"] = "Caja cerrada correctamente.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Cerrar", new { id = idCaja });
            }
        }

      
        public async Task<IActionResult> Delete(int id)
        {
            var caja = await _business.ObtenerPorId(id);

            if (caja == null)
                return NotFound();

            return View(caja);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmado(int id)
        {
            try
            {
                await _business.EliminarCaja(id);
                TempData["Success"] = "Caja eliminada correctamente.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Delete", new { id });
            }
        }

        
        private async Task CargarDropdownComercios(int? seleccionado = null)
        {
            var comercios = await _business.ObtenerComercios();

            ViewBag.Comercios = new SelectList(
                comercios ?? new List<Comercio>(),
                "IdComercio",
                "Nombre",
                seleccionado
            );
        }

        
        [HttpGet]
public IActionResult ObtenerSinpes(int idCaja)
{
    var sinpes = _sinpeRepository.ObtenerPorCaja(idCaja);

    return new JsonResult(sinpes, new System.Text.Json.JsonSerializerOptions
    {
        PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase
    });
}

[HttpPost]
public IActionResult SincronizarSinpe(int idSinpe)
{
    try
    {
        _sinpeBusiness.SincronizarSinpe(idSinpe);
        return Json(new { success = true, mensaje = "SINPE sincronizado correctamente." });
    }
    catch (Exception ex)
    {
        return Json(new { success = false, mensaje = ex.Message });
    }
}
    }
}