using System.Threading.Tasks;
using mapa_conocimiento_api.Models;

namespace mapa_conocimiento_api.Repositorios
{
    public interface IOdsProyectoRepository
    {
        Task<ods_proyecto?> ObtenerPorIdsAsync(int proyecto_id, int ods_id);
        Task CrearAsync(ods_proyecto entidad);
        Task EliminarAsync(int proyecto_id, int ods_id);
    }
}
