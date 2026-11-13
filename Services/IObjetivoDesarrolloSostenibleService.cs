using mapa_conocimiento_api.Models;
using mapa_conocimiento_api.Peticiones;

namespace mapa_conocimiento_api.Servicios
{
    public interface IObjetivoDesarrolloSostenibleService
    {
        Task<IEnumerable<ObjetivoDesarrolloSostenible>> ObtenerTodosAsync();
        Task<ObjetivoDesarrolloSostenible?> ObtenerPorIdAsync(int id);
        Task<ObjetivoDesarrolloSostenible> CrearAsync(ObjetivoDesarrolloSostenibleCrear peticion);
        Task<bool> ActualizarAsync(int id, ObjetivoDesarrolloSostenibleEditar peticion);
        Task<bool> EliminarAsync(int id);
    }
}
