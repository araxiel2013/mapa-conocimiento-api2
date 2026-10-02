using mapa_conocimiento_api.Models;

namespace mapa_conocimiento_api.Repositorios
{
    public interface IProductoRepository
    {
        Task<IEnumerable<producto>> ObtenerTodosAsync();
        Task<producto?> ObtenerPorIdAsync(int id);
        Task CrearAsync(producto entidad);
        Task ActualizarAsync(producto entidad);
        Task EliminarAsync(int id);
    }
}