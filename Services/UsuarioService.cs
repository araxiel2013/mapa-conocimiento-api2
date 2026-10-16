using mapa_conocimiento_api.Models;
using mapa_conocimiento_api.Peticiones;
using mapa_conocimiento_api.Repositorios;

namespace mapa_conocimiento_api.Servicios
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repositorio;

        public UsuarioService(IUsuarioRepository repositorio)
        {
            _repositorio = repositorio;
        }

        private UsuarioResponse MapearAResponse(usuario entidad)
        {
            // Extraemos solo los nombres de los roles de la tabla puente
            var roles = entidad.RolesUsuario
                .Where(ru => ru.RolRelacionado != null)
                .Select(ru => ru.RolRelacionado!.nombre)
                .ToList();

            return new UsuarioResponse
            {
                id = entidad.id,
                username = entidad.username,
                email = entidad.email,
                nombre_completo = entidad.nombre_completo,
                activo = entidad.activo,
                roles = roles
            };
        }

        public async Task<IEnumerable<UsuarioResponse>> ObtenerTodosAsync()
        {
            var entidades = await _repositorio.ObtenerTodosAsync();
            return entidades.Select(MapearAResponse);
        }

        public async Task<UsuarioResponse?> ObtenerPorIdAsync(int id)
        {
            var entidad = await _repositorio.ObtenerPorIdAsync(id);
            return entidad == null ? null : MapearAResponse(entidad);
        }

        public async Task<UsuarioResponse> CrearAsync(UsuarioCrear peticion)
        {
            // Validamos si el username o correo ya existen en el sistema (incluyendo borrados lógicos)
            if (await _repositorio.ExisteUsernameOEmailIgnorandoFiltrosAsync(peticion.username, peticion.email))
            {
                bool estaActivo = await _repositorio.ExisteUsernameOEmailAsync(peticion.username, peticion.email);
                if (estaActivo)
                {
                    throw new ArgumentException("El nombre de usuario o correo ya existe y se encuentra activo en el sistema.");
                }
                else
                {
                    throw new ArgumentException("El nombre de usuario o correo pertenece a un usuario inactivo en el sistema.");
                }
            }

            var nuevoUsuario = new usuario
            {
                username = peticion.username,
                // CRÍTICO FASE 3: Encriptamos la contraseña con BCrypt antes de enviarla al repositorio
                password = BCrypt.Net.BCrypt.HashPassword(peticion.password),
                email = peticion.email,
                nombre_completo = peticion.nombre_completo
            };

            await _repositorio.CrearAsync(nuevoUsuario, peticion.roles_ids);

            var entidadCreada = await _repositorio.ObtenerPorIdAsync(nuevoUsuario.id);
            return MapearAResponse(entidadCreada!);
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var entidadExistente = await _repositorio.ObtenerPorIdAsync(id);
            if (entidadExistente == null) return false;

            await _repositorio.EliminarAsync(id);
            return true;
        }
    }
}