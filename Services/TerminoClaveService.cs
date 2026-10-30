using mapa_conocimiento_api.Models;
using mapa_conocimiento_api.Peticiones;
using mapa_conocimiento_api.Repositorios;

namespace mapa_conocimiento_api.Servicios
{
    public class TerminoClaveService : ITerminoClaveService
    {
        private readonly ITerminoClaveRepository _repositorio;

        public TerminoClaveService(ITerminoClaveRepository repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<IEnumerable<TerminoClave>> ObtenerTodosAsync()
        {
            return await _repositorio.ObtenerTodosAsync();
        }

        public async Task<TerminoClave?> ObtenerPorIdAsync(string termino)
        {
            return await _repositorio.ObtenerPorIdAsync(termino);
        }

        public async Task<TerminoClave> CrearAsync(TerminoClaveCrear peticion)
        {
            if (string.IsNullOrWhiteSpace(peticion.termino))
            {
                throw new ArgumentException("El campo termino es obligatorio y no puede contener únicamente espacios.");
            }

            var nuevaEntidad = new TerminoClave
            {
                termino = peticion.termino.Trim()
            };

            await _repositorio.CrearAsync(nuevaEntidad);
            return nuevaEntidad;
        }

        public async Task<bool> EliminarAsync(string termino)
        {
            var entidadExistente = await _repositorio.ObtenerPorIdAsync(termino);
            if (entidadExistente == null) 
            {
                return false; 
            }

            await _repositorio.EliminarAsync(termino);
            return true;
        }
    }
}
