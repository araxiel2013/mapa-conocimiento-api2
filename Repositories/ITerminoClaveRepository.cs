using mapa_conocimiento_api.Models;

namespace mapa_conocimiento_api.Repositorios
{
    // Contrato para el repositorio de termino_clave (Capa de Acceso a Datos)
    public interface ITerminoClaveRepository
    {
        // Lista todos los registros activos
        Task<IEnumerable<termino_clave>> ObtenerTodosAsync();
        
        // Obtiene un registro puntual. La llave primaria es un string (el término en sí).
        Task<termino_clave?> ObtenerPorIdAsync(string termino);
        
        // Inserta un nuevo término clave
        Task CrearAsync(termino_clave entidad);
        
        // Ejecuta el borrado lógico del registro mediante su llave primaria string
        Task EliminarAsync(string termino);

        // Nota: Esta entidad NO tiene método ActualizarAsync por regla de negocio
    }
}