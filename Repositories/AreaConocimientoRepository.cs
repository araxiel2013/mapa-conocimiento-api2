using Microsoft.EntityFrameworkCore;
using mapa_conocimiento_api.Models;

namespace mapa_conocimiento_api.Repositorios
{
    public class AreaConocimientoRepository : IAreaConocimientoRepository
    {
        private readonly postgresContext _context;

        public AreaConocimientoRepository(postgresContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AreaConocimiento>> ObtenerTodosAsync()
        {
            return await _context.area_conocimiento.ToListAsync();
        }

        public async Task<AreaConocimiento?> ObtenerPorIdAsync(int id)
        {
            return await _context.area_conocimiento.FindAsync(id);
        }

        public async Task CrearAsync(AreaConocimiento entidad)
        {
            var granArea = entidad.gran_area.Trim();
            var area = entidad.area.Trim();
            var disciplina = entidad.disciplina.Trim();

            var existente = await _context.area_conocimiento
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(a => (entidad.id > 0 && a.id == entidad.id) ||
                                          (a.gran_area.ToLower() == granArea.ToLower() &&
                                           a.area.ToLower() == area.ToLower() &&
                                           a.disciplina.ToLower() == disciplina.ToLower()));

            if (existente != null)
            {
                if (existente.activo)
                {
                    throw new ArgumentException($"Ya existe un área de conocimiento con gran área '{granArea}', área '{area}' y disciplina '{disciplina}'.");
                }

                existente.gran_area = granArea;
                existente.area = area;
                existente.disciplina = disciplina;
                existente.activo = true;
                await _context.SaveChangesAsync();
                entidad.id = existente.id;
                return;
            }

            entidad.gran_area = granArea;
            entidad.area = area;
            entidad.disciplina = disciplina;
            entidad.activo = true;
            await _context.area_conocimiento.AddAsync(entidad);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(AreaConocimiento entidad)
        {
            _context.area_conocimiento.Update(entidad);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var entidad = await _context.area_conocimiento.FindAsync(id);
            if (entidad != null)
            {
                entidad.activo = false;
                await _context.SaveChangesAsync();
            }
        }
    }
}
