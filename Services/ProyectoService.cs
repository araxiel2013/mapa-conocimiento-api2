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
                titulo = peticion.titulo,
                resumen = peticion.resumen,
                presupuesto = peticion.presupuesto,
                tipo_financiacion = peticion.tipo_financiacion,
                tipo_fondos = peticion.tipo_fondos,
                fecha_inicio = peticion.fecha_inicio,
                fecha_fin = peticion.fecha_fin
            };
            await _repositorio.CrearAsync(nuevaEntidad);
            return nuevaEntidad;
        }

        public async Task<bool> ActualizarAsync(int id, ProyectoEditar peticion)
        {
            var entidadExistente = await _repositorio.ObtenerPorIdAsync(id);
            if (entidadExistente == null) return false;
            
            entidadExistente.titulo = peticion.titulo;
            entidadExistente.resumen = peticion.resumen;
            entidadExistente.presupuesto = peticion.presupuesto;
            entidadExistente.tipo_financiacion = peticion.tipo_financiacion;
            entidadExistente.tipo_fondos = peticion.tipo_fondos;
            entidadExistente.fecha_inicio = peticion.fecha_inicio;
            entidadExistente.fecha_fin = peticion.fecha_fin;

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
