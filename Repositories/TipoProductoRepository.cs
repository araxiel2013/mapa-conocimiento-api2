using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using mapa_conocimiento_api.Models;

namespace mapa_conocimiento_api.Repositorios
{
    public class TipoProductoRepository : ITipoProductoRepository
    {
        private readonly postgresContext _context;

        public TipoProductoRepository(postgresContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TipoProducto>> ObtenerTodosAsync()
        {
            return await _context.tipo_producto.ToListAsync();
        }

        public async Task<TipoProducto?> ObtenerPorIdAsync(int id)
        {
            return await _context.tipo_producto.FindAsync(id);
        }

        public async Task CrearAsync(TipoProducto entidad)
        {
            var nombreNormalizado = entidad.nombre.Trim();
            var existente = await _context.tipo_producto
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(t => (entidad.id > 0 && t.id == entidad.id) ||
                                          t.nombre.ToLower() == nombreNormalizado.ToLower());

            if (existente != null)
            {
                if (existente.activo)
                {
                    throw new ArgumentException($"Ya existe un tipo de producto con el nombre '{entidad.nombre}'.");
                }

                existente.categoria = entidad.categoria;
                existente.clase = entidad.clase;
                existente.nombre = nombreNormalizado;
                existente.tipologia = entidad.tipologia;
                existente.activo = true;
                await _context.SaveChangesAsync();
                entidad.id = existente.id;
                return;
            }

            entidad.nombre = nombreNormalizado;
            entidad.activo = true;
            await _context.tipo_producto.AddAsync(entidad);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(TipoProducto entidad)
        {
            _context.tipo_producto.Update(entidad);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var entidad = await _context.tipo_producto.FindAsync(id);
            if (entidad != null)
            {
                entidad.activo = false;
                await _context.SaveChangesAsync();
            }
        }
    }
}
