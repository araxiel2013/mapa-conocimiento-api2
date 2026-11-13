using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<IEnumerable<Usuario>> ObtenerTodosAsync()
        {
            return await _context.usuario
                .Include(u => u.RolesUsuario)
                    .ThenInclude(ru => ru.RolRelacionado)
                .ToListAsync();
        }

        public async Task<Usuario?> ObtenerPorIdAsync(int id)
        {
            return await _context.usuario
                .Include(u => u.RolesUsuario)
                    .ThenInclude(ru => ru.RolRelacionado)
                .FirstOrDefaultAsync(u => u.id == id);
        }

        public async Task<Usuario?> ObtenerPorUsernameConRolesAsync(string username)
        {
            return await _context.usuario
                .Include(u => u.RolesUsuario.Where(ru => ru.activo))
                    .ThenInclude(ru => ru.RolRelacionado)
                .FirstOrDefaultAsync(u => u.username == username);
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

        public async Task<bool> ExistenRolesActivosAsync(List<int> rolesIds)
        {
            if (rolesIds == null || rolesIds.Count == 0)
                return false;

            var idsUnicos = rolesIds.Distinct().ToList();

            var rolesValidosCount = await _context.rol
                .Where(r => r.activo && idsUnicos.Contains(r.id))
                .CountAsync();

            return rolesValidosCount == idsUnicos.Count;
        }

        public async Task CrearAsync(Usuario entidad, List<int> roles_ids)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                entidad.activo = true;
                entidad.fecha_creacion = DateTime.UtcNow;
                entidad.fecha_actualizacion = DateTime.UtcNow;

                await _context.usuario.AddAsync(entidad);
                await _context.SaveChangesAsync();

                foreach (var rolId in roles_ids.Distinct())
                {
                    var rolUsuario = new RolUsuario
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

                var rolUsuarioRelaciones = await _context.rol_usuario.Where(ru => ru.usuario_id == id).ToListAsync();
                foreach (var ru in rolUsuarioRelaciones) ru.activo = false;

                await _context.SaveChangesAsync();
            }
        }
    }
}
