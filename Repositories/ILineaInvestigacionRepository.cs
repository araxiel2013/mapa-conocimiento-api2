using mapa_conocimiento_api.Models;

namespace mapa_conocimiento_api.Repositorios
{
    // Contrato para el repositorio de linea_investigacion (Capa de Acceso a Datos)
    public interface ILineaInvestigacionRepository
    {
        // Obtiene todas las líneas de investigación activas
        Task<IEnumerable<linea_investigacion>> ObtenerTodosAsync();
        
        // Obtiene un registro por su ID numérico
        Task<linea_investigacion?> ObtenerPorIdAsync(int id);
        
        // Inserta una nueva línea de investigación
        Task CrearAsync(linea_investigacion entidad);
        
        // Actualiza una línea de investigación existente
        Task ActualizarAsync(linea_investigacion entidad);
        
        // Ejecuta el borrado lógico dado su ID
        Task EliminarAsync(int id);
    }
}