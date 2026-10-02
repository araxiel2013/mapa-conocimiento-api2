using mapa_conocimiento_api.Models;

namespace mapa_conocimiento_api.Repositorios
{
    public interface IAliadoProyectoRepository
    {
        // Usamos ambas llaves para buscar el registro exacto
        Task<aliado_proyecto?> ObtenerPorIdsAsync(int aliado_nit, int proyecto_id);
        Task CrearAsync(aliado_proyecto entidad);
        Task EliminarAsync(int aliado_nit, int proyecto_id);
    }
}