using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MiProyectoMVC.Models;
using MiProyectoMVC.Repositories;
using MiProyectoMVC.Services;
using System.Security.Claims;

namespace MiProyectoMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly AuthFileService _authService;
        private readonly IUsuarioRepository _usuarioRepository;

        public AccountController(AuthFileService authService, IUsuarioRepository usuarioRepository)
        {
            _authService = authService;
            _usuarioRepository = usuarioRepository;
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (!_authService.ValidarPassword(model.Correo, model.Contrasena))
            {
                ModelState.AddModelError("", "Correo o contraseña incorrectos.");
                return View(model);
            }

            var usuarioAuth = _authService.ObtenerPorCorreo(model.Correo)!;

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, model.Correo),
                new Claim(ClaimTypes.Role, usuarioAuth.Rol),
                new Claim("IdComercio", usuarioAuth.IdComercio.ToString())
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return RedirectToAction("Index", "Cajas");
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (_authService.ExisteCorreo(model.Correo))
            {
                ModelState.AddModelError("", "Este correo ya está registrado.");
                return View(model);
            }

            int idComercio = 0;

            if (model.Rol == "Cajero")
            {
                var usuarioApi = _usuarioRepository.ObtenerPorCorreo(model.Correo);
                if (usuarioApi == null || usuarioApi.IdUsuario == 0)
                {
                    ModelState.AddModelError("", "El correo no está registrado en el sistema. Contacte al administrador.");
                    return View(model);
                }
                idComercio = usuarioApi.IdComercio;
            }

            var nuevoUsuario = new UsuarioAuth
            {
                Correo = model.Correo,
                PasswordHash = AuthFileService.HashPassword(model.Contrasena),
                Rol = model.Rol,
                IdComercio = idComercio
            };

            _authService.Guardar(nuevoUsuario);

            TempData["Success"] = "Usuario registrado correctamente. Inicie sesión.";
            return RedirectToAction("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}