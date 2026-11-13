using mapa_conocimiento_api.Models;
using mapa_conocimiento_api.Peticiones;

namespace mapa_conocimiento_api.Servicios
{
    public interface IAliadoService
    {
        Task<IEnumerable<Aliado>> ObtenerTodosAsync();
        Task<Aliado?> ObtenerPorIdAsync(int nit);
        Task<Aliado> CrearAsync(AliadoCrear peticion);
        Task<bool> ActualizarAsync(int nit, AliadoEditar peticion);
        Task<bool> EliminarAsync(int nit);
    }
}
