using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mapa_conocimiento_api.Peticiones;
using mapa_conocimiento_api.Servicios;

namespace mapa_conocimiento_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoService _servicio;

        public ProductoController(IProductoService servicio)
        {
            _servicio = servicio;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTodos()
        {
            var resultados = await _servicio.ObtenerTodosAsync();
            return Ok(new { tabla = "producto", total = resultados.Count(), datos = resultados });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var resultado = await _servicio.ObtenerPorIdAsync(id);
            if (resultado == null) return NotFound(new { estado = 404, mensaje = "El producto no fue encontrado." });
            return Ok(resultado);
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] ProductoCrear peticion)
        {
            try
            {
                var nuevaEntidad = await _servicio.CrearAsync(peticion);
                return CreatedAtAction(nameof(ObtenerPorId), new { id = nuevaEntidad.id }, new { estado = 201, mensaje = "Producto creado exitosamente.", datos = nuevaEntidad });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { estado = 400, mensaje = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] ProductoEditar peticion)
        {
            try
            {
                var fueActualizado = await _servicio.ActualizarAsync(id, peticion);
                if (!fueActualizado) return NotFound(new { estado = 404, mensaje = "El producto no fue encontrado para actualizar." });
                return Ok(new { estado = 200, mensaje = "Producto actualizado correctamente." });
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
            if (!fueEliminado) return NotFound(new { estado = 404, mensaje = "El producto no fue encontrado para eliminar." });
            return Ok(new { estado = 200, mensaje = "Producto eliminado correctamente (borrado lógico)." });
        }
    }
}