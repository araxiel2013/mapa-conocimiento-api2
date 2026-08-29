using mapa_conocimiento_api.Models;
using mapa_conocimiento_api.Peticiones;
using mapa_conocimiento_api.Repositorios;

namespace mapa_conocimiento_api.Servicios
{
    // Implementación de las reglas de negocio para area_aplicacion
    public class AreaAplicacionService : IAreaAplicacionService
    {
        private readonly IAreaAplicacionRepository _repositorio;

        // Inyección de dependencias del repositorio
        public AreaAplicacionService(IAreaAplicacionRepository repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<IEnumerable<area_aplicacion>> ObtenerTodosAsync()
        {
            return await _repositorio.ObtenerTodosAsync();
        }

        public async Task<area_aplicacion?> ObtenerPorIdAsync(int id)
        {
            return await _repositorio.ObtenerPorIdAsync(id);
        }

        public async Task<area_aplicacion> CrearAsync(AreaAplicacionCrear peticion)
        {
            // Mapeo manual del DTO de entrada hacia la entidad
            var nuevaEntidad = new area_aplicacion
            {
                nombre = peticion.nombre
            };

            await _repositorio.CrearAsync(nuevaEntidad);
            return nuevaEntidad;
        }

        public async Task<bool> ActualizarAsync(int id, AreaAplicacionEditar peticion)
        {
            // Verificamos si existe antes de intentar actualizar
            var entidadExistente = await _repositorio.ObtenerPorIdAsync(id);
            if (entidadExistente == null) 
            {
                return false; 
            }

            // Aplicamos el reemplazo de los datos
            entidadExistente.nombre = peticion.nombre;

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