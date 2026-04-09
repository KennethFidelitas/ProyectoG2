namespace MiProyectoMVC.Repositories;

using MiProyectoMVC.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

public class CajaRepository : ICajaRepository
{
    private readonly HttpClient _http;

    public CajaRepository(HttpClient http)
    {
        _http = http;
        _http.BaseAddress = new Uri("https://demo2-api-8rhs.onrender.com/api/");
    }

  
    public async Task Registrar(Caja caja)
    {
        
        caja.Comercio = new Comercio
        {
            IdComercio = caja.ComercioId
        };

        var response = await _http.PostAsJsonAsync("cajas", caja);
        response.EnsureSuccessStatusCode();
    }

  
    public async Task Editar(Caja caja)
    {
        
        caja.Comercio = new Comercio
        {
            IdComercio = caja.ComercioId
        };

        var response = await _http.PutAsJsonAsync($"cajas/{caja.IdCaja}", caja);
        response.EnsureSuccessStatusCode();
    }

   
    public async Task Eliminar(int id)
    {
        var response = await _http.DeleteAsync($"cajas/{id}");
        response.EnsureSuccessStatusCode();
    }

    
    public async Task CerrarCaja(int id, decimal montoFinal)
{
    var caja = await ObtenerPorId(id);
    caja.EstaAbierta = false;
    caja.MontoFinal = montoFinal;
    caja.FechaCierre = DateTime.UtcNow; // asigna fecha de cierre
    await _http.PutAsJsonAsync($"cajas/{id}", caja);
}

    public async Task<Caja?> ObtenerPorId(int id)
    {
        return await _http.GetFromJsonAsync<Caja>($"cajas/{id}");
    }

    public async Task<List<Caja>> ObtenerTodos()
    {
        var data = await _http.GetFromJsonAsync<List<Caja>>("cajas");
        return data ?? new List<Caja>();
    }
}