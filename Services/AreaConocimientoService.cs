using mapa_conocimiento_api.Models;
using mapa_conocimiento_api.Peticiones;
using mapa_conocimiento_api.Repositorios;

namespace mapa_conocimiento_api.Servicios
{
    public class AreaConocimientoService : IAreaConocimientoService
    {
        private readonly IAreaConocimientoRepository _repositorio;

        public AreaConocimientoService(IAreaConocimientoRepository repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<IEnumerable<AreaConocimiento>> ObtenerTodosAsync()
        {
            return await _repositorio.ObtenerTodosAsync();
        }

        public async Task<AreaConocimiento?> ObtenerPorIdAsync(int id)
        {
            return await _repositorio.ObtenerPorIdAsync(id);
        }

        public async Task<AreaConocimiento> CrearAsync(AreaConocimientoCrear peticion)
        {
            var nuevaEntidad = new AreaConocimiento
            {
                gran_area = peticion.gran_area,
                area = peticion.area,
                disciplina = peticion.disciplina
            };

            await _repositorio.CrearAsync(nuevaEntidad);
            return nuevaEntidad;
        }

        public async Task<bool> ActualizarAsync(int id, AreaConocimientoEditar peticion)
        {
            var entidadExistente = await _repositorio.ObtenerPorIdAsync(id);
            if (entidadExistente == null) 
            {
                return false;
            }

            entidadExistente.gran_area = peticion.gran_area;
            entidadExistente.area = peticion.area;
            entidadExistente.disciplina = peticion.disciplina;

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
