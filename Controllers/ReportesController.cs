using Microsoft.AspNetCore.Mvc;
using MiProyectoMVC.Business;
using MiProyectoMVC.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace MiProyectoMVC.Controllers
{
    [Authorize(AuthenticationSchemes = "Cookies", Roles = "Administrador")]
    public class ReportesController : Controller
    {
        private readonly ReporteBusiness _business;

        public ReportesController(ReporteBusiness business)
        {
            _business = business;
        }

        // GET: Listar reportes
        public IActionResult Index()
        {
            var reportes = _business.ObtenerTodos();
            return View(reportes);
        }

        // POST: Generar reportes
        [HttpPost]
        public IActionResult Generar()
        {
            var reportes = _business.GenerarReportes();
            TempData["Success"] = "Reportes generados correctamente.";
            return RedirectToAction("Index");
        }
    }
}