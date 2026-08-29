using Microsoft.EntityFrameworkCore;
using mapa_conocimiento_api.Models;

namespace mapa_conocimiento_api.Repositorios
{
    // Implementación del acceso a datos usando EF Core
    public class AreaConocimientoRepository : IAreaConocimientoRepository
    {
        private readonly postgresContext _context;

        // Se inyecta el contexto de datos configurado para Supabase
        public AreaConocimientoRepository(postgresContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<area_conocimiento>> ObtenerTodosAsync()
        {
            // Retorna todos los registros; el filtro global se encarga de excluir los inactivos
            return await _context.area_conocimiento.ToListAsync();
        }

        public async Task<area_conocimiento?> ObtenerPorIdAsync(int id)
        {
            return await _context.area_conocimiento.FindAsync(id);
        }

        public async Task CrearAsync(area_conocimiento entidad)
        {
            // Aseguramos que al crear un registro, este nazca activo
            entidad.activo = true;
            await _context.area_conocimiento.AddAsync(entidad);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(area_conocimiento entidad)
        {
            _context.area_conocimiento.Update(entidad);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            // Buscamos la entidad para aplicar el borrado lógico
            var entidad = await _context.area_conocimiento.FindAsync(id);
            if (entidad != null)
            {
                // Regla innegociable de la Fase 1: nunca borrar físicamente
                entidad.activo = false;
                await _context.SaveChangesAsync();
            }
        }
    }
}