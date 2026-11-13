using mapa_conocimiento_api.Models;
using mapa_conocimiento_api.Peticiones;
using mapa_conocimiento_api.Repositorios;

namespace mapa_conocimiento_api.Servicios
{
    public class AreaAplicacionService : IAreaAplicacionService
    {
        private readonly IAreaAplicacionRepository _repositorio;

        public AreaAplicacionService(IAreaAplicacionRepository repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<IEnumerable<AreaAplicacion>> ObtenerTodosAsync()
        {
            return await _repositorio.ObtenerTodosAsync();
        }

        public async Task<AreaAplicacion?> ObtenerPorIdAsync(int id)
        {
            return await _repositorio.ObtenerPorIdAsync(id);
        }

        public async Task<AreaAplicacion> CrearAsync(AreaAplicacionCrear peticion)
        {
            var nuevaEntidad = new AreaAplicacion
            {
                nombre = peticion.nombre
            };

            await _repositorio.CrearAsync(nuevaEntidad);
            return nuevaEntidad;
        }

        public async Task<bool> ActualizarAsync(int id, AreaAplicacionEditar peticion)
        {
            var entidadExistente = await _repositorio.ObtenerPorIdAsync(id);
            if (entidadExistente == null) 
            {
                return false; 
            }

            entidadExistente.nombre = peticion.nombre;

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
