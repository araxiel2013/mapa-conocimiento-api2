using mapa_conocimiento_api.Peticiones;

namespace mapa_conocimiento_api.Servicios
{
    public interface IAuthService
    {
        Task<LoginResponse?> AutenticarAsync(LoginRequest peticion);
    }
}