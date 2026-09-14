using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using mapa_conocimiento_api.Models;

namespace mapa_conocimiento_api.Repositorios
{
    public class ProyectoRepository : IProyectoRepository
    {
        private readonly postgresContext _context;

        public ProyectoRepository(postgresContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<proyecto>> ObtenerTodosAsync()
        {
            return await _context.proyecto.ToListAsync();
        }

        public async Task<proyecto?> ObtenerPorIdAsync(int id)
        {
            return await _context.proyecto.FindAsync(id);
        }

        public async Task CrearAsync(proyecto entidad)
        {
            entidad.activo = true;
            await _context.proyecto.AddAsync(entidad);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(proyecto entidad)
        {
            _context.proyecto.Update(entidad);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var entidad = await _context.proyecto.FindAsync(id);
            if (entidad != null)
            {
                entidad.activo = false;
                await _context.SaveChangesAsync();
            }
        }
    }
}

