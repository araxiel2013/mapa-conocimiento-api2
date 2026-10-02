using System.Threading.Tasks;
using mapa_conocimiento_api.Models;

namespace mapa_conocimiento_api.Repositorios
{
    public interface IAaProyectoRepository
    {
        Task<aa_proyecto?> ObtenerPorIdsAsync(int proyecto_id, int area_aplicacion_id);
        Task CrearAsync(aa_proyecto entidad);
        Task EliminarAsync(int proyecto_id, int area_aplicacion_id);
    }
}
