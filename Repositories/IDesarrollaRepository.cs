using mapa_conocimiento_api.Models;

namespace mapa_conocimiento_api.Repositorios
{
    public interface IDesarrollaRepository
    {
        Task<Desarrolla?> ObtenerPorIdsAsync(int docente_cedula, int proyecto_id);
        Task CrearAsync(Desarrolla entidad);
        Task EliminarAsync(int docente_cedula, int proyecto_id);
    }
}
