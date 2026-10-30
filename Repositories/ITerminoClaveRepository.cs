using mapa_conocimiento_api.Models;

namespace mapa_conocimiento_api.Repositorios
{
    public interface ITerminoClaveRepository
    {
        Task<IEnumerable<TerminoClave>> ObtenerTodosAsync();
        Task<TerminoClave?> ObtenerPorIdAsync(string termino);
        Task CrearAsync(TerminoClave entidad);
        Task EliminarAsync(string termino);
    }
}
