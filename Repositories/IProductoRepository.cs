using mapa_conocimiento_api.Models;

namespace mapa_conocimiento_api.Repositorios
{
    public interface IProductoRepository
    {
        Task<IEnumerable<Producto>> ObtenerTodosAsync();
        Task<Producto?> ObtenerPorIdAsync(int id);
        Task CrearAsync(Producto entidad);
        Task ActualizarAsync(Producto entidad);
        Task EliminarAsync(int id);
    }
}
