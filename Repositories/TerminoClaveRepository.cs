using Microsoft.EntityFrameworkCore;
using mapa_conocimiento_api.Models;

namespace mapa_conocimiento_api.Repositorios
{
    // Implementación del acceso a datos para termino_clave usando EF Core
    public class TerminoClaveRepository : ITerminoClaveRepository
    {
        private readonly postgresContext _context;

        public TerminoClaveRepository(postgresContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<termino_clave>> ObtenerTodosAsync()
        {
            return await _context.termino_clave.ToListAsync();
        }

        public async Task<termino_clave?> ObtenerPorIdAsync(string termino)
        {
            // Usamos FindAsync pasándole la llave primaria tipo string
            return await _context.termino_clave.FindAsync(termino);
        }

        public async Task CrearAsync(termino_clave entidad)
        {
            entidad.activo = true; // El registro nace activo
            await _context.termino_clave.AddAsync(entidad);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(string termino)
        {
            var entidad = await _context.termino_clave.FindAsync(termino);
            if (entidad != null)
            {
                // Aplicamos el borrado lógico
                entidad.activo = false;
                await _context.SaveChangesAsync();
            }
        }
    }
}