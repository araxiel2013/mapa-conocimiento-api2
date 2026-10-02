using Microsoft.AspNetCore.Mvc;
using mapa_conocimiento_api.Peticiones;
using mapa_conocimiento_api.Servicios;

namespace mapa_conocimiento_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocenteProductoController : ControllerBase
    {
        private readonly IDocenteProductoService _servicio;
        public DocenteProductoController(IDocenteProductoService servicio) => _servicio = servicio;

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] DocenteProductoCrear peticion)
        {
            try {
                await _servicio.CrearAsync(peticion);
                return Ok(new { estado = 200, mensaje = "Docente vinculado al producto exitosamente." });
            } catch (ArgumentException ex) {
                return BadRequest(new { estado = 400, mensaje = ex.Message });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Eliminar([FromQuery] int docenteCedula, [FromQuery] int productoId)
        {
            var fueEliminado = await _servicio.EliminarAsync(docenteCedula, productoId);
            if (!fueEliminado) return NotFound(new { estado = 404, mensaje = "La relación no fue encontrada para eliminar." });
            return Ok(new { estado = 200, mensaje = "Relación eliminada exitosamente (borrado lógico)." });
        }
    }
}