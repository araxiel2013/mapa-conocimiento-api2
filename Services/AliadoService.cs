using mapa_conocimiento_api.Models;
using mapa_conocimiento_api.Peticiones;
using mapa_conocimiento_api.Repositorios;

namespace mapa_conocimiento_api.Servicios
{
    // Implementación de las reglas de negocio para aliado
    public class AliadoService : IAliadoService
    {
        private readonly IAliadoRepository _repositorio;

        // Inyección de dependencias del repositorio
        public AliadoService(IAliadoRepository repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<IEnumerable<aliado>> ObtenerTodosAsync()
        {
            return await _repositorio.ObtenerTodosAsync();
        }

        public async Task<aliado?> ObtenerPorIdAsync(int nit)
        {
            return await _repositorio.ObtenerPorIdAsync(nit);
        }

        public async Task<aliado> CrearAsync(AliadoCrear peticion)
        {
            // Mapeo manual del DTO de entrada hacia la entidad
            // CRÍTICO: Asignamos el nit porque la llave primaria no es autoincremental
            var nuevaEntidad = new aliado
            {
                nit = peticion.nit,
                nombre = peticion.nombre
            };

            await _repositorio.CrearAsync(nuevaEntidad);
            return nuevaEntidad;
        }

        public async Task<bool> ActualizarAsync(int nit, AliadoEditar peticion)
        {
            // Verificamos si existe antes de intentar actualizar
            var entidadExistente = await _repositorio.ObtenerPorIdAsync(nit);
            if (entidadExistente == null) 
            {
                return false; 
            }

            // Aplicamos el reemplazo de los datos editables
            entidadExistente.nombre = peticion.nombre;

            await _repositorio.ActualizarAsync(entidadExistente);
            return true;
        }

        public async Task<bool> EliminarAsync(int nit)
        {
            // Verificamos si existe antes de intentar hacer el borrado lógico
            var entidadExistente = await _repositorio.ObtenerPorIdAsync(nit);
            if (entidadExistente == null) 
            {
                return false; 
            }

            await _repositorio.EliminarAsync(nit);
            return true;
        }
    }
}