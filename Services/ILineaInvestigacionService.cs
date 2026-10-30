using mapa_conocimiento_api.Models;
using mapa_conocimiento_api.Peticiones;

namespace mapa_conocimiento_api.Servicios
{
    public interface ILineaInvestigacionService
    {
        Task<IEnumerable<LineaInvestigacion>> ObtenerTodosAsync();
        Task<LineaInvestigacion?> ObtenerPorIdAsync(int id);
        Task<LineaInvestigacion> CrearAsync(LineaInvestigacionCrear peticion);
        Task<bool> ActualizarAsync(int id, LineaInvestigacionEditar peticion);
        Task<bool> EliminarAsync(int id);
    }
}
