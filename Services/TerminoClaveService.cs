using mapa_conocimiento_api.Models;
using mapa_conocimiento_api.Peticiones;
using mapa_conocimiento_api.Repositorios;

namespace mapa_conocimiento_api.Servicios
{
    // Implementación de las reglas de negocio para termino_clave
    public class TerminoClaveService : ITerminoClaveService
    {
        private readonly ITerminoClaveRepository _repositorio;

        public TerminoClaveService(ITerminoClaveRepository repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<IEnumerable<termino_clave>> ObtenerTodosAsync()
        {
            return await _repositorio.ObtenerTodosAsync();
        }

        public async Task<termino_clave?> ObtenerPorIdAsync(string termino)
        {
            return await _repositorio.ObtenerPorIdAsync(termino);
        }

        public async Task<termino_clave> CrearAsync(TerminoClaveCrear peticion)
        {
            // El campo recibido en el DTO se convierte en la llave primaria de la entidad
            var nuevaEntidad = new termino_clave
            {
                termino = peticion.termino
            };

            await _repositorio.CrearAsync(nuevaEntidad);
            return nuevaEntidad;
        }

        public async Task<bool> EliminarAsync(string termino)
        {
            // Verificamos si existe buscando por la llave primaria tipo string
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