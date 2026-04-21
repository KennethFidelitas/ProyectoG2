using Microsoft.AspNetCore.Mvc;
using MiProyectoMVC.Models;
using MiProyectoMVC.Business;
using Microsoft.AspNetCore.Authorization;

namespace MiProyectoMVC.Controllers
{
    [Authorize(AuthenticationSchemes = "Cookies", Roles = "Administrador")]
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
                _business.Registrar(model);

                TempData["Success"] = "Pago SINPE registrado correctamente";
                return RedirectToAction("Create");
            }

            return View(model);
        }
    }
}