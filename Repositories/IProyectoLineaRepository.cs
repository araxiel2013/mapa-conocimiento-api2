using System.Threading.Tasks;
using mapa_conocimiento_api.Models;

namespace mapa_conocimiento_api.Repositorios
{
    public interface IProyectoLineaRepository
    {
        Task<proyecto_linea?> ObtenerPorIdsAsync(int proyecto_id, int linea_investigacion_id);
        Task CrearAsync(proyecto_linea entidad);
        Task EliminarAsync(int proyecto_id, int linea_investigacion_id);
    }
}
