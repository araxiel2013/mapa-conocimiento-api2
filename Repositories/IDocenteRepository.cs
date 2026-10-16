using mapa_conocimiento_api.Models;

namespace mapa_conocimiento_api.Repositorios
{
    public interface IDocenteRepository
    {
        Task<IEnumerable<docente>> ObtenerTodosAsync();
        Task<docente?> ObtenerPorIdAsync(int cedula);
        Task<docente?> ObtenerPorCedulaOCorreoIgnorandoFiltrosAsync(int cedula, string correo);
        Task CrearAsync(docente entidad);
        Task ActualizarAsync(docente entidad);
        Task EliminarAsync(int cedula);
    }
}