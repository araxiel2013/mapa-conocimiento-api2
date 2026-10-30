using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using mapa_conocimiento_api.Models;

namespace mapa_conocimiento_api.Repositorios
{
    public class OdsProyectoRepository : IOdsProyectoRepository
    {
        private readonly postgresContext _context;
        public OdsProyectoRepository(postgresContext context) => _context = context;

        public async Task<OdsProyecto?> ObtenerPorIdsAsync(int proyecto_id, int ods_id) =>
            await _context.ods_proyecto.FirstOrDefaultAsync(o => o.proyecto == proyecto_id && o.ods == ods_id);

        public async Task CrearAsync(OdsProyecto entidad)
        {
            var existente = await _context.ods_proyecto
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(o => o.proyecto == entidad.proyecto && o.ods == entidad.ods);

            if (existente != null)
            {
                if (existente.activo)
                    throw new ArgumentException("El ODS ya se encuentra asignado a este proyecto.");

                existente.activo = true;
                await _context.SaveChangesAsync();
                return;
            }

            entidad.activo = true;
            await _context.ods_proyecto.AddAsync(entidad);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int proyecto_id, int ods_id)
        {
            var entidad = await ObtenerPorIdsAsync(proyecto_id, ods_id);
            if (entidad != null) { entidad.activo = false; await _context.SaveChangesAsync(); }
        }
    }
}
