using System.Threading.Tasks;
using mapa_conocimiento_api.Models;
using mapa_conocimiento_api.Peticiones;

namespace mapa_conocimiento_api.Servicios
{
    public interface IPalabrasClaveService
    {
        Task<bool> CrearAsync(PalabrasClaveCrear peticion);
        Task<bool> EliminarAsync(int proyecto_id, string termino_clave);
    }
}
