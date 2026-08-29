using mapa_conocimiento_api.Models;

namespace mapa_conocimiento_api.Repositorios
{
    // Contrato para el repositorio de objetivo_desarrollo_sostenible (Capa de Acceso a Datos)
    public interface IObjetivoDesarrolloSostenibleRepository
    {
        // Obtiene todos los registros activos
        Task<IEnumerable<objetivo_desarrollo_sostenible>> ObtenerTodosAsync();
        
        // Obtiene un registro por su ID
        Task<objetivo_desarrollo_sostenible?> ObtenerPorIdAsync(int id);
        
        // Inserta un nuevo ODS
        Task CrearAsync(objetivo_desarrollo_sostenible entidad);
        
        // Actualiza los datos de un ODS
        Task ActualizarAsync(objetivo_desarrollo_sostenible entidad);
        
        // Ejecuta el borrado lógico del ODS
        Task EliminarAsync(int id);
    }
}