using System.Collections.Generic;
using System.Threading.Tasks;
using mapa_conocimiento_api.Models;

namespace mapa_conocimiento_api.Repositorios
{
    public interface ITipoProductoRepository
    {
        Task<IEnumerable<TipoProducto>> ObtenerTodosAsync();
        Task<TipoProducto?> ObtenerPorIdAsync(int id);
        Task CrearAsync(TipoProducto entidad);
        Task ActualizarAsync(TipoProducto entidad);
        Task EliminarAsync(int id);
    }
}
