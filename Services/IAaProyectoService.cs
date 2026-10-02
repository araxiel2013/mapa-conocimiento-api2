using System.Threading.Tasks;
using mapa_conocimiento_api.Peticiones;

namespace mapa_conocimiento_api.Servicios
{
    public interface IAaProyectoService
    {
        Task<bool> CrearAsync(AaProyectoCrear peticion);
        Task<bool> EliminarAsync(int proyecto_id, int area_aplicacion_id);
    }
}
