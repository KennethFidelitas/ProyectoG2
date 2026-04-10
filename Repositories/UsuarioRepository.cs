using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using MiProyectoMVC.Models;

namespace MiProyectoMVC.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly HttpClient _http;

        public UsuarioRepository(HttpClient http)
        {
            _http = http;
            _http.BaseAddress = new Uri("https://demo2-api-8rhs.onrender.com/api/");
        }

        public List<Usuario> ObtenerTodos()
        {
            var lista = _http.GetFromJsonAsync<List<Usuario>>("usuarios").Result;
            return lista ?? new List<Usuario>();
        }

        public Usuario ObtenerPorId(int id)
        {
            var lista = _http.GetFromJsonAsync<List<Usuario>>("usuarios").Result;
            return lista?.FirstOrDefault(u => u.IdUsuario == id);
        }

        public Usuario ObtenerPorIdentificacion(string identificacion)
        {
            var lista = _http.GetFromJsonAsync<List<Usuario>>("usuarios").Result;
            return lista?.FirstOrDefault(u => u.Identificacion == identificacion);
        }

        public void Crear(Usuario usuario)
        {
            var json = new
            {
                comercio = new
                {
                    idComercio = usuario.IdComercio
                },
                nombres = usuario.Nombres,
                primerApellido = usuario.PrimerApellido,
                segundoApellido = usuario.SegundoApellido,
                identificacion = usuario.Identificacion,
                correoElectronico = usuario.CorreoElectronico,
                estado = usuario.Estado
            };

            var response = _http.PostAsJsonAsync("usuarios", json)
                                .GetAwaiter().GetResult();

            if (!response.IsSuccessStatusCode)
            {
                var error = response.Content.ReadAsStringAsync().Result;
                throw new Exception("Error API: " + error);
            }
        }

        public void Editar(Usuario usuario)
        {
            var json = new
            {
                idUsuario = usuario.IdUsuario,
                comercio = new
                {
                    idComercio = usuario.IdComercio
                },
                nombres = usuario.Nombres,
                primerApellido = usuario.PrimerApellido,
                segundoApellido = usuario.SegundoApellido,
                identificacion = usuario.Identificacion,
                correoElectronico = usuario.CorreoElectronico,
                estado = usuario.Estado
            };

            var response = _http.PutAsJsonAsync($"usuarios/{usuario.IdUsuario}", json)
                                .GetAwaiter().GetResult();

            if (!response.IsSuccessStatusCode)
            {
                var error = response.Content.ReadAsStringAsync().Result;
                throw new Exception("Error API: " + error);
            }
        }

        public void Eliminar(int id)
        {
            var response = _http.DeleteAsync($"usuarios/{id}")
                                .GetAwaiter().GetResult();

            if (!response.IsSuccessStatusCode)
            {
                var error = response.Content.ReadAsStringAsync().Result;
                throw new Exception("Error API: " + error);
            }
        }
    }
}