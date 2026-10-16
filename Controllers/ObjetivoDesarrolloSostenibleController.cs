using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mapa_conocimiento_api.Peticiones;
using mapa_conocimiento_api.Servicios;

namespace mapa_conocimiento_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ObjetivoDesarrolloSostenibleController : ControllerBase
    {
        private readonly IObjetivoDesarrolloSostenibleService _servicio;

        public ObjetivoDesarrolloSostenibleController(IObjetivoDesarrolloSostenibleService servicio)
        {
            _servicio = servicio;
        }

        // GET: api/ObjetivoDesarrolloSostenible
        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var resultados = await _servicio.ObtenerTodosAsync();
            return Ok(resultados); 
        }

        // GET: api/ObjetivoDesarrolloSostenible/5
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var resultado = await _servicio.ObtenerPorIdAsync(id);
            if (resultado == null) 
            {
                return NotFound(new { mensaje = "El ODS no fue encontrado." }); 
            }
            return Ok(resultado); 
        }

        // POST: api/ObjetivoDesarrolloSostenible
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] ObjetivoDesarrolloSostenibleCrear peticion)
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

        // PUT: api/ObjetivoDesarrolloSostenible/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] ObjetivoDesarrolloSostenibleEditar peticion)
        {
            try
            {
                var fueActualizado = await _servicio.ActualizarAsync(id, peticion);
                if (!fueActualizado) 
                {
                    return NotFound(new { mensaje = "El ODS no fue encontrado para actualizar." }); 
                }
                return Ok(new { mensaje = "ODS actualizado correctamente." }); 
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { estado = 400, mensaje = ex.Message });
            }
        }

        // DELETE: api/ObjetivoDesarrolloSostenible/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var fueEliminado = await _servicio.EliminarAsync(id);
            if (!fueEliminado) 
            {
                return NotFound(new { mensaje = "El ODS no fue encontrado para eliminar." }); 
            }
            return Ok(new { mensaje = "ODS eliminado correctamente." }); 
        }
    }
}