using System;
using System.Threading.Tasks;
using mapa_conocimiento_api.Models;
using mapa_conocimiento_api.Peticiones;
using mapa_conocimiento_api.Repositorios;

namespace mapa_conocimiento_api.Servicios
{
    public class ProyectoLineaService : IProyectoLineaService
    {
        private readonly IProyectoLineaRepository _repositorio;
        private readonly IProyectoRepository _proyectoRepo;
        private readonly ILineaInvestigacionRepository _lineaRepo;

        public ProyectoLineaService(IProyectoLineaRepository repositorio, IProyectoRepository proyectoRepo, ILineaInvestigacionRepository lineaRepo)
        {
            _repositorio = repositorio; _proyectoRepo = proyectoRepo; _lineaRepo = lineaRepo;
        }

        public async Task<bool> CrearAsync(ProyectoLineaCrear peticion)
        {
            if (await _proyectoRepo.ObtenerPorIdAsync(peticion.proyecto_id) == null) throw new ArgumentException("El proyecto no existe.");
            if (await _lineaRepo.ObtenerPorIdAsync(peticion.linea_investigacion_id) == null) throw new ArgumentException("La línea de investigación no existe.");

            await _repositorio.CrearAsync(new ProyectoLinea { proyecto = peticion.proyecto_id, linea_investigacion = peticion.linea_investigacion_id });
            return true;
        }

        public async Task<bool> EliminarAsync(int proyecto_id, int linea_investigacion_id)
        {
            if (await _repositorio.ObtenerPorIdsAsync(proyecto_id, linea_investigacion_id) == null) return false;
            await _repositorio.EliminarAsync(proyecto_id, linea_investigacion_id); return true;
        }
    }
}
