using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using mapa_conocimiento_api.Models;

namespace mapa_conocimiento_api.Repositorios
{
    public class DocenteProductoRepository : IDocenteProductoRepository
    {
        private readonly postgresContext _context;
        public DocenteProductoRepository(postgresContext context) => _context = context;

        public async Task<DocenteProducto?> ObtenerPorIdsAsync(int docente_cedula, int producto_id) =>
            await _context.docente_producto.FirstOrDefaultAsync(d => d.docente == docente_cedula && d.producto == producto_id);

        public async Task CrearAsync(DocenteProducto entidad)
        {
            var existente = await _context.docente_producto
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(d => d.docente == entidad.docente && d.producto == entidad.producto);

            if (existente != null)
            {
                if (existente.activo)
                    throw new ArgumentException("El docente ya se encuentra vinculado a este producto.");

                existente.activo = true;
                await _context.SaveChangesAsync();
                return;
            }

            entidad.activo = true;
            await _context.docente_producto.AddAsync(entidad);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int docente_cedula, int producto_id)
        {
            var entidad = await ObtenerPorIdsAsync(docente_cedula, producto_id);
            if (entidad != null) { entidad.activo = false; await _context.SaveChangesAsync(); }
        }
    }
}
