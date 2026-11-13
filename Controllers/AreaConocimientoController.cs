using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mapa_conocimiento_api.Peticiones;
using mapa_conocimiento_api.Servicios;

namespace mapa_conocimiento_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AreaConocimientoController : ControllerBase
    {
        private readonly IAreaConocimientoService _servicio;

        public AreaConocimientoController(IAreaConocimientoService servicio)
        {
            _servicio = servicio;
        }

        // GET: api/AreaConocimiento
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var resultados = await _servicio.ObtenerTodosAsync();
            return Ok(new { tabla = "area_conocimiento", total = resultados.Count(), datos = resultados });
        }

        // GET: api/AreaConocimiento/5
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var resultado = await _servicio.ObtenerPorIdAsync(id);
            if (resultado == null) 
            {
                return NotFound(new { estado = 404, mensaje = "El área de conocimiento no fue encontrada." });
            }
            return Ok(resultado);
        }

        // POST: api/AreaConocimiento
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] AreaConocimientoCrear peticion)
        {
            try
            {
                var nuevaEntidad = await _servicio.CrearAsync(peticion);
                return CreatedAtAction(nameof(ObtenerPorId), new { id = nuevaEntidad.id }, new { estado = 201, mensaje = "Área de conocimiento creada exitosamente.", datos = nuevaEntidad });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { estado = 400, mensaje = ex.Message });
            }
        }

        // PUT: api/AreaConocimiento/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] AreaConocimientoEditar peticion)
        {
            try
            {
                var fueActualizado = await _servicio.ActualizarAsync(id, peticion);
                if (!fueActualizado) 
                {
                    return NotFound(new { estado = 404, mensaje = "El área de conocimiento no fue encontrada para actualizar." });
                }
                return Ok(new { estado = 200, mensaje = "Área de conocimiento actualizada correctamente." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { estado = 400, mensaje = ex.Message });
            }
        }

        // DELETE: api/AreaConocimiento/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var fueEliminado = await _servicio.EliminarAsync(id);
            if (!fueEliminado) 
            {
                return NotFound(new { estado = 404, mensaje = "El área de conocimiento no fue encontrada para eliminar." });
            }
            return Ok(new { estado = 200, mensaje = "Área de conocimiento eliminada correctamente (borrado lógico)." });
        }
    }
}
