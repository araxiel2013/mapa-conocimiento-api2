using mapa_conocimiento_api.Models;

namespace mapa_conocimiento_api.Repositorios
{
    public interface IAreaAplicacionRepository
    {
        Task<IEnumerable<AreaAplicacion>> ObtenerTodosAsync();
        Task<AreaAplicacion?> ObtenerPorIdAsync(int id);
        Task CrearAsync(AreaAplicacion entidad);
        Task ActualizarAsync(AreaAplicacion entidad);
        Task EliminarAsync(int id);
    }
}
