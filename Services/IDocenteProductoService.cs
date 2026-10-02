using System.Threading.Tasks;
using mapa_conocimiento_api.Peticiones;

namespace mapa_conocimiento_api.Servicios
{
    public interface IDocenteProductoService
    {
        Task<bool> CrearAsync(DocenteProductoCrear peticion);
        Task<bool> EliminarAsync(int docente_cedula, int producto_id);
    }
}
