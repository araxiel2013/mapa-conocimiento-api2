using Microsoft.AspNetCore.Mvc;
using mapa_conocimiento_api.Peticiones;
using mapa_conocimiento_api.Servicios;

namespace mapa_conocimiento_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest peticion)
        {
            var resultado = await _authService.AutenticarAsync(peticion);

            if (resultado == null)
            {
                return Unauthorized(new 
                { 
                    estado = 401, 
                    mensaje = "Credenciales inválidas",
                    detalle = "El usuario o la contraseña no coinciden."
                });
            }

            return Ok(new 
            { 
                estado = 200, 
                mensaje = "Autenticación exitosa",
                token = resultado.token,
                usuario = resultado.usuario
            });
        }

        [HttpPost("recuperar-contrasena")]
        public async Task<IActionResult> RecuperarContrasena([FromBody] RecuperarContrasenaRequest peticion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var (exitoso, mensaje) = await _authService.RecuperarContrasenaAsync(peticion);

            if (!exitoso)
            {
                return BadRequest(new 
                { 
                    estado = 400, 
                    mensaje 
                });
            }

            return Ok(new 
            { 
                estado = 200, 
                mensaje 
            });
        }
    }
}