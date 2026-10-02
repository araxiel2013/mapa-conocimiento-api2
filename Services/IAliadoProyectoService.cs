using mapa_conocimiento_api.Peticiones;

namespace mapa_conocimiento_api.Servicios
{
    public interface IAliadoProyectoService
    {
        Task<bool> CrearAsync(AliadoProyectoCrear peticion);
        Task<bool> EliminarAsync(int aliado_nit, int proyecto_id);
    }
}