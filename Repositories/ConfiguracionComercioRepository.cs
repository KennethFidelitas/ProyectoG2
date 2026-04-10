namespace MiProyectoMVC.Repositories;

using MiProyectoMVC.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

public class ConfiguracionComercioRepository : IConfiguracionComercioRepository
{
    private readonly HttpClient _http;

    public ConfiguracionComercioRepository(HttpClient http)
    {
        _http = http;
        _http.BaseAddress = new Uri("https://demo2-api-8rhs.onrender.com/api/");
    }

    public async Task<List<ConfiguracionComercio>> ObtenerTodos()
    {
        var data = await _http.GetFromJsonAsync<List<ConfiguracionComercio>>("configuracion-comercio");
        return data ?? new List<ConfiguracionComercio>();
    }

    public async Task<ConfiguracionComercio?> ObtenerPorComercio(int idComercio)
    {
        var lista = await _http.GetFromJsonAsync<List<ConfiguracionComercio>>("configuracion-comercio");
        return lista?.FirstOrDefault(c => c.IdComercio == idComercio);
    }

    public async Task Crear(ConfiguracionComercio config)
    {
        await _http.PostAsJsonAsync("configuracion-comercio", config);
    }

    public async Task Editar(ConfiguracionComercio config)
    {
        await _http.PutAsJsonAsync($"configuracion-comercio/{config.IdConfiguracion}", config);
    }
}