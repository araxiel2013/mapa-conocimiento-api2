using Microsoft.EntityFrameworkCore;
using mapa_conocimiento_api.Models;

namespace mapa_conocimiento_api.Repositorios
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly postgresContext _context;

        public UsuarioRepository(postgresContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<usuario>> ObtenerTodosAsync()
        {
            // Eager Loading para traernos los roles de cada usuario
            return await _context.usuario
                .Include(u => u.RolesUsuario)
                    .ThenInclude(ru => ru.RolRelacionado)
                .ToListAsync();
        }

        public async Task<usuario?> ObtenerPorIdAsync(int id)
        {
            return await _context.usuario
                .Include(u => u.RolesUsuario)
                    .ThenInclude(ru => ru.RolRelacionado)
                .FirstOrDefaultAsync(u => u.id == id);
        }

        public async Task<bool> ExisteUsernameOEmailAsync(string username, string email)
        {
            return await _context.usuario
                .AnyAsync(u => u.username == username || u.email == email);
        }

        public async Task<bool> ExisteUsernameOEmailIgnorandoFiltrosAsync(string username, string email)
        {
            return await _context.usuario
                .IgnoreQueryFilters()
                .AnyAsync(u => u.username == username || u.email == email);
        }

        public async Task CrearAsync(usuario entidad, List<int> roles_ids)
        {
            // Iniciamos una transacción porque insertaremos en dos tablas (usuario y rol_usuario)
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                entidad.activo = true;
                entidad.fecha_creacion = DateTime.UtcNow;
                entidad.fecha_actualizacion = DateTime.UtcNow;

                await _context.usuario.AddAsync(entidad);
                await _context.SaveChangesAsync(); // Se guarda para obtener el ID generado

                // Insertamos en la tabla puente los roles asignados
                foreach (var rolId in roles_ids)
                {
                    var rolUsuario = new rol_usuario
                    {
                        usuario_id = entidad.id,
                        rol_id = rolId,
                        activo = true
                    };
                    await _context.rol_usuario.AddAsync(rolUsuario);
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task EliminarAsync(int id)
        {
            var entidad = await _context.usuario.FindAsync(id);
            if (entidad != null)
            {
                entidad.activo = false;
                entidad.fecha_actualizacion = DateTime.UtcNow;
                await _context.SaveChangesAsync();
            }
        }
    }
}