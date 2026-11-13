using System.Threading.Tasks;
using mapa_conocimiento_api.Models;

namespace mapa_conocimiento_api.Repositorios
{
    public interface IAaProyectoRepository
    {
        Task<AaProyecto?> ObtenerPorIdsAsync(int proyecto_id, int area_aplicacion_id);
        Task CrearAsync(AaProyecto entidad);
        Task EliminarAsync(int proyecto_id, int area_aplicacion_id);
    }
}
