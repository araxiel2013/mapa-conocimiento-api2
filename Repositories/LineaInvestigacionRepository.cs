using Microsoft.EntityFrameworkCore;
using mapa_conocimiento_api.Models;

namespace mapa_conocimiento_api.Repositorios
{
    // Implementación del acceso a datos para linea_investigacion usando EF Core
    public class LineaInvestigacionRepository : ILineaInvestigacionRepository
    {
        private readonly postgresContext _context;

        public LineaInvestigacionRepository(postgresContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<linea_investigacion>> ObtenerTodosAsync()
        {
            return await _context.linea_investigacion.ToListAsync();
        }

        public async Task<linea_investigacion?> ObtenerPorIdAsync(int id)
        {
            return await _context.linea_investigacion.FindAsync(id);
        }

        public async Task CrearAsync(linea_investigacion entidad)
        {
            var nombreNormalizado = entidad.nombre.Trim();
            var existente = await _context.linea_investigacion
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(l => (entidad.id > 0 && l.id == entidad.id) ||
                                          l.nombre.ToLower() == nombreNormalizado.ToLower());

            if (existente != null)
            {
                if (existente.activo)
                {
                    throw new ArgumentException($"Ya existe una línea de investigación con el nombre '{entidad.nombre}'.");
                }

                // Reactivación si estaba previamente inactivado (borrado lógico)
                existente.nombre = nombreNormalizado;
                existente.descripcion = entidad.descripcion;
                existente.activo = true;
                await _context.SaveChangesAsync();
                entidad.id = existente.id;
                return;
            }

            entidad.nombre = nombreNormalizado;
            entidad.activo = true; // Todo nuevo registro nace activo por defecto
            await _context.linea_investigacion.AddAsync(entidad);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(linea_investigacion entidad)
        {
            _context.linea_investigacion.Update(entidad);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var entidad = await _context.linea_investigacion.FindAsync(id);
            if (entidad != null)
            {
                // Aplicamos el borrado lógico
                entidad.activo = false;
                await _context.SaveChangesAsync();
            }
        }
    }
}