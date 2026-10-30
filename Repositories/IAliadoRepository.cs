using mapa_conocimiento_api.Models;

namespace mapa_conocimiento_api.Repositorios
{
    public interface IAliadoRepository
    {
        Task<IEnumerable<Aliado>> ObtenerTodosAsync();
        Task<Aliado?> ObtenerPorIdAsync(int nit);
        Task CrearAsync(Aliado entidad);
        Task ActualizarAsync(Aliado entidad);
        Task EliminarAsync(int nit);
    }
}
