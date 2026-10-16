using Microsoft.EntityFrameworkCore;
using mapa_conocimiento_api.Models;

namespace mapa_conocimiento_api.Repositorios
{
    public class DocenteRepository : IDocenteRepository
    {
        private readonly postgresContext _context;

        public DocenteRepository(postgresContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<docente>> ObtenerTodosAsync()
        {
            // CRÍTICO FASE 2: Eager Loading usando .Include() para traer los datos relacionales
            return await _context.docente
                .Include(d => d.LineaInvestigacionRelacionada)
                .ToListAsync();
        }

        public async Task<docente?> ObtenerPorIdAsync(int cedula)
        {
            return await _context.docente
                .Include(d => d.LineaInvestigacionRelacionada)
                .FirstOrDefaultAsync(d => d.cedula == cedula);
        }

        public async Task CrearAsync(docente entidad)
        {
            var existente = await _context.docente
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(d => d.cedula == entidad.cedula);

            if (existente != null)
            {
                if (existente.activo)
                {
                    throw new ArgumentException($"Ya existe un docente registrado con la cédula {entidad.cedula}.");
                }

                // Reactivación si estaba previamente inactivado (borrado lógico)
                existente.nombres = entidad.nombres;
                existente.apellidos = entidad.apellidos;
                existente.genero = entidad.genero;
                existente.cargo = entidad.cargo;
                existente.fecha_nacimiento = entidad.fecha_nacimiento;
                existente.correo = entidad.correo;
                existente.telefono = entidad.telefono;
                existente.url_cvlac = entidad.url_cvlac;
                existente.fecha_actualizacion = entidad.fecha_actualizacion;
                existente.escalafon = entidad.escalafon;
                existente.perfil = entidad.perfil;
                existente.cat_minciencia = entidad.cat_minciencia;
                existente.conv_minciencia = entidad.conv_minciencia;
                existente.nacionalidad = entidad.nacionalidad;
                existente.linea_investigacion_principal = entidad.linea_investigacion_principal;
                existente.activo = true;
                await _context.SaveChangesAsync();
                return;
            }

            entidad.activo = true;
            await _context.docente.AddAsync(entidad);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(docente entidad)
        {
            _context.docente.Update(entidad);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int cedula)
        {
            var entidad = await _context.docente.FindAsync(cedula);
            if (entidad != null)
            {
                entidad.activo = false;

                // Cascada lógica sobre las tablas puente asociadas al docente
                var desarrollaRelaciones = await _context.desarrolla.Where(d => d.docente == cedula).ToListAsync();
                foreach (var d in desarrollaRelaciones) d.activo = false;

                var docenteProductoRelaciones = await _context.docente_producto.Where(dp => dp.docente == cedula).ToListAsync();
                foreach (var dp in docenteProductoRelaciones) dp.activo = false;

                await _context.SaveChangesAsync();
            }
        }
    }
}