using mapa_conocimiento_api.Models;

namespace mapa_conocimiento_api.Repositorios
{
    // Contrato para el repositorio de area_aplicacion (Capa de Acceso a Datos)
    public interface IAreaAplicacionRepository
    {
        // Lista todos los registros activos (el DbContext aplica el filtro automáticamente)
        Task<IEnumerable<area_aplicacion>> ObtenerTodosAsync();
        
        // Obtiene un registro puntual por su ID
        Task<area_aplicacion?> ObtenerPorIdAsync(int id);
        
        // Inserta una nueva área de aplicación
        Task CrearAsync(area_aplicacion entidad);
        
        // Actualiza un área de aplicación existente
        Task ActualizarAsync(area_aplicacion entidad);
        
        // Ejecuta el borrado lógico del registro
        Task EliminarAsync(int id);
    }
}