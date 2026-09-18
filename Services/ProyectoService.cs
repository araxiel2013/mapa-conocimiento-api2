using mapa_conocimiento_api.Models;
using mapa_conocimiento_api.Peticiones;
using mapa_conocimiento_api.Repositorios;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace mapa_conocimiento_api.Servicios
{
    public class ProyectoService : IProyectoService
    {
        private readonly IProyectoRepository _repositorio;

        public ProyectoService(IProyectoRepository repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<IEnumerable<proyecto>> ObtenerTodosAsync() => await _repositorio.ObtenerTodosAsync();

        public async Task<proyecto?> ObtenerPorIdAsync(int id) => await _repositorio.ObtenerPorIdAsync(id);

        public async Task<proyecto> CrearAsync(ProyectoCrear peticion)
        {
            var nuevaEntidad = new proyecto { 
                nombre = peticion.titulo,
                activo = true
            };
            await _repositorio.CrearAsync(nuevaEntidad);
            return nuevaEntidad;
        }

        public async Task<bool> ActualizarAsync(int id, ProyectoEditar peticion)
        {
            var entidadExistente = await _repositorio.ObtenerPorIdAsync(id);
            if (entidadExistente == null) return false;
            
            entidadExistente.nombre = peticion.titulo;

            await _repositorio.ActualizarAsync(entidadExistente);
            return true;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            if (await _repositorio.ObtenerPorIdAsync(id) == null) return false;
            await _repositorio.EliminarAsync(id);
            return true;
        }
    }
}
