using Microsoft.AspNetCore.Mvc;
using MiProyectoMVC.Models;
using MiProyectoMVC.Business;

namespace MiProyectoMVC.Controllers
{
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
        public IActionResult Create(Sinpe model)
        {
            if (ModelState.IsValid)
            {
                _business.Registrar(model); // ✅ CORREGIDO

                TempData["Success"] = "Pago SINPE registrado correctamente";
                return RedirectToAction("Create");
            }

            return View(model);
        }
    }
}