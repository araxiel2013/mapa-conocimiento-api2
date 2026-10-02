using Microsoft.EntityFrameworkCore;
using mapa_conocimiento_api.Models;

namespace mapa_conocimiento_api.Repositorios
{
    public class DesarrollaRepository : IDesarrollaRepository
    {
        private readonly postgresContext _context;

        public DesarrollaRepository(postgresContext context)
        {
            _context = context;
        }

        public async Task<desarrolla?> ObtenerPorIdsAsync(int docente_cedula, int proyecto_id)
        {
            return await _context.desarrolla
                .FirstOrDefaultAsync(d => d.docente == docente_cedula && d.proyecto == proyecto_id);
        }

        public async Task CrearAsync(desarrolla entidad)
        {
            entidad.activo = true;
            await _context.desarrolla.AddAsync(entidad);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int docente_cedula, int proyecto_id)
        {
            var entidad = await ObtenerPorIdsAsync(docente_cedula, proyecto_id);
            if (entidad != null)
            {
                entidad.activo = false;
                await _context.SaveChangesAsync();
            }
        }
    }
}