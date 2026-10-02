using System;
using System.Threading.Tasks;
using mapa_conocimiento_api.Models;
using mapa_conocimiento_api.Peticiones;
using mapa_conocimiento_api.Repositorios;

namespace mapa_conocimiento_api.Servicios
{
    public class AaProyectoService : IAaProyectoService
    {
        private readonly IAaProyectoRepository _repositorio;
        private readonly IProyectoRepository _proyectoRepo;
        private readonly IAreaAplicacionRepository _areaRepo;

        public AaProyectoService(IAaProyectoRepository repositorio, IProyectoRepository proyectoRepo, IAreaAplicacionRepository areaRepo)
        {
            _repositorio = repositorio; _proyectoRepo = proyectoRepo; _areaRepo = areaRepo;
        }

        public async Task<bool> CrearAsync(AaProyectoCrear peticion)
        {
            if (await _proyectoRepo.ObtenerPorIdAsync(peticion.proyecto_id) == null) throw new ArgumentException("El proyecto no existe.");
            if (await _areaRepo.ObtenerPorIdAsync(peticion.area_aplicacion_id) == null) throw new ArgumentException("El área de aplicación no existe.");

            await _repositorio.CrearAsync(new aa_proyecto { proyecto = peticion.proyecto_id, area_aplicacion = peticion.area_aplicacion_id });
            return true;
        }

        public async Task<bool> EliminarAsync(int proyecto_id, int area_aplicacion_id)
        {
            if (await _repositorio.ObtenerPorIdsAsync(proyecto_id, area_aplicacion_id) == null) return false;
            await _repositorio.EliminarAsync(proyecto_id, area_aplicacion_id); return true;
        }
    }
}
