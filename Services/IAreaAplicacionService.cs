using mapa_conocimiento_api.Models;
using mapa_conocimiento_api.Peticiones;

namespace mapa_conocimiento_api.Servicios
{
    public interface IAreaAplicacionService
    {
        Task<IEnumerable<AreaAplicacion>> ObtenerTodosAsync();
        Task<AreaAplicacion?> ObtenerPorIdAsync(int id);
        Task<AreaAplicacion> CrearAsync(AreaAplicacionCrear peticion);
        Task<bool> ActualizarAsync(int id, AreaAplicacionEditar peticion);
        Task<bool> EliminarAsync(int id);
    }
}
