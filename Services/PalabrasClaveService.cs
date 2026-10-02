using System;
using System.Threading.Tasks;
using mapa_conocimiento_api.Models;
using mapa_conocimiento_api.Peticiones;
using mapa_conocimiento_api.Repositorios;

namespace mapa_conocimiento_api.Servicios
{
    public class PalabrasClaveService : IPalabrasClaveService
    {
        private readonly IPalabrasClaveRepository _repositorio;
        private readonly IProyectoRepository _proyectoRepo;
        private readonly ITerminoClaveRepository _terminoRepo;

        public PalabrasClaveService(IPalabrasClaveRepository repositorio, IProyectoRepository proyectoRepo, ITerminoClaveRepository terminoRepo)
        {
            _repositorio = repositorio; _proyectoRepo = proyectoRepo; _terminoRepo = terminoRepo;
        }

        public async Task<bool> CrearAsync(PalabrasClaveCrear peticion)
        {
            if (await _proyectoRepo.ObtenerPorIdAsync(peticion.proyecto_id) == null) throw new ArgumentException("El proyecto no existe.");
            if (await _terminoRepo.ObtenerPorIdAsync(peticion.termino_clave) == null) throw new ArgumentException("El término clave no existe.");

            await _repositorio.CrearAsync(new palabras_clave { proyecto = peticion.proyecto_id, termino_clave = peticion.termino_clave });
            return true;
        }

        public async Task<bool> EliminarAsync(int proyecto_id, string termino_clave)
        {
            if (await _repositorio.ObtenerPorIdsAsync(proyecto_id, termino_clave) == null) return false;
            await _repositorio.EliminarAsync(proyecto_id, termino_clave); return true;
        }
    }
}

