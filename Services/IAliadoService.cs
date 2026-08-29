using mapa_conocimiento_api.Models;
using mapa_conocimiento_api.Peticiones;

namespace mapa_conocimiento_api.Servicios
{
    // Contrato para el servicio de aliado (Capa de Reglas de Negocio)
    public interface IAliadoService
    {
        // Lista todos los aliados activos
        Task<IEnumerable<aliado>> ObtenerTodosAsync();
        
        // Busca un aliado por su NIT
        Task<aliado?> ObtenerPorIdAsync(int nit);
        
        // Procesa la petición validada, mapea a entidad y la guarda
        Task<aliado> CrearAsync(AliadoCrear peticion);
        
        // Retorna true si se actualizó con éxito, o false si el NIT no existe (404)
        Task<bool> ActualizarAsync(int nit, AliadoEditar peticion);
        
        // Retorna true si el borrado lógico fue exitoso, o false si el NIT no existe (404)
        Task<bool> EliminarAsync(int nit);
    }
}