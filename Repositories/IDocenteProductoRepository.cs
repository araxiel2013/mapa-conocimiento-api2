using System.Threading.Tasks;
using mapa_conocimiento_api.Models;

namespace mapa_conocimiento_api.Repositorios
{
    public interface IDocenteProductoRepository
    {
        Task<docente_producto?> ObtenerPorIdsAsync(int docente_cedula, int producto_id);
        Task CrearAsync(docente_producto entidad);
        Task EliminarAsync(int docente_cedula, int producto_id);
    }
}
