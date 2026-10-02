using Microsoft.EntityFrameworkCore;
using mapa_conocimiento_api.Models;

namespace mapa_conocimiento_api.Repositorios
{
    public class AliadoProyectoRepository : IAliadoProyectoRepository
    {
        private readonly postgresContext _context;

        public AliadoProyectoRepository(postgresContext context)
        {
            _context = context;
        }

        public async Task<aliado_proyecto?> ObtenerPorIdsAsync(int aliado_nit, int proyecto_id)
        {
            return await _context.aliado_proyecto
                .FirstOrDefaultAsync(ap => ap.aliado == aliado_nit && ap.proyecto == proyecto_id);
        }

        public async Task CrearAsync(aliado_proyecto entidad)
        {
            entidad.activo = true;
            await _context.aliado_proyecto.AddAsync(entidad);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int aliado_nit, int proyecto_id)
        {
            var entidad = await ObtenerPorIdsAsync(aliado_nit, proyecto_id);
            if (entidad != null)
            {
                entidad.activo = false;
                await _context.SaveChangesAsync();
            }
        }
    }
}