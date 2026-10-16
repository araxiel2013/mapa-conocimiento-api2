using mapa_conocimiento_api.Peticiones;

namespace mapa_conocimiento_api.Servicios
{
    public interface IUsuarioService
    {
        Task<IEnumerable<UsuarioResponse>> ObtenerTodosAsync();
        Task<UsuarioResponse?> ObtenerPorIdAsync(int id);
        Task<UsuarioResponse> CrearAsync(UsuarioCrear peticion);
        Task<bool> EliminarAsync(int id);
    }
}