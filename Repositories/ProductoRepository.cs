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

        public async Task<IEnumerable<Producto>> ObtenerTodosAsync()
        {
            return await _context.producto
                .Include(p => p.ProyectoRelacionado)
                .Include(p => p.TipoProductoRelacionado)
                .ToListAsync();
        }

        public async Task<Producto?> ObtenerPorIdAsync(int id)
        {
            return await _context.producto
                .Include(p => p.ProyectoRelacionado)
                .Include(p => p.TipoProductoRelacionado)
                .FirstOrDefaultAsync(p => p.id == id);
        }

        public async Task CrearAsync(Producto entidad)
        {
            var nombreNormalizado = entidad.nombre.Trim();
            var existente = await _context.producto
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(p => (entidad.id > 0 && p.id == entidad.id) ||
                                          p.nombre.ToLower() == nombreNormalizado.ToLower());

            if (existente != null)
            {
                if (existente.activo)
                {
                    throw new ArgumentException($"Ya existe un producto con el nombre '{entidad.nombre}'.");
                }

                existente.nombre = nombreNormalizado;
                existente.categoria = entidad.categoria;
                existente.fecha_entrega = entidad.fecha_entrega;
                existente.proyecto_id = entidad.proyecto_id;
                existente.tipo_producto_id = entidad.tipo_producto_id;
                existente.activo = true;
                await _context.SaveChangesAsync();
                entidad.id = existente.id;
                return;
            }

            entidad.nombre = nombreNormalizado;
            entidad.activo = true;
            await _context.producto.AddAsync(entidad);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(Producto entidad)
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

                var docenteProductoRelaciones = await _context.docente_producto.Where(dp => dp.producto == id).ToListAsync();
                foreach (var dp in docenteProductoRelaciones) dp.activo = false;

                await _context.SaveChangesAsync();
            }
        }
    }
}
