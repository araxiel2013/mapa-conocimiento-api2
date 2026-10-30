using System;
using System.Threading.Tasks;
using mapa_conocimiento_api.Models;
using mapa_conocimiento_api.Peticiones;
using mapa_conocimiento_api.Repositorios;

namespace mapa_conocimiento_api.Servicios
{
    public class OdsProyectoService : IOdsProyectoService
    {
        private readonly IOdsProyectoRepository _repositorio;
        private readonly IProyectoRepository _proyectoRepo;
        private readonly IObjetivoDesarrolloSostenibleRepository _odsRepo;

        public OdsProyectoService(IOdsProyectoRepository repositorio, IProyectoRepository proyectoRepo, IObjetivoDesarrolloSostenibleRepository odsRepo)
        {
            _repositorio = repositorio; _proyectoRepo = proyectoRepo; _odsRepo = odsRepo;
        }

        public async Task<bool> CrearAsync(OdsProyectoCrear peticion)
        {
            if (await _proyectoRepo.ObtenerPorIdAsync(peticion.proyecto_id) == null) throw new ArgumentException("El proyecto no existe.");
            if (await _odsRepo.ObtenerPorIdAsync(peticion.ods_id) == null) throw new ArgumentException("El ODS no existe.");

            await _repositorio.CrearAsync(new OdsProyecto { proyecto = peticion.proyecto_id, ods = peticion.ods_id });
            return true;
        }

        public async Task<bool> EliminarAsync(int proyecto_id, int ods_id)
        {
            if (await _repositorio.ObtenerPorIdsAsync(proyecto_id, ods_id) == null) return false;
            await _repositorio.EliminarAsync(proyecto_id, ods_id); return true;
        }
    }
}
