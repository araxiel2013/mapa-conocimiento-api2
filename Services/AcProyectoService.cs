using System;
using System.Threading.Tasks;
using mapa_conocimiento_api.Models;
using mapa_conocimiento_api.Peticiones;
using mapa_conocimiento_api.Repositorios;

namespace mapa_conocimiento_api.Servicios
{
    public class AcProyectoService : IAcProyectoService
    {
        private readonly IAcProyectoRepository _repositorio;
        private readonly IProyectoRepository _proyectoRepo;
        private readonly IAreaConocimientoRepository _areaRepo;

        public AcProyectoService(IAcProyectoRepository repositorio, IProyectoRepository proyectoRepo, IAreaConocimientoRepository areaRepo)
        {
            _repositorio = repositorio; _proyectoRepo = proyectoRepo; _areaRepo = areaRepo;
        }

        public async Task<bool> CrearAsync(AcProyectoCrear peticion)
        {
            if (await _proyectoRepo.ObtenerPorIdAsync(peticion.proyecto_id) == null) throw new ArgumentException("El proyecto no existe.");
            if (await _areaRepo.ObtenerPorIdAsync(peticion.area_conocimiento_id) == null) throw new ArgumentException("El área de conocimiento no existe.");

            await _repositorio.CrearAsync(new ac_proyecto { proyecto = peticion.proyecto_id, area_conocimiento = peticion.area_conocimiento_id });
            return true;
        }

        public async Task<bool> EliminarAsync(int proyecto_id, int area_conocimiento_id)
        {
            if (await _repositorio.ObtenerPorIdsAsync(proyecto_id, area_conocimiento_id) == null) return false;
            await _repositorio.EliminarAsync(proyecto_id, area_conocimiento_id); return true;
        }
    }
}

