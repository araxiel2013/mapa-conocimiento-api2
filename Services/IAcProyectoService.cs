using System.Threading.Tasks;
using mapa_conocimiento_api.Models;
using mapa_conocimiento_api.Peticiones;

namespace mapa_conocimiento_api.Servicios
{
    public interface IAcProyectoService
    {
        Task<bool> CrearAsync(AcProyectoCrear peticion);
        Task<bool> EliminarAsync(int proyecto_id, int area_conocimiento_id);
    }
}
