using mapa_conocimiento_api.Models;
using mapa_conocimiento_api.Peticiones;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace mapa_conocimiento_api.Servicios
{
    public interface ITipoProductoService
    {
        Task<IEnumerable<tipo_producto>> ObtenerTodosAsync();
        Task<tipo_producto?> ObtenerPorIdAsync(int id);
        Task<tipo_producto> CrearAsync(TipoProductoCrear peticion);
        Task<bool> ActualizarAsync(int id, TipoProductoEditar peticion);
        Task<bool> EliminarAsync(int id);
    }
}

