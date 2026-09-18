using mapa_conocimiento_api.Models;
using mapa_conocimiento_api.Peticiones;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace mapa_conocimiento_api.Servicios
{
    public interface IProyectoService
    {
        Task<IEnumerable<proyecto>> ObtenerTodosAsync();
        Task<proyecto?> ObtenerPorIdAsync(int id);
        Task<proyecto> CrearAsync(ProyectoCrear peticion);
        Task<bool> ActualizarAsync(int id, ProyectoEditar peticion);
        Task<bool> EliminarAsync(int id);
    }
}

