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

        public async Task<IEnumerable<tipo_producto>> ObtenerTodosAsync()
        {
            return await _context.tipo_producto.ToListAsync();
        }

        public async Task<tipo_producto?> ObtenerPorIdAsync(int id)
        {
            return await _context.tipo_producto.FindAsync(id);
        }

        public async Task CrearAsync(tipo_producto entidad)
        {
            entidad.activo = true;
            await _context.tipo_producto.AddAsync(entidad);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(tipo_producto entidad)
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

