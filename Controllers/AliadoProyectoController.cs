using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mapa_conocimiento_api.Peticiones;
using mapa_conocimiento_api.Servicios;

namespace mapa_conocimiento_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AliadoProyectoController : ControllerBase
    {
        private readonly IAliadoProyectoService _servicio;
        public AliadoProyectoController(IAliadoProyectoService servicio) => _servicio = servicio;

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] AliadoProyectoCrear peticion)
        {
            try {
                await _servicio.CrearAsync(peticion);
                return Ok(new { estado = 200, mensaje = "Aliado asignado al proyecto exitosamente." });
            } catch (ArgumentException ex) {
                return BadRequest(new { estado = 400, mensaje = ex.Message });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Eliminar([FromQuery] int aliadoNit, [FromQuery] int proyectoId)
        {
            var fueEliminado = await _servicio.EliminarAsync(aliadoNit, proyectoId);
            if (!fueEliminado) return NotFound(new { estado = 404, mensaje = "La relación no fue encontrada para eliminar." });
            return Ok(new { estado = 200, mensaje = "Relación eliminada exitosamente (borrado lógico)." });
        }
    }
}