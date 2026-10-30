using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using mapa_conocimiento_api.Models;

namespace mapa_conocimiento_api.Repositorios
{
    public class ProyectoLineaRepository : IProyectoLineaRepository
    {
        private readonly postgresContext _context;
        public ProyectoLineaRepository(postgresContext context) => _context = context;

        public async Task<ProyectoLinea?> ObtenerPorIdsAsync(int proyecto_id, int linea_investigacion_id) =>
            await _context.proyecto_linea.FirstOrDefaultAsync(p => p.proyecto == proyecto_id && p.linea_investigacion == linea_investigacion_id);

        public async Task CrearAsync(ProyectoLinea entidad)
        {
            var existente = await _context.proyecto_linea
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(p => p.proyecto == entidad.proyecto && p.linea_investigacion == entidad.linea_investigacion);

            if (existente != null)
            {
                if (existente.activo)
                    throw new ArgumentException("La línea de investigación ya se encuentra asignada a este proyecto.");

                existente.activo = true;
                await _context.SaveChangesAsync();
                return;
            }

            entidad.activo = true;
            await _context.proyecto_linea.AddAsync(entidad);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int proyecto_id, int linea_investigacion_id)
        {
            var entidad = await ObtenerPorIdsAsync(proyecto_id, linea_investigacion_id);
            if (entidad != null) { entidad.activo = false; await _context.SaveChangesAsync(); }
        }
    }
}
