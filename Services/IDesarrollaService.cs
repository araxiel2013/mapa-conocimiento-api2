using mapa_conocimiento_api.Peticiones;

namespace mapa_conocimiento_api.Servicios
{
    public interface IDesarrollaService
    {
        Task<bool> CrearAsync(DesarrollaCrear peticion);
        Task<bool> EliminarAsync(int docente_cedula, int proyecto_id);
    }
}