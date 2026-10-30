using mapa_conocimiento_api.Models;
using mapa_conocimiento_api.Peticiones;
using mapa_conocimiento_api.Repositorios;

namespace mapa_conocimiento_api.Servicios
{
    public class ObjetivoDesarrolloSostenibleService : IObjetivoDesarrolloSostenibleService
    {
        private readonly IObjetivoDesarrolloSostenibleRepository _repositorio;

        public ObjetivoDesarrolloSostenibleService(IObjetivoDesarrolloSostenibleRepository repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<IEnumerable<ObjetivoDesarrolloSostenible>> ObtenerTodosAsync()
        {
            return await _repositorio.ObtenerTodosAsync();
        }

        public async Task<ObjetivoDesarrolloSostenible?> ObtenerPorIdAsync(int id)
        {
            return await _repositorio.ObtenerPorIdAsync(id);
        }

        public async Task<ObjetivoDesarrolloSostenible> CrearAsync(ObjetivoDesarrolloSostenibleCrear peticion)
        {
            var nuevaEntidad = new ObjetivoDesarrolloSostenible
            {
                nombre = peticion.nombre,
                categoria = peticion.categoria
            };

            await _repositorio.CrearAsync(nuevaEntidad);
            return nuevaEntidad;
        }

        public async Task<bool> ActualizarAsync(int id, ObjetivoDesarrolloSostenibleEditar peticion)
        {
            var entidadExistente = await _repositorio.ObtenerPorIdAsync(id);
            if (entidadExistente == null) 
            {
                return false; 
            }

            entidadExistente.nombre = peticion.nombre;
            entidadExistente.categoria = peticion.categoria;

            await _repositorio.ActualizarAsync(entidadExistente);
            return true;
        }

        public async Task<bool> EliminarAsync(int id)
        {
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
