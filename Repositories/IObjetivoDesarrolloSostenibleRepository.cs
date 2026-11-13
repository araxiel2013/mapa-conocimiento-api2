using mapa_conocimiento_api.Models;

namespace mapa_conocimiento_api.Repositorios
{
    public interface IObjetivoDesarrolloSostenibleRepository
    {
        Task<IEnumerable<ObjetivoDesarrolloSostenible>> ObtenerTodosAsync();
        Task<ObjetivoDesarrolloSostenible?> ObtenerPorIdAsync(int id);
        Task CrearAsync(ObjetivoDesarrolloSostenible entidad);
        Task ActualizarAsync(ObjetivoDesarrolloSostenible entidad);
        Task EliminarAsync(int id);
    }
}
