using mapa_conocimiento_api.Models;
using mapa_conocimiento_api.Peticiones;

namespace mapa_conocimiento_api.Servicios
{
    // Contrato para el servicio de termino_clave (Capa de Reglas de Negocio)
    public interface ITerminoClaveService
    {
        // Lista todos los términos activos
        Task<IEnumerable<termino_clave>> ObtenerTodosAsync();
        
        // Busca un término por su texto exacto (llave primaria)
        Task<termino_clave?> ObtenerPorIdAsync(string termino);
        
        // Mapea y guarda un nuevo término clave
        Task<termino_clave> CrearAsync(TerminoClaveCrear peticion);
        
        // Retorna true si el borrado lógico fue exitoso, o false si el término no existe (404)
        Task<bool> EliminarAsync(string termino);

        // Sin método ActualizarAsync por reglas de negocio de esta entidad
    }
}