using Microsoft.AspNetCore.Mvc;
using mapa_conocimiento_api.Peticiones;
using mapa_conocimiento_api.Servicios;

namespace mapa_conocimiento_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
            return Ok(resultados);
        }

        // GET: api/Aliado/900123456
        [HttpGet("{nit}")]
        public async Task<IActionResult> ObtenerPorId(int nit)
        {
            var resultado = await _servicio.ObtenerPorIdAsync(nit);
            if (resultado == null) 
            {
                return NotFound(new { mensaje = "El aliado no fue encontrado." });
            }
            return Ok(resultado);
        }

        // POST: api/Aliado
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] AliadoCrear peticion)
        {
            var nuevaEntidad = await _servicio.CrearAsync(peticion);
            // Retornamos la URL apuntando al parámetro 'nit'
            return CreatedAtAction(nameof(ObtenerPorId), new { nit = nuevaEntidad.nit }, nuevaEntidad);
        }

        // PUT: api/Aliado/900123456
        [HttpPut("{nit}")]
        public async Task<IActionResult> Actualizar(int nit, [FromBody] AliadoEditar peticion)
        {
            var fueActualizado = await _servicio.ActualizarAsync(nit, peticion);
            if (!fueActualizado) 
            {
                return NotFound(new { mensaje = "El aliado no fue encontrado para actualizar." });
            }
            return Ok(new { mensaje = "Aliado actualizado correctamente." });
        }

        // DELETE: api/Aliado/900123456
        [HttpDelete("{nit}")]
        public async Task<IActionResult> Eliminar(int nit)
        {
            var fueEliminado = await _servicio.EliminarAsync(nit);
            if (!fueEliminado) 
            {
                return NotFound(new { mensaje = "El aliado no fue encontrado para eliminar." });
            }
            return Ok(new { mensaje = "Aliado eliminado correctamente." });
        }
    }
}