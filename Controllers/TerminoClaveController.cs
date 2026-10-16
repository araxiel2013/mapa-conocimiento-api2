using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mapa_conocimiento_api.Peticiones;
using mapa_conocimiento_api.Servicios;

namespace mapa_conocimiento_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TerminoClaveController : ControllerBase
    {
        private readonly ITerminoClaveService _servicio;

        public TerminoClaveController(ITerminoClaveService servicio)
        {
            _servicio = servicio;
        }

        // GET: api/TerminoClave
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var resultados = await _servicio.ObtenerTodosAsync();
            return Ok(resultados);
        }

        // GET: api/TerminoClave/inteligencia-artificial
        [HttpGet("{termino}")]
        public async Task<IActionResult> ObtenerPorId(string termino)
        {
            var resultado = await _servicio.ObtenerPorIdAsync(termino);
            if (resultado == null) 
            {
                return NotFound(new { mensaje = "El término clave no fue encontrado." });
            }
            return Ok(resultado);
        }

        // POST: api/TerminoClave
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] TerminoClaveCrear peticion)
        {
            try
            {
                var nuevaEntidad = await _servicio.CrearAsync(peticion);
                // Redirigimos a ObtenerPorId usando el string recién creado
                return CreatedAtAction(nameof(ObtenerPorId), new { termino = nuevaEntidad.termino }, nuevaEntidad);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { estado = 400, mensaje = ex.Message });
            }
        }

        // DELETE: api/TerminoClave/inteligencia-artificial
        [HttpDelete("{termino}")]
        public async Task<IActionResult> Eliminar(string termino)
        {
            var fueEliminado = await _servicio.EliminarAsync(termino);
            if (!fueEliminado) 
            {
                return NotFound(new { mensaje = "El término clave no fue encontrado para eliminar." });
            }
            return Ok(new { mensaje = "Término clave eliminado correctamente." });
        }
    }
}