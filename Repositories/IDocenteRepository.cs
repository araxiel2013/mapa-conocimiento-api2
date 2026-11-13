using mapa_conocimiento_api.Models;

namespace mapa_conocimiento_api.Repositorios
{
    public interface IDocenteRepository
    {
        Task<IEnumerable<Docente>> ObtenerTodosAsync();
        Task<Docente?> ObtenerPorIdAsync(int cedula);
        Task<Docente?> ObtenerPorCedulaOCorreoIgnorandoFiltrosAsync(int cedula, string correo);
        Task CrearAsync(Docente entidad);
        Task ActualizarAsync(Docente entidad);
        Task EliminarAsync(int cedula);
    }
}
