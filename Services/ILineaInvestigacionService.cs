using mapa_conocimiento_api.Models;
using mapa_conocimiento_api.Peticiones;

namespace mapa_conocimiento_api.Servicios
{
    // Contrato para el servicio de linea_investigacion (Capa de Reglas de Negocio)
    public interface ILineaInvestigacionService
    {
        // Lista todas las líneas de investigación activas
        Task<IEnumerable<linea_investigacion>> ObtenerTodosAsync();
        
        // Busca una línea de investigación por su ID
        Task<linea_investigacion?> ObtenerPorIdAsync(int id);
        
        // Procesa la petición validada, la mapea a entidad y la guarda
        Task<linea_investigacion> CrearAsync(LineaInvestigacionCrear peticion);
        
        // Retorna true si se actualizó con éxito, o false si el ID no existe (404)
        Task<bool> ActualizarAsync(int id, LineaInvestigacionEditar peticion);
        
        // Retorna true si el borrado lógico fue exitoso, o false si el ID no existe (404)
        Task<bool> EliminarAsync(int id);
    }
}