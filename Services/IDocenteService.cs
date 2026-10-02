using mapa_conocimiento_api.Peticiones;

namespace mapa_conocimiento_api.Servicios
{
    public interface IDocenteService
    {
        // Nota que ahora devolvemos el Response DTO en lugar de la Entidad directa
        Task<IEnumerable<DocenteResponse>> ObtenerTodosAsync();
        Task<DocenteResponse?> ObtenerPorIdAsync(int cedula);
        Task<DocenteResponse> CrearAsync(DocenteCrear peticion);
        Task<bool> ActualizarAsync(int cedula, DocenteEditar peticion);
        Task<bool> EliminarAsync(int cedula);
    }
}