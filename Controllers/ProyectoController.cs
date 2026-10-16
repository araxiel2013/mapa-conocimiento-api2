using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mapa_conocimiento_api.Peticiones;
using mapa_conocimiento_api.Servicios;
using System.Threading.Tasks;

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
        public async Task<IActionResult> ObtenerTodos() => Ok(await _servicio.ObtenerTodosAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var resultado = await _servicio.ObtenerPorIdAsync(id);
            if (resultado == null) return NotFound(new { mensaje = "El proyecto no fue encontrado." });
            return Ok(resultado);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] ProyectoCrear peticion)
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

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] ProyectoEditar peticion)
        {
            try
            {
                if (!await _servicio.ActualizarAsync(id, peticion)) return NotFound(new { mensaje = "El proyecto no fue encontrado." });
                return Ok(new { mensaje = "Proyecto actualizado correctamente." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { estado = 400, mensaje = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            if (!await _servicio.EliminarAsync(id)) return NotFound(new { mensaje = "El proyecto no fue encontrado." });
            return Ok(new { mensaje = "Proyecto eliminado correctamente." });
        }
    }
}
