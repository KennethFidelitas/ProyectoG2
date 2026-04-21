using Microsoft.AspNetCore.Mvc;
using MiProyectoMVC.Business;
using MiProyectoMVC.Models;
using Microsoft.AspNetCore.Authorization;

namespace MiProyectoMVC.Controllers
{
    [Authorize(AuthenticationSchemes = "Cookies", Roles = "Administrador")]
    public class ConfiguracionComercioController : Controller
    {
        private readonly ConfiguracionComercioBusiness _business;
        private readonly ComercioBusiness _comercioBusiness;

        public ConfiguracionComercioController(
            ConfiguracionComercioBusiness business,
            ComercioBusiness comercioBusiness)
        {
            _business = business;
            _comercioBusiness = comercioBusiness;
        }

        // LISTAR
        public async Task<IActionResult> Index()
        {
            var lista = await _business.ObtenerTodos();

            // Traer nombres de comercios
            var comercios = await _comercioBusiness.ObtenerTodos();

            foreach (var item in lista)
            {
                var comercio = comercios.FirstOrDefault(c => c.IdComercio == item.IdComercio);
                item.NombreComercio = comercio?.Nombre ?? "N/A";
            }

            return View(lista);
        }

        //GET: CREAR
        public async Task<IActionResult> Crear()
        {
            ViewBag.Comercios = await _comercioBusiness.ObtenerTodos();
            return View();
        }

        //POST: CREAR
        [HttpPost]
        public async Task<IActionResult> Crear(ConfiguracionComercio config)
        {
            var existente = await _business.ObtenerPorComercio(config.IdComercio);

            if (existente != null)
            {
                TempData["Error"] = "Este comercio ya tiene una configuración registrada.";
                return RedirectToAction("Crear");
            }

            config.FechaDeRegistro = DateTime.Now;
            config.Estado = true;

            await _business.Crear(config);

            return RedirectToAction("Index");
        }

        //GET: EDITAR
        public async Task<IActionResult> Editar(int idComercio)
        {
            var config = await _business.ObtenerPorComercio(idComercio);

            if (config == null)
                return NotFound();

            return View(config);
        }

        //POST: EDITAR
        [HttpPost]
        public async Task<IActionResult> Editar(ConfiguracionComercio config)
        {
            config.FechaDeModificacion = DateTime.Now;

            await _business.Editar(config);

            return RedirectToAction("Index");
        }
    }
}