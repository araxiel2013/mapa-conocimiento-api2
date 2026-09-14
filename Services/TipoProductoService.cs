using mapa_conocimiento_api.Models;
using mapa_conocimiento_api.Peticiones;
using mapa_conocimiento_api.Repositorios;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace mapa_conocimiento_api.Servicios
{
    public class TipoProductoService : ITipoProductoService
    {
        private readonly ITipoProductoRepository _repositorio;

        public TipoProductoService(ITipoProductoRepository repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<IEnumerable<tipo_producto>> ObtenerTodosAsync() => await _repositorio.ObtenerTodosAsync();

        public async Task<tipo_producto?> ObtenerPorIdAsync(int id) => await _repositorio.ObtenerPorIdAsync(id);

        public async Task<tipo_producto> CrearAsync(TipoProductoCrear peticion)
        {
            var nuevaEntidad = new tipo_producto { 
                categoria = peticion.categoria,
                clase = peticion.clase,
                nombre = peticion.nombre,
                tipologia = peticion.tipologia
            };
            await _repositorio.CrearAsync(nuevaEntidad);
            return nuevaEntidad;
        }

        public async Task<bool> ActualizarAsync(int id, TipoProductoEditar peticion)
        {
            var entidadExistente = await _repositorio.ObtenerPorIdAsync(id);
            if (entidadExistente == null) return false;
            
            entidadExistente.categoria = peticion.categoria;
            entidadExistente.clase = peticion.clase;
            entidadExistente.nombre = peticion.nombre;
            entidadExistente.tipologia = peticion.tipologia;

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

