using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mapa_conocimiento_api.Models;
using mapa_conocimiento_api.Peticiones;
using mapa_conocimiento_api.Servicios;

namespace mapa_conocimiento_api.Controllers
{
    // Habilita la validación automática de DTOs y define la ruta base: api/AreaConocimiento
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AreaConocimientoController : ControllerBase
    {
        private readonly IAreaConocimientoService _servicio;

        // Inyectamos nuestro servicio
        public AreaConocimientoController(IAreaConocimientoService servicio)
        {
            _servicio = servicio;
        }

        // GET: api/AreaConocimiento
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var resultados = await _servicio.ObtenerTodosAsync();
            return Ok(resultados); // 200 OK
        }

        // GET: api/AreaConocimiento/5
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var resultado = await _servicio.ObtenerPorIdAsync(id);
            if (resultado == null) 
            {
                return NotFound(new { mensaje = "El área de conocimiento no fue encontrada." }); // 404 Not Found
            }
            return Ok(resultado); // 200 OK
        }

        // POST: api/AreaConocimiento
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] AreaConocimientoCrear peticion)
        {
            var nuevaEntidad = await _servicio.CrearAsync(peticion);
            // 201 Created, devuelve la URL para consultar el nuevo recurso y el objeto creado
            return CreatedAtAction(nameof(ObtenerPorId), new { id = nuevaEntidad.id }, nuevaEntidad);
        }

        // PUT: api/AreaConocimiento/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] AreaConocimientoEditar peticion)
        {
            var fueActualizado = await _servicio.ActualizarAsync(id, peticion);
            if (!fueActualizado) 
            {
                return NotFound(new { mensaje = "El área de conocimiento no fue encontrada para actualizar." }); // 404 Not Found
            }
            return Ok(new { mensaje = "Área de conocimiento actualizada correctamente." }); // 200 OK
        }

        // DELETE: api/AreaConocimiento/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var fueEliminado = await _servicio.EliminarAsync(id);
            if (!fueEliminado) 
            {
                return NotFound(new { mensaje = "El área de conocimiento no fue encontrada para eliminar." }); // 404 Not Found
            }
            return Ok(new { mensaje = "Área de conocimiento eliminada correctamente." }); // 200 OK
        }
    }
}