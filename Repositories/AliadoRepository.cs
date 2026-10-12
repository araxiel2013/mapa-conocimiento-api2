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
            var existente = await _context.aliado
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(a => a.nit == entidad.nit);

            if (existente != null)
            {
                if (existente.activo)
                {
                    throw new ArgumentException($"Ya existe un aliado registrado con el NIT {entidad.nit}.");
                }

                // Reactivación si estaba previamente inactivado (borrado lógico)
                existente.razon_social = entidad.razon_social;
                existente.nombre_contacto = entidad.nombre_contacto;
                existente.correo = entidad.correo;
                existente.telefono = entidad.telefono;
                existente.ciudad = entidad.ciudad;
                existente.activo = true;
                await _context.SaveChangesAsync();
                return;
            }

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