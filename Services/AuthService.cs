using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using mapa_conocimiento_api.Models;
using mapa_conocimiento_api.Peticiones;
using mapa_conocimiento_api.Repositorios;

namespace mapa_conocimiento_api.Servicios
{
    public class AuthService : IAuthService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IUsuarioRepository usuarioRepository, IConfiguration configuration)
        {
            _usuarioRepository = usuarioRepository;
            _configuration = configuration;
        }

        public async Task<LoginResponse?> AutenticarAsync(LoginRequest peticion)
        {
            // 1. Buscamos el usuario en BD a través del repositorio incluyendo sus roles
            var usuario = await _usuarioRepository.ObtenerPorUsernameConRolesAsync(peticion.username);

            // 2. Comparamos la contraseña en texto plano contra el Hash BCrypt de la base de datos
            if (usuario == null || !BCrypt.Net.BCrypt.Verify(peticion.password, usuario.password))
            {
                return null; // Credenciales inválidas
            }

            // 3. Construimos los "Claims" (información que va dentro del token)
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.username),
                new Claim(ClaimTypes.Email, usuario.email)
            };

            var rolesLista = new List<string>();
            foreach (var ru in usuario.RolesUsuario)
            {
                if (ru.RolRelacionado != null)
                {
                    // Añadimos el rol al token para que el middleware de C# pueda validar permisos
                    claims.Add(new Claim(ClaimTypes.Role, ru.RolRelacionado.nombre));
                    rolesLista.Add(ru.RolRelacionado.nombre);
                }
            }

            // 4. Firmamos matemáticamente el Token
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = _configuration["JwtSettings:Key"]
                ?? _configuration["JwtSettings__Key"]
                ?? _configuration["JWT_SECRET_KEY"]
                ?? jwtSettings.GetValue<string>("Key")
                ?? throw new InvalidOperationException("La clave secreta JWT (JwtSettings:Key o JwtSettings__Key) no está configurada.");

            var issuer = _configuration["JwtSettings:Issuer"]
                ?? _configuration["JwtSettings__Issuer"]
                ?? jwtSettings.GetValue<string>("Issuer")
                ?? "MapaConocimientoAPI";

            var audience = _configuration["JwtSettings:Audience"]
                ?? _configuration["JwtSettings__Audience"]
                ?? jwtSettings.GetValue<string>("Audience")
                ?? "MapaConocimientoReactClient";

            var durationMinutes = jwtSettings.GetValue<double?>("DurationInMinutes") ?? 480;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(durationMinutes),
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

            // 5. Devolvemos el DTO estructurado
            return new LoginResponse
            {
                token = tokenString,
                usuario = new UsuarioAuthDto
                {
                    username = usuario.username,
                    nombre_completo = usuario.nombre_completo,
                    roles = rolesLista
                }
            };
        }

        public Task<LoginResponse?> LoginAsync(LoginRequest peticion) => AutenticarAsync(peticion);
    }
}