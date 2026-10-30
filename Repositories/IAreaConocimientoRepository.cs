using mapa_conocimiento_api.Models;

namespace mapa_conocimiento_api.Repositorios
{
    public interface IAreaConocimientoRepository
    {
        Task<IEnumerable<AreaConocimiento>> ObtenerTodosAsync();
        Task<AreaConocimiento?> ObtenerPorIdAsync(int id);
        Task CrearAsync(AreaConocimiento entidad);
        Task ActualizarAsync(AreaConocimiento entidad);
        Task EliminarAsync(int id);
    }
}
