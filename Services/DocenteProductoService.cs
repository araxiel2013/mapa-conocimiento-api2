using System;
using System.Threading.Tasks;
using mapa_conocimiento_api.Models;
using mapa_conocimiento_api.Peticiones;
using mapa_conocimiento_api.Repositorios;

namespace mapa_conocimiento_api.Servicios
{
    public class DocenteProductoService : IDocenteProductoService
    {
        private readonly IDocenteProductoRepository _repositorio;
        private readonly IDocenteRepository _docenteRepo;
        private readonly IProductoRepository _productoRepo;

        public DocenteProductoService(IDocenteProductoRepository repositorio, IDocenteRepository docenteRepo, IProductoRepository productoRepo)
        {
            _repositorio = repositorio; _docenteRepo = docenteRepo; _productoRepo = productoRepo;
        }

        public async Task<bool> CrearAsync(DocenteProductoCrear peticion)
        {
            if (await _docenteRepo.ObtenerPorIdAsync(peticion.docente_cedula) == null) throw new ArgumentException("El docente no existe.");
            if (await _productoRepo.ObtenerPorIdAsync(peticion.producto_id) == null) throw new ArgumentException("El producto no existe.");

            await _repositorio.CrearAsync(new DocenteProducto { docente = peticion.docente_cedula, producto = peticion.producto_id });
            return true;
        }

        public async Task<bool> EliminarAsync(int docente_cedula, int producto_id)
        {
            if (await _repositorio.ObtenerPorIdsAsync(docente_cedula, producto_id) == null) return false;
            await _repositorio.EliminarAsync(docente_cedula, producto_id); return true;
        }
    }
}
