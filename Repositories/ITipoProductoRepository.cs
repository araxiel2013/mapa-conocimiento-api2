using System.Collections.Generic;
using System.Threading.Tasks;
using mapa_conocimiento_api.Models;

namespace mapa_conocimiento_api.Repositorios
{
    public interface ITipoProductoRepository
    {
        Task<IEnumerable<tipo_producto>> ObtenerTodosAsync();
        Task<tipo_producto?> ObtenerPorIdAsync(int id);
        Task CrearAsync(tipo_producto entidad);
        Task ActualizarAsync(tipo_producto entidad);
        Task EliminarAsync(int id);
    }
}

