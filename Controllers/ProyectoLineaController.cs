using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mapa_conocimiento_api.Peticiones;
using mapa_conocimiento_api.Servicios;

namespace mapa_conocimiento_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProyectoLineaController : ControllerBase
    {
        private readonly IProyectoLineaService _servicio;
        public ProyectoLineaController(IProyectoLineaService servicio) => _servicio = servicio;

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] ProyectoLineaCrear peticion)
        {
            try {
                await _servicio.CrearAsync(peticion);
                return Ok(new { estado = 200, mensaje = "Línea de investigación asignada al proyecto exitosamente." });
            } catch (ArgumentException ex) {
                return BadRequest(new { estado = 400, mensaje = ex.Message });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Eliminar([FromQuery] int proyectoId, [FromQuery] int lineaInvestigacionId)
        {
            var fueEliminado = await _servicio.EliminarAsync(proyectoId, lineaInvestigacionId);
            if (!fueEliminado) return NotFound(new { estado = 404, mensaje = "La relación no fue encontrada para eliminar." });
            return Ok(new { estado = 200, mensaje = "Relación eliminada exitosamente (borrado lógico)." });
        }
    }
}