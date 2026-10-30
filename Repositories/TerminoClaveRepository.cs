using Microsoft.EntityFrameworkCore;
using mapa_conocimiento_api.Models;

namespace mapa_conocimiento_api.Repositorios
{
    public class TerminoClaveRepository : ITerminoClaveRepository
    {
        private readonly postgresContext _context;

        public TerminoClaveRepository(postgresContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TerminoClave>> ObtenerTodosAsync()
        {
            return await _context.termino_clave.ToListAsync();
        }

        public async Task<TerminoClave?> ObtenerPorIdAsync(string termino)
        {
            var terminoNormalizado = termino.Trim();
            return await _context.termino_clave
                .FirstOrDefaultAsync(t => t.termino.ToLower() == terminoNormalizado.ToLower());
        }

        public async Task CrearAsync(TerminoClave entidad)
        {
            var terminoNormalizado = entidad.termino.Trim();
            var existente = await _context.termino_clave
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(t => t.termino.ToLower() == terminoNormalizado.ToLower());

            if (existente != null)
            {
                if (existente.activo)
                {
                    throw new ArgumentException($"Ya existe el término clave '{entidad.termino}'.");
                }

                existente.activo = true;
                await _context.SaveChangesAsync();
                return;
            }

            entidad.termino = terminoNormalizado;
            entidad.activo = true;
            await _context.termino_clave.AddAsync(entidad);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(string termino)
        {
            var terminoNormalizado = termino.Trim();
            var entidad = await _context.termino_clave
                .FirstOrDefaultAsync(t => t.termino.ToLower() == terminoNormalizado.ToLower());
            if (entidad != null)
            {
                entidad.activo = false;
                await _context.SaveChangesAsync();
            }
        }
    }
}
