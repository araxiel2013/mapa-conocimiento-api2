using System.Threading.Tasks;
using mapa_conocimiento_api.Models;

namespace mapa_conocimiento_api.Repositorios
{
    public interface IAcProyectoRepository
    {
        Task<ac_proyecto?> ObtenerPorIdsAsync(int proyecto_id, int area_conocimiento_id);
        Task CrearAsync(ac_proyecto entidad);
        Task EliminarAsync(int proyecto_id, int area_conocimiento_id);
    }
}
