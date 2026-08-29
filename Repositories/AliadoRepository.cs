using Microsoft.EntityFrameworkCore;
using mapa_conocimiento_api.Models;

namespace mapa_conocimiento_api.Repositorios
{
    // Implementación del acceso a datos para aliado usando EF Core
    public class AliadoRepository : IAliadoRepository
    {
        private readonly postgresContext _context;

        public AliadoRepository(postgresContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<aliado>> ObtenerTodosAsync()
        {
            return await _context.aliado.ToListAsync();
        }

        public async Task<aliado?> ObtenerPorIdAsync(int nit)
        {
            // Buscamos utilizando la llave primaria que en este caso es el NIT
            return await _context.aliado.FindAsync(nit);
        }

        public async Task CrearAsync(aliado entidad)
        {
            entidad.activo = true; // Todo nuevo registro nace activo por defecto
            await _context.aliado.AddAsync(entidad);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(aliado entidad)
        {
            _context.aliado.Update(entidad);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int nit)
        {
            var entidad = await _context.aliado.FindAsync(nit);
            if (entidad != null)
            {
                // Aplicamos el borrado lógico
                entidad.activo = false;
                await _context.SaveChangesAsync();
            }
        }
    }
}