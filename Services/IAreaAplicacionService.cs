using mapa_conocimiento_api.Models;
using mapa_conocimiento_api.Peticiones;

namespace mapa_conocimiento_api.Servicios
{
    // Contrato para el servicio de area_aplicacion (Capa de Reglas de Negocio)
    public interface IAreaAplicacionService
    {
        // Lista todas las áreas de aplicación activas
        Task<IEnumerable<area_aplicacion>> ObtenerTodosAsync();
        
        // Busca un área de aplicación por su ID
        Task<area_aplicacion?> ObtenerPorIdAsync(int id);
        
        // Procesa la petición validada, mapea a entidad y la guarda
        Task<area_aplicacion> CrearAsync(AreaAplicacionCrear peticion);
        
        // Retorna true si se actualizó con éxito, o false si el ID no existe (404)
        Task<bool> ActualizarAsync(int id, AreaAplicacionEditar peticion);
        
        // Retorna true si el borrado lógico fue exitoso, o false si el ID no existe (404)
        Task<bool> EliminarAsync(int id);
    }
}