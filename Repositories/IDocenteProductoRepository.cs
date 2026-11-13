using System.Threading.Tasks;
using mapa_conocimiento_api.Models;

namespace mapa_conocimiento_api.Repositorios
{
    public interface IDocenteProductoRepository
    {
        Task<DocenteProducto?> ObtenerPorIdsAsync(int docente_cedula, int producto_id);
        Task CrearAsync(DocenteProducto entidad);
        Task EliminarAsync(int docente_cedula, int producto_id);
    }
}
