using mapa_conocimiento_api.Models;
using mapa_conocimiento_api.Peticiones;
using mapa_conocimiento_api.Repositorios;

namespace mapa_conocimiento_api.Servicios
{
    // Implementación de las reglas de negocio para linea_investigacion
    public class LineaInvestigacionService : ILineaInvestigacionService
    {
        private readonly ILineaInvestigacionRepository _repositorio;

        // Inyección de dependencias del repositorio
        public LineaInvestigacionService(ILineaInvestigacionRepository repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<IEnumerable<linea_investigacion>> ObtenerTodosAsync()
        {
            return await _repositorio.ObtenerTodosAsync();
        }

        public async Task<linea_investigacion?> ObtenerPorIdAsync(int id)
        {
            return await _repositorio.ObtenerPorIdAsync(id);
        }

        public async Task<linea_investigacion> CrearAsync(LineaInvestigacionCrear peticion)
        {
            // Mapeo manual del DTO de entrada hacia la entidad
        var nuevaEntidad = new linea_investigacion
        {
            nombre = peticion.nombre,
            descripcion = peticion.descripcion, // <--- Verifica que esta línea exista
            activo = true
        };

            await _repositorio.CrearAsync(nuevaEntidad);
            return nuevaEntidad;
        }

        public async Task<bool> ActualizarAsync(int id, LineaInvestigacionEditar peticion)
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