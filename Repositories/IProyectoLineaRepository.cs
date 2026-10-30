using System.Threading.Tasks;
using mapa_conocimiento_api.Models;

namespace mapa_conocimiento_api.Repositorios
{
    public interface IProyectoLineaRepository
    {
        Task<ProyectoLinea?> ObtenerPorIdsAsync(int proyecto_id, int linea_investigacion_id);
        Task CrearAsync(ProyectoLinea entidad);
        Task EliminarAsync(int proyecto_id, int linea_investigacion_id);
    }
}
