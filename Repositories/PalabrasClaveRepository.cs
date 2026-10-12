using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using mapa_conocimiento_api.Models;

namespace mapa_conocimiento_api.Repositorios
{
    public class PalabrasClaveRepository : IPalabrasClaveRepository
    {
        private readonly postgresContext _context;
        public PalabrasClaveRepository(postgresContext context) => _context = context;

        public async Task<palabras_clave?> ObtenerPorIdsAsync(int proyecto_id, string termino_clave) =>
            await _context.palabras_clave.FirstOrDefaultAsync(p => p.proyecto == proyecto_id && p.termino_clave == termino_clave);

        public async Task CrearAsync(palabras_clave entidad)
        {
            var existente = await _context.palabras_clave
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(p => p.proyecto == entidad.proyecto && p.termino_clave == entidad.termino_clave);

            if (existente != null)
            {
                if (existente.activo)
                    throw new ArgumentException("El término clave ya se encuentra asignado a este proyecto.");

                existente.activo = true;
                await _context.SaveChangesAsync();
                return;
            }

            entidad.activo = true;
            await _context.palabras_clave.AddAsync(entidad);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int proyecto_id, string termino_clave)
        {
            var entidad = await ObtenerPorIdsAsync(proyecto_id, termino_clave);
            if (entidad != null) { entidad.activo = false; await _context.SaveChangesAsync(); }
        }
    }
}
