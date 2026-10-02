using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using mapa_conocimiento_api.Models;

namespace mapa_conocimiento_api.Repositorios
{
    public class AcProyectoRepository : IAcProyectoRepository
    {
        private readonly postgresContext _context;
        public AcProyectoRepository(postgresContext context) => _context = context;

        public async Task<ac_proyecto?> ObtenerPorIdsAsync(int proyecto_id, int area_conocimiento_id) =>
            await _context.ac_proyecto.FirstOrDefaultAsync(a => a.proyecto == proyecto_id && a.area_conocimiento == area_conocimiento_id);

        public async Task CrearAsync(ac_proyecto entidad)
        {
            entidad.activo = true;
            await _context.ac_proyecto.AddAsync(entidad);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int proyecto_id, int area_conocimiento_id)
        {
            var entidad = await ObtenerPorIdsAsync(proyecto_id, area_conocimiento_id);
            if (entidad != null) { entidad.activo = false; await _context.SaveChangesAsync(); }
        }
    }
}
