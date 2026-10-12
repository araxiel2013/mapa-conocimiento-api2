using System;
using System.Threading.Tasks;
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
            var existente = await _context.desarrolla
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(d => d.docente == entidad.docente && d.proyecto == entidad.proyecto);

            if (existente != null)
            {
                if (existente.activo)
                    throw new ArgumentException("El docente ya se encuentra asignado a este proyecto.");

                existente.rol = entidad.rol;
                existente.descripcion = entidad.descripcion;
                existente.activo = true;
                await _context.SaveChangesAsync();
                return;
            }

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