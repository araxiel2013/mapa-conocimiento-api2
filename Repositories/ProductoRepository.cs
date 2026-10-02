using Microsoft.EntityFrameworkCore;
using mapa_conocimiento_api.Models;

namespace mapa_conocimiento_api.Repositorios
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly postgresContext _context;

        public ProductoRepository(postgresContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<producto>> ObtenerTodosAsync()
        {
            // CRÍTICO FASE 2: Eager Loading para las dos llaves foráneas
            return await _context.producto
                .Include(p => p.ProyectoRelacionado)
                .Include(p => p.TipoProductoRelacionado)
                .ToListAsync();
        }

        public async Task<producto?> ObtenerPorIdAsync(int id)
        {
            return await _context.producto
                .Include(p => p.ProyectoRelacionado)
                .Include(p => p.TipoProductoRelacionado)
                .FirstOrDefaultAsync(p => p.id == id);
        }

        public async Task CrearAsync(producto entidad)
        {
            entidad.activo = true;
            await _context.producto.AddAsync(entidad);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(producto entidad)
        {
            _context.producto.Update(entidad);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var entidad = await _context.producto.FindAsync(id);
            if (entidad != null)
            {
                entidad.activo = false;
                await _context.SaveChangesAsync();
            }
        }
    }
}