using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace MiProyectoMVC.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthApiController : ControllerBase
    {
        private readonly IHttpClientFactory _httpFactory;
        private readonly IConfiguration _config;

        public AuthApiController(IHttpClientFactory httpFactory, IConfiguration config)
        {
            _httpFactory = httpFactory;
            _config = config;
        }

        [HttpPost("token")]
        public async Task<IActionResult> ObtenerToken([FromBody] TokenRequestDto request)
        {
            try
            {
                var http = _httpFactory.CreateClient("API");
                var configuraciones = await http.GetFromJsonAsync<List<ConfiguracionDto>>("configuracioncomercios");

                var config = configuraciones?.FirstOrDefault(c =>
                    c.IdComercio == request.IdComercio &&
                    (c.TipoConfiguracion == 2 || c.TipoConfiguracion == 3));

                if (config == null)
                    return Unauthorized(new { mensaje = "El comercio no tiene configuración Externa o Ambas." });

                // Token simple sin JWT por limitaciones del entorno
                var token = Convert.ToBase64String(
                    System.Text.Encoding.UTF8.GetBytes($"{request.IdComercio}:{DateTime.UtcNow:yyyyMMddHH}:{_config["Jwt:Key"]}"));

                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return Unauthorized(new { mensaje = "Error: " + ex.Message });
            }
        }
    }

    public class TokenRequestDto
    {
        public int IdComercio { get; set; }
    }

    public class ConfiguracionDto
    {
        public int IdComercio { get; set; }
        public int TipoConfiguracion { get; set; }
    }
}