using mapa_conocimiento_api.Models;
using mapa_conocimiento_api.Peticiones;
using mapa_conocimiento_api.Repositorios;

namespace mapa_conocimiento_api.Servicios
{
    // Implementación de las reglas de negocio para area_conocimiento
    public class AreaConocimientoService : IAreaConocimientoService
    {
        private readonly IAreaConocimientoRepository _repositorio;

        // Inyección de dependencias del repositorio
        public AreaConocimientoService(IAreaConocimientoRepository repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<IEnumerable<area_conocimiento>> ObtenerTodosAsync()
        {
            return await _repositorio.ObtenerTodosAsync();
        }

        public async Task<area_conocimiento?> ObtenerPorIdAsync(int id)
        {
            return await _repositorio.ObtenerPorIdAsync(id);
        }

        public async Task<area_conocimiento> CrearAsync(AreaConocimientoCrear peticion)
        {
            // Mapeo manual del DTO de entrada hacia la entidad de dominio
            var nuevaEntidad = new area_conocimiento
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
            // Verificamos si existe antes de intentar actualizar
            var entidadExistente = await _repositorio.ObtenerPorIdAsync(id);
            if (entidadExistente == null) 
            {
                return false; // El controlador lo interpretará como un 404 Not Found
            }

            // Aplicamos el reemplazo completo de los datos (regla del PUT)
            entidadExistente.gran_area = peticion.gran_area;
            entidadExistente.area = peticion.area;
            entidadExistente.disciplina = peticion.disciplina;

            await _repositorio.ActualizarAsync(entidadExistente);
            return true;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            // Verificamos si existe antes de intentar hacer el borrado lógico
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