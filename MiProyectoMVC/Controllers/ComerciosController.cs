using Microsoft.AspNetCore.Mvc;
using MiProyectoMVC.Business;
using MiProyectoMVC.Models;

namespace MiProyectoMVC.Controllers
{
    public class ComerciosController : Controller
    {
        private readonly ComercioBusiness _business;

        public ComerciosController(ComercioBusiness business)
        {
            _business = business;
        }

        public IActionResult Index()
        {
            var lista = _business.ObtenerTodos();
            return View(lista);
        }

        public IActionResult Details(int id)
        {
            var comercio = _business.ObtenerPorId(id);

            if (comercio == null)
                return NotFound();

            return View(comercio);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Comercio comercio)
        {
            if (!ModelState.IsValid)
                return View(comercio);

            _business.CrearComercio(comercio);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            var comercio = _business.ObtenerPorId(id);

            if (comercio == null)
                return NotFound();

            return View(comercio);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Comercio comercio)
        {
            if (!ModelState.IsValid)
                return View(comercio);

            _business.EditarComercio(comercio);
            return RedirectToAction(nameof(Index));
        }
public IActionResult Delete(int id)
{
    var comercio = _business.ObtenerPorId(id);

    if (comercio == null)
        return NotFound();

    return View(comercio);
}

[HttpPost]
[ValidateAntiForgeryToken]
[ActionName("Delete")]
public IActionResult DeleteConfirmed(int IdComercio)
{
    _business.EliminarComercio(IdComercio);
    return RedirectToAction(nameof(Index));
}
    }
}