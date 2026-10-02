using Microsoft.EntityFrameworkCore;
using mapa_conocimiento_api.Models;
using System.Threading.Tasks;

namespace mapa_conocimiento_api.Repositorios
{
    public class AaProyectoRepository : IAaProyectoRepository
    {
        private readonly postgresContext _context;
        public AaProyectoRepository(postgresContext context) => _context = context;

        public async Task<aa_proyecto?> ObtenerPorIdsAsync(int proyecto_id, int area_aplicacion_id) =>
            await _context.aa_proyecto.FirstOrDefaultAsync(a => a.proyecto == proyecto_id && a.area_aplicacion == area_aplicacion_id);

        public async Task CrearAsync(aa_proyecto entidad)
        {
            entidad.activo = true;
            await _context.aa_proyecto.AddAsync(entidad);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int proyecto_id, int area_aplicacion_id)
        {
            var entidad = await ObtenerPorIdsAsync(proyecto_id, area_aplicacion_id);
            if (entidad != null) { entidad.activo = false; await _context.SaveChangesAsync(); }
        }
    }
}
