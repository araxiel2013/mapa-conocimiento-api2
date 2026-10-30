using Microsoft.EntityFrameworkCore;
using mapa_conocimiento_api.Models;

namespace mapa_conocimiento_api.Repositorios
{
    public class AreaAplicacionRepository : IAreaAplicacionRepository
    {
        private readonly postgresContext _context;

        public AreaAplicacionRepository(postgresContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AreaAplicacion>> ObtenerTodosAsync()
        {
            return await _context.area_aplicacion.ToListAsync();
        }

        public async Task<AreaAplicacion?> ObtenerPorIdAsync(int id)
        {
            return await _context.area_aplicacion.FindAsync(id);
        }

        public async Task CrearAsync(AreaAplicacion entidad)
        {
            var nombreNormalizado = entidad.nombre.Trim();
            var existente = await _context.area_aplicacion
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(a => (entidad.id > 0 && a.id == entidad.id) ||
                                          a.nombre.ToLower() == nombreNormalizado.ToLower());

            if (existente != null)
            {
                if (existente.activo)
                {
                    throw new ArgumentException($"Ya existe un área de aplicación con el nombre '{entidad.nombre}'.");
                }

                existente.nombre = nombreNormalizado;
                existente.activo = true;
                await _context.SaveChangesAsync();
                entidad.id = existente.id;
                return;
            }

            entidad.nombre = nombreNormalizado;
            entidad.activo = true;
            await _context.area_aplicacion.AddAsync(entidad);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(AreaAplicacion entidad)
        {
            _context.area_aplicacion.Update(entidad);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var entidad = await _context.area_aplicacion.FindAsync(id);
            if (entidad != null)
            {
                entidad.activo = false;
                await _context.SaveChangesAsync();
            }
        }
    }
}
