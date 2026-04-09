using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using MiProyectoMVC.Models;

namespace MiProyectoMVC.Repositories
{
    public class SinpeRepository : ISinpeRepository
    {
        private readonly HttpClient _http;

        public SinpeRepository(HttpClient http)
        {
            _http = http;
            _http.BaseAddress = new Uri("https://demo2-api-8rhs.onrender.com/api/");
        }

        public void Registrar(Sinpe sinpe)
{
    sinpe.FechaDeRegistro = DateTime.UtcNow;
    var response = _http.PostAsJsonAsync("sinpes", sinpe).GetAwaiter().GetResult();
    response.EnsureSuccessStatusCode();
}

        public void Editar(Sinpe sinpe)
        {
            _ = _http.PutAsJsonAsync($"sinpes/{sinpe.IdSinpe}", sinpe);
        }

        public void Eliminar(int id)
        {
            _ = _http.DeleteAsync($"sinpes/{id}");
        }

        public Sinpe ObtenerPorId(int id)
        {
            var lista = _http.GetFromJsonAsync<List<Sinpe>>("sinpes").Result;
            return lista?.FirstOrDefault(x => x.IdSinpe == id);
        }

        public List<Sinpe> ObtenerTodos()
        {
            var lista = _http.GetFromJsonAsync<List<Sinpe>>("sinpes").Result;
            return lista ?? new List<Sinpe>();
        }

       public List<Sinpe> ObtenerPorCaja(int idCaja)
{
    var cajas = _http.GetFromJsonAsync<List<Caja>>("cajas").Result;
    var caja = cajas?.FirstOrDefault(c => c.IdCaja == idCaja);

    if (caja == null)
        return new List<Sinpe>();

    var telefonoCaja = caja.Comercio?.Telefono;

    var sinpes = _http.GetFromJsonAsync<List<Sinpe>>("sinpes").Result;

    return sinpes?
        .Where(s => s.TelefonoDestinatario == telefonoCaja)
        .ToList() ?? new List<Sinpe>();
}

        public Comercio ObtenerComercioPorTelefono(string telefono)
        {
            var comercios = _http.GetFromJsonAsync<List<Comercio>>("comercios").Result;
            return comercios?.FirstOrDefault(c => c.Telefono == telefono);
        }

        public Caja ObtenerCajaAbierta(int comercioId)
        {
            var cajas = _http.GetFromJsonAsync<List<Caja>>("cajas").Result;
            return cajas?.FirstOrDefault(c => c.Comercio.IdComercio == comercioId && c.EstaAbierta);
        }

        public void AgregarMontoACaja(int idCaja, decimal monto)
        {
            var caja = ObtenerCajaAbierta(idCaja);
            if (caja != null)
            {
                caja.MontoFinal += monto;
                _ = _http.PutAsJsonAsync($"cajas/{caja.IdCaja}", caja);
            }
        }
    }
}