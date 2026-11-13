using Microsoft.EntityFrameworkCore;
using mapa_conocimiento_api.Models;

namespace mapa_conocimiento_api.Repositorios
{
    public class AliadoRepository : IAliadoRepository
    {
        private readonly postgresContext _context;

        public AliadoRepository(postgresContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Aliado>> ObtenerTodosAsync()
        {
            return await _context.aliado.ToListAsync();
        }

        public async Task<Aliado?> ObtenerPorIdAsync(int nit)
        {
            return await _context.aliado.FindAsync(nit);
        }

        public async Task CrearAsync(Aliado entidad)
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

                existente.razon_social = entidad.razon_social;
                existente.nombre_contacto = entidad.nombre_contacto;
                existente.correo = entidad.correo;
                existente.telefono = entidad.telefono;
                existente.ciudad = entidad.ciudad;
                existente.activo = true;
                await _context.SaveChangesAsync();
                return;
            }

            entidad.activo = true;
            await _context.aliado.AddAsync(entidad);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(Aliado entidad)
        {
            _context.aliado.Update(entidad);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int nit)
        {
            var entidad = await _context.aliado.FindAsync(nit);
            if (entidad != null)
            {
                entidad.activo = false;

                var aliadoProyectoRelaciones = await _context.aliado_proyecto.Where(ap => ap.aliado == nit).ToListAsync();
                foreach (var ap in aliadoProyectoRelaciones) ap.activo = false;

                await _context.SaveChangesAsync();
            }
        }
    }
}
