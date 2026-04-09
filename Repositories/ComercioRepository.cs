namespace MiProyectoMVC.Repositories;

using MiProyectoMVC.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

public class ComercioRepository : IComercioRepository
{
    private readonly HttpClient _http;

    public ComercioRepository(HttpClient http)
    {
        _http = http;
        _http.BaseAddress = new Uri("https://demo2-api-8rhs.onrender.com/api/");
    }

    public async Task<List<Comercio>> ObtenerTodos()
    {
        var data = await _http.GetFromJsonAsync<List<Comercio>>("comercios");
        return data ?? new List<Comercio>();
    }

   public async Task<Comercio?> ObtenerPorId(int id)
{
    var lista = await _http.GetFromJsonAsync<List<Comercio>>("comercios");
    return lista?.FirstOrDefault(c => c.IdComercio == id);
}

    public async Task Registrar(Comercio comercio)
    {
        await _http.PostAsJsonAsync("comercios", comercio);
    }

    public async Task Editar(Comercio comercio)
    {
        await _http.PutAsJsonAsync($"comercios/{comercio.IdComercio}", comercio);
    }

    public async Task Eliminar(int id)
    {
        await _http.DeleteAsync($"comercios/{id}");
    }
}