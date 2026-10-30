using mapa_conocimiento_api.Models;
using mapa_conocimiento_api.Peticiones;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace mapa_conocimiento_api.Servicios
{
    public interface ITipoProductoService
    {
        Task<IEnumerable<TipoProducto>> ObtenerTodosAsync();
        Task<TipoProducto?> ObtenerPorIdAsync(int id);
        Task<TipoProducto> CrearAsync(TipoProductoCrear peticion);
        Task<bool> ActualizarAsync(int id, TipoProductoEditar peticion);
        Task<bool> EliminarAsync(int id);
    }
}
