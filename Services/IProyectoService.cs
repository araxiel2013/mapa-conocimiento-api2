using mapa_conocimiento_api.Models;
using mapa_conocimiento_api.Peticiones;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace mapa_conocimiento_api.Servicios
{
    public interface IProyectoService
    {
        Task<IEnumerable<Proyecto>> ObtenerTodosAsync();
        Task<Proyecto?> ObtenerPorIdAsync(int id);
        Task<Proyecto> CrearAsync(ProyectoCrear peticion);
        Task<bool> ActualizarAsync(int id, ProyectoEditar peticion);
        Task<bool> EliminarAsync(int id);
    }
}
