using mapa_conocimiento_api.Models;
using mapa_conocimiento_api.Peticiones;

namespace mapa_conocimiento_api.Servicios
{
    public interface IAreaConocimientoService
    {
        Task<IEnumerable<AreaConocimiento>> ObtenerTodosAsync();
        Task<AreaConocimiento?> ObtenerPorIdAsync(int id);
        Task<AreaConocimiento> CrearAsync(AreaConocimientoCrear peticion);
        Task<bool> ActualizarAsync(int id, AreaConocimientoEditar peticion);
        Task<bool> EliminarAsync(int id);
    }
}
