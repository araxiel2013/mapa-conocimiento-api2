using mapa_conocimiento_api.Models;
using mapa_conocimiento_api.Peticiones;

namespace mapa_conocimiento_api.Servicios
{
    // Contrato para el servicio de ODS (Capa de Reglas de Negocio)
    public interface IObjetivoDesarrolloSostenibleService
    {
        // Lista todos los ODS activos
        Task<IEnumerable<objetivo_desarrollo_sostenible>> ObtenerTodosAsync();
        
        // Busca un ODS por su llave primaria
        Task<objetivo_desarrollo_sostenible?> ObtenerPorIdAsync(int id);
        
        // Procesa la petición validada, mapea a entidad y la guarda
        Task<objetivo_desarrollo_sostenible> CrearAsync(ObjetivoDesarrolloSostenibleCrear peticion);
        
        // Retorna true si se actualizó con éxito, o false si el ID no existe (404)
        Task<bool> ActualizarAsync(int id, ObjetivoDesarrolloSostenibleEditar peticion);
        
        // Retorna true si el borrado lógico fue exitoso, o false si el ID no existe (404)
        Task<bool> EliminarAsync(int id);
    }
}