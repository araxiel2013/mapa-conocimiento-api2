using mapa_conocimiento_api.Models;

namespace mapa_conocimiento_api.Repositorios
{
    // Contrato para el repositorio de area_conocimiento (Capa de Acceso a Datos)
    public interface IAreaConocimientoRepository
    {
        // Obtiene todos los registros (el filtro de 'activo = true' lo aplicará automáticamente el DbContext)
        Task<IEnumerable<area_conocimiento>> ObtenerTodosAsync();
        
        // Obtiene un registro específico por su llave primaria
        Task<area_conocimiento?> ObtenerPorIdAsync(int id);
        
        // Inserta un nuevo registro en la base de datos
        Task CrearAsync(area_conocimiento entidad);
        
        // Aplica los cambios de un registro modificado
        Task ActualizarAsync(area_conocimiento entidad);
        
        // Ejecuta el borrado lógico del registro dado su ID
        Task EliminarAsync(int id);
    }
}   