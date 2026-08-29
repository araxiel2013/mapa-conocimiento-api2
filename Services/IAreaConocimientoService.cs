using mapa_conocimiento_api.Models;
using mapa_conocimiento_api.Peticiones;

namespace mapa_conocimiento_api.Servicios
{
    // Contrato para el servicio de area_conocimiento (Capa de Reglas de Negocio)
    public interface IAreaConocimientoService
    {
        // Lista todos los registros activos
        Task<IEnumerable<area_conocimiento>> ObtenerTodosAsync();
        
        // Busca un registro puntual; retorna null si no existe
        Task<area_conocimiento?> ObtenerPorIdAsync(int id);
        
        // Recibe el DTO validado, aplica la lógica de negocio y retorna la entidad creada
        Task<area_conocimiento> CrearAsync(AreaConocimientoCrear peticion);
        
        // Retorna true si la actualización fue exitosa, o false si el ID no existe (404)
        Task<bool> ActualizarAsync(int id, AreaConocimientoEditar peticion);
        
        // Retorna true si el borrado lógico fue exitoso, o false si el ID no existe (404)
        Task<bool> EliminarAsync(int id);
    }
}