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
            var secretKey = jwtSettings.GetValue<string>("Key");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: jwtSettings.GetValue<string>("Issuer"),
                audience: jwtSettings.GetValue<string>("Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(jwtSettings.GetValue<double>("DurationInMinutes")),
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