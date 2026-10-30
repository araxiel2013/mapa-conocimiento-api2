using mapa_conocimiento_api.Models;
using mapa_conocimiento_api.Peticiones;
using mapa_conocimiento_api.Repositorios;

namespace mapa_conocimiento_api.Servicios
{
    public class LineaInvestigacionService : ILineaInvestigacionService
    {
        private readonly ILineaInvestigacionRepository _repositorio;

        public LineaInvestigacionService(ILineaInvestigacionRepository repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<IEnumerable<LineaInvestigacion>> ObtenerTodosAsync()
        {
            return await _repositorio.ObtenerTodosAsync();
        }

        public async Task<LineaInvestigacion?> ObtenerPorIdAsync(int id)
        {
            return await _repositorio.ObtenerPorIdAsync(id);
        }

        public async Task<LineaInvestigacion> CrearAsync(LineaInvestigacionCrear peticion)
        {
            var nuevaEntidad = new LineaInvestigacion
            {
                nombre = peticion.nombre,
                descripcion = peticion.descripcion,
                activo = true
            };

            await _repositorio.CrearAsync(nuevaEntidad);
            return nuevaEntidad;
        }

        public async Task<bool> ActualizarAsync(int id, LineaInvestigacionEditar peticion)
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
