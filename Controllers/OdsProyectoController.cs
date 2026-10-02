using Microsoft.AspNetCore.Mvc;
using mapa_conocimiento_api.Peticiones;
using mapa_conocimiento_api.Servicios;

namespace mapa_conocimiento_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OdsProyectoController : ControllerBase
    {
        private readonly IOdsProyectoService _servicio;
        public OdsProyectoController(IOdsProyectoService servicio) => _servicio = servicio;

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] OdsProyectoCrear peticion)
        {
            try {
                await _servicio.CrearAsync(peticion);
                return Ok(new { estado = 200, mensaje = "ODS asignado al proyecto exitosamente." });
            } catch (ArgumentException ex) {
                return BadRequest(new { estado = 400, mensaje = ex.Message });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Eliminar([FromQuery] int proyectoId, [FromQuery] int odsId)
        {
            var fueEliminado = await _servicio.EliminarAsync(proyectoId, odsId);
            if (!fueEliminado) return NotFound(new { estado = 404, mensaje = "La relación no fue encontrada para eliminar." });
            return Ok(new { estado = 200, mensaje = "Relación eliminada exitosamente (borrado lógico)." });
        }
    }
}