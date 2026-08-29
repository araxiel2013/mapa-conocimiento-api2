using mapa_conocimiento_api.Models;

namespace mapa_conocimiento_api.Repositorios
{
    // Contrato para el repositorio de aliado (Capa de Acceso a Datos)
    public interface IAliadoRepository
    {
        // Obtiene todos los aliados activos
        Task<IEnumerable<aliado>> ObtenerTodosAsync();
        
        // Obtiene un registro por su NIT (llave primaria no autoincremental)
        Task<aliado?> ObtenerPorIdAsync(int nit);
        
        // Inserta un nuevo aliado
        Task CrearAsync(aliado entidad);
        
        // Actualiza un aliado existente
        Task ActualizarAsync(aliado entidad);
        
        // Ejecuta el borrado lógico dado su NIT
        Task EliminarAsync(int nit);
    }
}