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
    public class AliadoController : ControllerBase
    {
        private readonly IAliadoService _servicio;

        public AliadoController(IAliadoService servicio)
        {
            _servicio = servicio;
        }

        // GET: api/Aliado
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var resultados = await _servicio.ObtenerTodosAsync();
            return Ok(new { tabla = "aliado", total = resultados.Count(), datos = resultados });
        }

        // GET: api/Aliado/900123456
        [HttpGet("{nit}")]
        public async Task<IActionResult> ObtenerPorId(int nit)
        {
            var resultado = await _servicio.ObtenerPorIdAsync(nit);
            if (resultado == null) 
            {
                return NotFound(new { estado = 404, mensaje = "El aliado no fue encontrado." });
            }
            return Ok(resultado);
        }

        // POST: api/Aliado
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] AliadoCrear peticion)
        {
            try
            {
                var nuevaEntidad = await _servicio.CrearAsync(peticion);
                return CreatedAtAction(nameof(ObtenerPorId), new { nit = nuevaEntidad.nit }, new { estado = 201, mensaje = "Aliado creado exitosamente.", datos = nuevaEntidad });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { estado = 400, mensaje = ex.Message });
            }
        }

        // PUT: api/Aliado/900123456
        [HttpPut("{nit}")]
        public async Task<IActionResult> Actualizar(int nit, [FromBody] AliadoEditar peticion)
        {
            try
            {
                var fueActualizado = await _servicio.ActualizarAsync(nit, peticion);
                if (!fueActualizado) 
                {
                    return NotFound(new { estado = 404, mensaje = "El aliado no fue encontrado para actualizar." });
                }
                return Ok(new { estado = 200, mensaje = "Aliado actualizado correctamente." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { estado = 400, mensaje = ex.Message });
            }
        }

        // DELETE: api/Aliado/900123456
        [HttpDelete("{nit}")]
        public async Task<IActionResult> Eliminar(int nit)
        {
            var fueEliminado = await _servicio.EliminarAsync(nit);
            if (!fueEliminado) 
            {
                return NotFound(new { estado = 404, mensaje = "El aliado no fue encontrado para eliminar." });
            }
            return Ok(new { estado = 200, mensaje = "Aliado eliminado correctamente (borrado lógico)." });
        }
    }
}
