using Microsoft.AspNetCore.Mvc;
using mapa_conocimiento_api.Peticiones;
using mapa_conocimiento_api.Servicios;

namespace mapa_conocimiento_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AreaAplicacionController : ControllerBase
    {
        private readonly IAreaAplicacionService _servicio;

        public AreaAplicacionController(IAreaAplicacionService servicio)
        {
            _servicio = servicio;
        }

        // GET: api/AreaAplicacion
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var resultados = await _servicio.ObtenerTodosAsync();
            return Ok(resultados);
        }

        // GET: api/AreaAplicacion/5
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var resultado = await _servicio.ObtenerPorIdAsync(id);
            if (resultado == null) 
            {
                return NotFound(new { mensaje = "El área de aplicación no fue encontrada." });
            }
            return Ok(resultado);
        }

        // POST: api/AreaAplicacion
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] AreaAplicacionCrear peticion)
        {
            var nuevaEntidad = await _servicio.CrearAsync(peticion);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = nuevaEntidad.id }, nuevaEntidad);
        }

        // PUT: api/AreaAplicacion/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] AreaAplicacionEditar peticion)
        {
            var fueActualizado = await _servicio.ActualizarAsync(id, peticion);
            if (!fueActualizado) 
            {
                return NotFound(new { mensaje = "El área de aplicación no fue encontrada para actualizar." });
            }
            return Ok(new { mensaje = "Área de aplicación actualizada correctamente." });
        }

        // DELETE: api/AreaAplicacion/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var fueEliminado = await _servicio.EliminarAsync(id);
            if (!fueEliminado) 
            {
                return NotFound(new { mensaje = "El área de aplicación no fue encontrada para eliminar." });
            }
            return Ok(new { mensaje = "Área de aplicación eliminada correctamente." });
        }
    }
}