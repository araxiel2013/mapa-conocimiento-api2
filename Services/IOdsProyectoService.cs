using System.Threading.Tasks;
using mapa_conocimiento_api.Peticiones;

namespace mapa_conocimiento_api.Servicios
{
    public interface IOdsProyectoService
    {
        Task<bool> CrearAsync(OdsProyectoCrear peticion);
        Task<bool> EliminarAsync(int proyecto_id, int ods_id);
    }
}
