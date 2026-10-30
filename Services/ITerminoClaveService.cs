using mapa_conocimiento_api.Models;
using mapa_conocimiento_api.Peticiones;

namespace mapa_conocimiento_api.Servicios
{
    public interface ITerminoClaveService
    {
        Task<IEnumerable<TerminoClave>> ObtenerTodosAsync();
        Task<TerminoClave?> ObtenerPorIdAsync(string termino);
        Task<TerminoClave> CrearAsync(TerminoClaveCrear peticion);
        Task<bool> EliminarAsync(string termino);
    }
}
