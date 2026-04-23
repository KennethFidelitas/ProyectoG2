using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using MiProyectoMVC.Models;

namespace MiProyectoMVC.Services
{
    public class AuthApiService
    {
        private readonly HttpClient _http;

        public AuthApiService(IHttpClientFactory httpFactory)
        {
            _http = httpFactory.CreateClient("API");
        }

        public UsuarioAuth? ObtenerPorCorreo(string correo)
{
    try
    {
        var lista = _http.GetFromJsonAsync<List<UsuarioAuth>>("authusuarios").Result;
        return lista?.FirstOrDefault(u =>
            u.Correo.Equals(correo, StringComparison.OrdinalIgnoreCase));
    }
    catch
    {
        return null;
    }
}

        public bool ExisteCorreo(string correo) => ObtenerPorCorreo(correo) != null;

        public void Guardar(UsuarioAuth usuario)
        {
            var response = _http.PostAsJsonAsync("authusuarios", usuario).GetAwaiter().GetResult();
            if (!response.IsSuccessStatusCode)
            {
                var error = response.Content.ReadAsStringAsync().Result;
                throw new Exception("Error al registrar: " + error);
            }
        }

        public bool ValidarPassword(string correo, string password)
        {
            var usuario = ObtenerPorCorreo(correo);
            if (usuario == null)
            {
                Console.WriteLine("DEBUG: Usuario no encontrado");
                return false;
            }

            var hashIngresado = HashPassword(password);
            Console.WriteLine($"DEBUG Hash almacenado:  {usuario.PasswordHash}");
            Console.WriteLine($"DEBUG Hash ingresado:   {hashIngresado}");

            return usuario.PasswordHash == hashIngresado;
        }

        public static string HashPassword(string password)
        {
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }
}