using Microsoft.AspNetCore.Mvc;
using mapa_conocimiento_api.Peticiones;
using mapa_conocimiento_api.Servicios;
using System.Threading.Tasks;

namespace mapa_conocimiento_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TipoProductoController : ControllerBase
    {
        private readonly ITipoProductoService _servicio;

        public TipoProductoController(ITipoProductoService servicio)
        {
            _servicio = servicio;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos() => Ok(await _servicio.ObtenerTodosAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var resultado = await _servicio.ObtenerPorIdAsync(id);
            if (resultado == null) return NotFound(new { mensaje = "El tipo de producto no fue encontrado." });
            return Ok(resultado);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] TipoProductoCrear peticion)
        {
            var nuevaEntidad = await _servicio.CrearAsync(peticion);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = nuevaEntidad.id }, nuevaEntidad);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] TipoProductoEditar peticion)
        {
            if (!await _servicio.ActualizarAsync(id, peticion)) return NotFound(new { mensaje = "El tipo de producto no fue encontrado." });
            return Ok(new { mensaje = "Tipo de producto actualizado correctamente." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            if (!await _servicio.EliminarAsync(id)) return NotFound(new { mensaje = "El tipo de producto no fue encontrado." });
            return Ok(new { mensaje = "Tipo de producto eliminado correctamente." });
        }
    }
}

