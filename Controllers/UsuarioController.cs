using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mapa_conocimiento_api.Peticiones;
using mapa_conocimiento_api.Servicios;

namespace mapa_conocimiento_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // FASE 3: Bloqueo estricto basado en roles (RBAC). Solo administradores pueden entrar.
    [Authorize(Roles = "Administrador")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _servicio;

        public UsuarioController(IUsuarioService servicio)
        {
            _servicio = servicio;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var resultados = await _servicio.ObtenerTodosAsync();
            return Ok(new { tabla = "usuario", total = resultados.Count(), datos = resultados });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var resultado = await _servicio.ObtenerPorIdAsync(id);
            if (resultado == null) return NotFound(new { estado = 404, mensaje = "El usuario no fue encontrado." });
            return Ok(resultado);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] UsuarioCrear peticion)
        {
            try
            {
                var nuevaEntidad = await _servicio.CrearAsync(peticion);
                return CreatedAtAction(nameof(ObtenerPorId), new { id = nuevaEntidad.id }, new { estado = 201, mensaje = "Usuario creado exitosamente.", datos = nuevaEntidad });
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
            if (!fueEliminado) return NotFound(new { estado = 404, mensaje = "El usuario no fue encontrado para eliminar." });
            return Ok(new { estado = 200, mensaje = "Usuario eliminado correctamente (borrado lógico)." });
        }
    }
}