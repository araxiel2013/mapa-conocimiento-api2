using mapa_conocimiento_api.Models;

namespace mapa_conocimiento_api.Repositorios
{
    public interface IAliadoProyectoRepository
    {
        Task<AliadoProyecto?> ObtenerPorIdsAsync(int aliado_nit, int proyecto_id);
        Task CrearAsync(AliadoProyecto entidad);
        Task EliminarAsync(int aliado_nit, int proyecto_id);
    }
}
