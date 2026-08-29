using mapa_conocimiento_api.Models;
using mapa_conocimiento_api.Peticiones;
using mapa_conocimiento_api.Repositorios;

namespace mapa_conocimiento_api.Servicios
{
    // Implementación de las reglas de negocio para objetivo_desarrollo_sostenible
    public class ObjetivoDesarrolloSostenibleService : IObjetivoDesarrolloSostenibleService
    {
        private readonly IObjetivoDesarrolloSostenibleRepository _repositorio;

        // Inyección del repositorio
        public ObjetivoDesarrolloSostenibleService(IObjetivoDesarrolloSostenibleRepository repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<IEnumerable<objetivo_desarrollo_sostenible>> ObtenerTodosAsync()
        {
            return await _repositorio.ObtenerTodosAsync();
        }

        public async Task<objetivo_desarrollo_sostenible?> ObtenerPorIdAsync(int id)
        {
            return await _repositorio.ObtenerPorIdAsync(id);
        }

        public async Task<objetivo_desarrollo_sostenible> CrearAsync(ObjetivoDesarrolloSostenibleCrear peticion)
        {
            // Mapeo manual del DTO de entrada hacia la entidad
            var nuevaEntidad = new objetivo_desarrollo_sostenible
            {
                nombre = peticion.nombre,
                categoria = peticion.categoria
            };

            await _repositorio.CrearAsync(nuevaEntidad);
            return nuevaEntidad;
        }

        public async Task<bool> ActualizarAsync(int id, ObjetivoDesarrolloSostenibleEditar peticion)
        {
            // Verificamos existencia para evitar errores y permitir el 404 en el controlador
            var entidadExistente = await _repositorio.ObtenerPorIdAsync(id);
            if (entidadExistente == null) 
            {
                return false; 
            }

            // Reemplazo total de campos (PUT)
            entidadExistente.nombre = peticion.nombre;
            entidadExistente.categoria = peticion.categoria;

            await _repositorio.ActualizarAsync(entidadExistente);
            return true;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            // Verificamos existencia antes del borrado lógico
            var entidadExistente = await _repositorio.ObtenerPorIdAsync(id);
            if (entidadExistente == null) 
            {
                return false; 
            }

            await _repositorio.EliminarAsync(id);
            return true;
        }
    }
}