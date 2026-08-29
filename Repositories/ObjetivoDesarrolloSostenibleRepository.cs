using Microsoft.EntityFrameworkCore;
using mapa_conocimiento_api.Models;

namespace mapa_conocimiento_api.Repositorios
{
    // Implementación del acceso a datos para ODS usando EF Core
    public class ObjetivoDesarrolloSostenibleRepository : IObjetivoDesarrolloSostenibleRepository
    {
        private readonly postgresContext _context;

        public ObjetivoDesarrolloSostenibleRepository(postgresContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<objetivo_desarrollo_sostenible>> ObtenerTodosAsync()
        {
            // El DbContext filtrará los inactivos automáticamente gracias al HasQueryFilter
            return await _context.objetivo_desarrollo_sostenible.ToListAsync();
        }

        public async Task<objetivo_desarrollo_sostenible?> ObtenerPorIdAsync(int id)
        {
            return await _context.objetivo_desarrollo_sostenible.FindAsync(id);
        }

        public async Task CrearAsync(objetivo_desarrollo_sostenible entidad)
        {
            entidad.activo = true; // Todo nuevo registro nace activo por defecto
            await _context.objetivo_desarrollo_sostenible.AddAsync(entidad);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(objetivo_desarrollo_sostenible entidad)
        {
            _context.objetivo_desarrollo_sostenible.Update(entidad);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var entidad = await _context.objetivo_desarrollo_sostenible.FindAsync(id);
            if (entidad != null)
            {
                // Aplicamos el borrado lógico
                entidad.activo = false;
                await _context.SaveChangesAsync();
            }
        }
    }
}