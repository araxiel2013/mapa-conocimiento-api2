using Microsoft.EntityFrameworkCore;
using mapa_conocimiento_api.Models;

namespace mapa_conocimiento_api.Repositorios
{
    public class ObjetivoDesarrolloSostenibleRepository : IObjetivoDesarrolloSostenibleRepository
    {
        private readonly postgresContext _context;

        public ObjetivoDesarrolloSostenibleRepository(postgresContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ObjetivoDesarrolloSostenible>> ObtenerTodosAsync()
        {
            return await _context.objetivo_desarrollo_sostenible.ToListAsync();
        }

        public async Task<ObjetivoDesarrolloSostenible?> ObtenerPorIdAsync(int id)
        {
            return await _context.objetivo_desarrollo_sostenible.FindAsync(id);
        }

        public async Task CrearAsync(ObjetivoDesarrolloSostenible entidad)
        {
            var nombreNormalizado = entidad.nombre.Trim();
            var existente = await _context.objetivo_desarrollo_sostenible
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(o => (entidad.id > 0 && o.id == entidad.id) ||
                                          o.nombre.ToLower() == nombreNormalizado.ToLower());

            if (existente != null)
            {
                if (existente.activo)
                {
                    throw new ArgumentException($"Ya existe un objetivo de desarrollo sostenible con el nombre '{entidad.nombre}'.");
                }

                existente.nombre = nombreNormalizado;
                existente.categoria = entidad.categoria;
                existente.activo = true;
                await _context.SaveChangesAsync();
                entidad.id = existente.id;
                return;
            }

            entidad.nombre = nombreNormalizado;
            entidad.activo = true;
            await _context.objetivo_desarrollo_sostenible.AddAsync(entidad);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(ObjetivoDesarrolloSostenible entidad)
        {
            _context.objetivo_desarrollo_sostenible.Update(entidad);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var entidad = await _context.objetivo_desarrollo_sostenible.FindAsync(id);
            if (entidad != null)
            {
                entidad.activo = false;
                await _context.SaveChangesAsync();
            }
        }
    }
}
