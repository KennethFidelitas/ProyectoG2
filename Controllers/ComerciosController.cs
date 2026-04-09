using Microsoft.AspNetCore.Mvc;
using MiProyectoMVC.Business;
using MiProyectoMVC.Models;
using System.Threading.Tasks;

namespace MiProyectoMVC.Controllers
{
    public class ComerciosController : Controller
    {
        private readonly ComercioBusiness _business;

        public ComerciosController(ComercioBusiness business)
        {
            _business = business;
        }

        public async Task<IActionResult> Index()
        {
            var lista = await _business.ObtenerTodos();
            return View(lista);
        }

        public async Task<IActionResult> Details(int id)
        {
            var comercio = await _business.ObtenerPorId(id);

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
        public async Task<IActionResult> Create(Comercio comercio)
        {
            if (!ModelState.IsValid)
                return View(comercio);

            await _business.CrearComercio(comercio);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var comercio = await _business.ObtenerPorId(id);

            if (comercio == null)
                return NotFound();

            return View(comercio);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Comercio comercio)
        {
            if (!ModelState.IsValid)
                return View(comercio);

            await _business.EditarComercio(comercio);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var comercio = await _business.ObtenerPorId(id);

            if (comercio == null)
                return NotFound();

            return View(comercio);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int IdComercio)
        {
            await _business.EliminarComercio(IdComercio);
            return RedirectToAction(nameof(Index));
        }
    }
}