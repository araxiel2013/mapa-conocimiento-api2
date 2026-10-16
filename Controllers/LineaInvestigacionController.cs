using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mapa_conocimiento_api.Peticiones;
using mapa_conocimiento_api.Servicios;

namespace mapa_conocimiento_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class LineaInvestigacionController : ControllerBase
    {
        private readonly ILineaInvestigacionService _servicio;

        public LineaInvestigacionController(ILineaInvestigacionService servicio)
        {
            _servicio = servicio;
        }

        // GET: api/LineaInvestigacion
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var resultados = await _servicio.ObtenerTodosAsync();
            return Ok(resultados);
        }

        // GET: api/LineaInvestigacion/5
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var resultado = await _servicio.ObtenerPorIdAsync(id);
            if (resultado == null) 
            {
                return NotFound(new { estado = 404, mensaje = "La línea de investigación no fue encontrada." });
            }
            return Ok(resultado);
        }

        // POST: api/LineaInvestigacion
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] LineaInvestigacionCrear peticion)
        {
            try
            {
                var nuevaEntidad = await _servicio.CrearAsync(peticion);
                return CreatedAtAction(nameof(ObtenerPorId), new { id = nuevaEntidad.id }, nuevaEntidad);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { estado = 400, mensaje = ex.Message });
            }
        }

        // PUT: api/LineaInvestigacion/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] LineaInvestigacionEditar peticion)
        {
            try
            {
                var fueActualizado = await _servicio.ActualizarAsync(id, peticion);
                if (!fueActualizado) 
                {
                    return NotFound(new { estado = 404, mensaje = "La línea de investigación no fue encontrada para actualizar." });
                }
                return Ok(new { mensaje = "Línea de investigación actualizada correctamente." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { estado = 400, mensaje = ex.Message });
            }
        }

        // DELETE: api/LineaInvestigacion/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var fueEliminado = await _servicio.EliminarAsync(id);
            if (!fueEliminado) 
            {
                return NotFound(new { estado = 404, mensaje = "La línea de investigación no fue encontrada para eliminar." });
            }
            return Ok(new { mensaje = "Línea de investigación eliminada correctamente." });
        }
    }
}