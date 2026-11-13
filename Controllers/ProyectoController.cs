using System;
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
    public class ProyectoController : ControllerBase
    {
        private readonly IProyectoService _servicio;

        public ProyectoController(IProyectoService servicio)
        {
            _servicio = servicio;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var resultados = await _servicio.ObtenerTodosAsync();
            return Ok(new { tabla = "proyecto", total = resultados.Count(), datos = resultados });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var resultado = await _servicio.ObtenerPorIdAsync(id);
            if (resultado == null) return NotFound(new { estado = 404, mensaje = "El proyecto no fue encontrado." });
            return Ok(resultado);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] ProyectoCrear peticion)
        {
            try
            {
                var nuevaEntidad = await _servicio.CrearAsync(peticion);
                return CreatedAtAction(nameof(ObtenerPorId), new { id = nuevaEntidad.id }, new { estado = 201, mensaje = "Proyecto creado exitosamente.", datos = nuevaEntidad });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { estado = 400, mensaje = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] ProyectoEditar peticion)
        {
            try
            {
                var fueActualizado = await _servicio.ActualizarAsync(id, peticion);
                if (!fueActualizado) return NotFound(new { estado = 404, mensaje = "El proyecto no fue encontrado para actualizar." });
                return Ok(new { estado = 200, mensaje = "Proyecto actualizado correctamente." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { estado = 400, mensaje = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var fueEliminado = await _servicio.EliminarAsync(id);
            if (!fueEliminado) return NotFound(new { estado = 404, mensaje = "El proyecto no fue encontrado para eliminar." });
            return Ok(new { estado = 200, mensaje = "Proyecto eliminado correctamente (borrado lógico)." });
        }
    }
}
