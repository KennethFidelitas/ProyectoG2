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
        try
        {
            await _http.PostAsJsonAsync("bitacora", evento);
        }
        catch
        {
            
        }
    }

    public async Task<List<BitacoraEvento>> ObtenerEventos()
    {
        try
        {
            var data = await _http.GetFromJsonAsync<List<BitacoraEvento>>("bitacora");
            return data ?? new List<BitacoraEvento>();
        }
        catch
        {
            return new List<BitacoraEvento>();
        }
    }
}