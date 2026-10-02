using Microsoft.AspNetCore.Mvc;
using mapa_conocimiento_api.Peticiones;
using mapa_conocimiento_api.Servicios;

namespace mapa_conocimiento_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocenteController : ControllerBase
    {
        private readonly IDocenteService _servicio;

        public DocenteController(IDocenteService servicio)
        {
            _servicio = servicio;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var resultados = await _servicio.ObtenerTodosAsync();
            return Ok(new { tabla = "docente", total = resultados.Count(), datos = resultados }); 
        }

        [HttpGet("{cedula}")]
        public async Task<IActionResult> ObtenerPorId(int cedula)
        {
            var resultado = await _servicio.ObtenerPorIdAsync(cedula);
            if (resultado == null) return NotFound(new { estado = 404, mensaje = "El docente no fue encontrado." });
            return Ok(resultado);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] DocenteCrear peticion)
        {
            try
            {
                var nuevaEntidad = await _servicio.CrearAsync(peticion);
                return CreatedAtAction(nameof(ObtenerPorId), new { cedula = nuevaEntidad.cedula }, new { estado = 201, mensaje = "Docente creado exitosamente.", datos = nuevaEntidad });
            }
            catch (ArgumentException ex)
            {
                // Capturamos el error de validación de la Llave Foránea y devolvemos 400
                return BadRequest(new { estado = 400, mensaje = ex.Message });
            }
        }

        [HttpPut("{cedula}")]
        public async Task<IActionResult> Actualizar(int cedula, [FromBody] DocenteEditar peticion)
        {
            try
            {
                var fueActualizado = await _servicio.ActualizarAsync(cedula, peticion);
                if (!fueActualizado) return NotFound(new { estado = 404, mensaje = "El docente no fue encontrado para actualizar." });
                return Ok(new { estado = 200, mensaje = "Docente actualizado correctamente." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { estado = 400, mensaje = ex.Message });
            }
        }

        [HttpDelete("{cedula}")]
        public async Task<IActionResult> Eliminar(int cedula)
        {
            var fueEliminado = await _servicio.EliminarAsync(cedula);
            if (!fueEliminado) return NotFound(new { estado = 404, mensaje = "El docente no fue encontrado para eliminar." });
            return Ok(new { estado = 200, mensaje = "Docente eliminado correctamente (borrado lógico)." });
        }
    }
}