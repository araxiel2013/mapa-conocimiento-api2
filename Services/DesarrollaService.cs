using System;
using System.Threading.Tasks;
using mapa_conocimiento_api.Models;
using mapa_conocimiento_api.Peticiones;
using mapa_conocimiento_api.Repositorios;

namespace mapa_conocimiento_api.Servicios
{
    public class DesarrollaService : IDesarrollaService
    {
        private readonly IDesarrollaRepository _repositorio;
        private readonly IDocenteRepository _docenteRepository;
        private readonly IProyectoRepository _proyectoRepository;

        public DesarrollaService(
            IDesarrollaRepository repositorio,
            IDocenteRepository docenteRepository,
            IProyectoRepository proyectoRepository)
        {
            _repositorio = repositorio;
            _docenteRepository = docenteRepository;
            _proyectoRepository = proyectoRepository;
        }

        public async Task<bool> CrearAsync(DesarrollaCrear peticion)
        {
            var docente = await _docenteRepository.ObtenerPorIdAsync(peticion.docente_cedula);
            if (docente == null) throw new ArgumentException("El docente no existe o está inactivo.");

            var proyecto = await _proyectoRepository.ObtenerPorIdAsync(peticion.proyecto_id);
            if (proyecto == null) throw new ArgumentException("El proyecto no existe o está inactivo.");

            var nuevaRelacion = new Desarrolla
            {
                docente = peticion.docente_cedula,
                proyecto = peticion.proyecto_id,
                rol = peticion.rol,
                descripcion = peticion.descripcion
            };

            await _repositorio.CrearAsync(nuevaRelacion);
            return true;
        }

        public async Task<bool> EliminarAsync(int docente_cedula, int proyecto_id)
        {
            var entidad = await _repositorio.ObtenerPorIdsAsync(docente_cedula, proyecto_id);
            if (entidad == null) return false;

            await _repositorio.EliminarAsync(docente_cedula, proyecto_id);
            return true;
        }
    }
}
