using System;
using System.Threading.Tasks;
using mapa_conocimiento_api.Models;
using mapa_conocimiento_api.Peticiones;
using mapa_conocimiento_api.Repositorios;

namespace mapa_conocimiento_api.Servicios
{
    public class AliadoProyectoService : IAliadoProyectoService
    {
        private readonly IAliadoProyectoRepository _repositorio;
        private readonly IAliadoRepository _aliadoRepository;
        private readonly IProyectoRepository _proyectoRepository;

        public AliadoProyectoService(
            IAliadoProyectoRepository repositorio,
            IAliadoRepository aliadoRepository,
            IProyectoRepository proyectoRepository)
        {
            _repositorio = repositorio;
            _aliadoRepository = aliadoRepository;
            _proyectoRepository = proyectoRepository;
        }

        public async Task<bool> CrearAsync(AliadoProyectoCrear peticion)
        {
            var aliado = await _aliadoRepository.ObtenerPorIdAsync(peticion.aliado_nit);
            if (aliado == null) throw new ArgumentException("El aliado no existe o está inactivo.");

            var proyecto = await _proyectoRepository.ObtenerPorIdAsync(peticion.proyecto_id);
            if (proyecto == null) throw new ArgumentException("El proyecto no existe o está inactivo.");

            var nuevaRelacion = new AliadoProyecto
            {
                aliado = peticion.aliado_nit,
                proyecto = peticion.proyecto_id
            };

            await _repositorio.CrearAsync(nuevaRelacion);
            return true;
        }

        public async Task<bool> EliminarAsync(int aliado_nit, int proyecto_id)
        {
            var entidad = await _repositorio.ObtenerPorIdsAsync(aliado_nit, proyecto_id);
            if (entidad == null) return false;

            await _repositorio.EliminarAsync(aliado_nit, proyecto_id);
            return true;
        }
    }
}
