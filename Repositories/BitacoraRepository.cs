namespace MiProyectoMVC.Repositories;

using MiProyectoMVC.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

public class BitacoraRepository : IBitacoraRepository
{
    private readonly HttpClient _http;

    public BitacoraRepository(HttpClient http)
    {
        _http = http;
        _http.BaseAddress = new Uri("https://demo2-api-8rhs.onrender.com/api/");
    }

    public async Task RegistrarEvento(BitacoraEvento evento)
    {
        await _http.PostAsJsonAsync("bitacora", evento);
    }

    public async Task<List<BitacoraEvento>> ObtenerEventos()
    {
        var data = await _http.GetFromJsonAsync<List<BitacoraEvento>>("bitacora");
        return data ?? new List<BitacoraEvento>();
    }
}