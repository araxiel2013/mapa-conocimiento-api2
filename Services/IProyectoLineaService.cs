using System.Threading.Tasks;
using mapa_conocimiento_api.Models;
using mapa_conocimiento_api.Peticiones;

namespace mapa_conocimiento_api.Servicios
{
    public interface IProyectoLineaService
    {
        Task<bool> CrearAsync(ProyectoLineaCrear peticion);
        Task<bool> EliminarAsync(int proyecto_id, int linea_investigacion_id);
    }
}
