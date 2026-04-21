using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using MiProyectoMVC.Models;

namespace MiProyectoMVC.Services
{
    public class AuthFileService
    {
        private readonly string _filePath;

        public AuthFileService(IWebHostEnvironment env)
        {
            _filePath = Path.Combine(env.ContentRootPath, "auth_usuarios.json");
            if (!File.Exists(_filePath))
                File.WriteAllText(_filePath, "[]");
        }

        public List<UsuarioAuth> ObtenerTodos()
        {
            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<UsuarioAuth>>(json) ?? new List<UsuarioAuth>();
        }

        public UsuarioAuth? ObtenerPorCorreo(string correo)
        {
            return ObtenerTodos().FirstOrDefault(u =>
                u.Correo.Equals(correo, StringComparison.OrdinalIgnoreCase));
        }

        public bool ExisteCorreo(string correo) => ObtenerPorCorreo(correo) != null;

        public void Guardar(UsuarioAuth usuario)
        {
            var lista = ObtenerTodos();
            lista.Add(usuario);
            File.WriteAllText(_filePath, JsonSerializer.Serialize(lista,
                new JsonSerializerOptions { WriteIndented = true }));
        }

        public bool ValidarPassword(string correo, string password)
        {
            var usuario = ObtenerPorCorreo(correo);
            if (usuario == null) return false;
            return usuario.PasswordHash == HashPassword(password);
        }

        public static string HashPassword(string password)
        {
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }
}