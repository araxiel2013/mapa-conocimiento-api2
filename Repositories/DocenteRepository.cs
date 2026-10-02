using Microsoft.EntityFrameworkCore;
using mapa_conocimiento_api.Models;

namespace mapa_conocimiento_api.Repositorios
{
    public class DocenteRepository : IDocenteRepository
    {
        private readonly postgresContext _context;

        public DocenteRepository(postgresContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<docente>> ObtenerTodosAsync()
        {
            // CRÍTICO FASE 2: Eager Loading usando .Include() para traer los datos relacionales
            return await _context.docente
                .Include(d => d.LineaInvestigacionRelacionada)
                .ToListAsync();
        }

        public async Task<docente?> ObtenerPorIdAsync(int cedula)
        {
            return await _context.docente
                .Include(d => d.LineaInvestigacionRelacionada)
                .FirstOrDefaultAsync(d => d.cedula == cedula);
        }

        public async Task CrearAsync(docente entidad)
        {
            entidad.activo = true;
            await _context.docente.AddAsync(entidad);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(docente entidad)
        {
            _context.docente.Update(entidad);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int cedula)
        {
            var entidad = await _context.docente.FindAsync(cedula);
            if (entidad != null)
            {
                entidad.activo = false;
                await _context.SaveChangesAsync();
            }
        }
    }
}