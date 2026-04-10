using Microsoft.AspNetCore.Mvc;
using MiProyectoMVC.Models;
using MiProyectoMVC.Business;

namespace MiProyectoMVC.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly UsuarioBusiness _business;

        public UsuariosController(UsuarioBusiness business)
        {
            _business = business;
        }

        // LISTAR
        public IActionResult Index()
        {
            var lista = _business.Listar();
            return View(lista);
        }

        // CREAR (GET)
        public IActionResult Create()
        {
            return View();
        }

        // CREAR (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Usuario model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Error = "Datos inválidos";
                return View(model);
            }

            try
            {
                _business.Crear(model);
                TempData["Success"] = "Usuario creado correctamente";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(model);
            }
        }

        // EDITAR (GET)
        public IActionResult Edit(int id)
        {
            var usuario = _business.Obtener(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // EDITAR (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Usuario model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Error = "Datos inválidos";
                return View(model);
            }

            try
            {
                _business.Editar(model);
                TempData["Success"] = "Usuario actualizado correctamente";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(model);
            }
        }
    }
}