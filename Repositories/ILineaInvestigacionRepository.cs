using mapa_conocimiento_api.Models;

namespace mapa_conocimiento_api.Repositorios
{
    public interface ILineaInvestigacionRepository
    {
        Task<IEnumerable<LineaInvestigacion>> ObtenerTodosAsync();
        Task<LineaInvestigacion?> ObtenerPorIdAsync(int id);
        Task CrearAsync(LineaInvestigacion entidad);
        Task ActualizarAsync(LineaInvestigacion entidad);
        Task EliminarAsync(int id);
    }
}
