using System.Threading.Tasks;
using mapa_conocimiento_api.Models;

namespace mapa_conocimiento_api.Repositorios
{
    public interface IOdsProyectoRepository
    {
        Task<OdsProyecto?> ObtenerPorIdsAsync(int proyecto_id, int ods_id);
        Task CrearAsync(OdsProyecto entidad);
        Task EliminarAsync(int proyecto_id, int ods_id);
    }
}
