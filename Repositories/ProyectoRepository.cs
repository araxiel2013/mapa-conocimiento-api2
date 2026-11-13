using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using mapa_conocimiento_api.Models;

namespace mapa_conocimiento_api.Repositorios
{
    public class ProyectoRepository : IProyectoRepository
    {
        private readonly postgresContext _context;

        public ProyectoRepository(postgresContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Proyecto>> ObtenerTodosAsync()
        {
            return await _context.proyecto.ToListAsync();
        }

        public async Task<Proyecto?> ObtenerPorIdAsync(int id)
        {
            return await _context.proyecto.FindAsync(id);
        }

        public async Task CrearAsync(Proyecto entidad)
        {
            var tituloNormalizado = entidad.titulo.Trim();
            var existente = await _context.proyecto
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(p => (entidad.id > 0 && p.id == entidad.id) ||
                                          p.titulo.ToLower() == tituloNormalizado.ToLower());

            if (existente != null)
            {
                if (existente.activo)
                {
                    throw new ArgumentException($"Ya existe un proyecto con el título '{entidad.titulo}'.");
                }

                existente.titulo = tituloNormalizado;
                existente.resumen = entidad.resumen;
                existente.presupuesto = entidad.presupuesto;
                existente.tipo_financiacion = entidad.tipo_financiacion;
                existente.tipo_fondos = entidad.tipo_fondos;
                existente.fecha_inicio = entidad.fecha_inicio;
                existente.fecha_fin = entidad.fecha_fin;
                existente.activo = true;
                await _context.SaveChangesAsync();
                entidad.id = existente.id;
                return;
            }

            entidad.titulo = tituloNormalizado;
            entidad.activo = true;
            await _context.proyecto.AddAsync(entidad);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(Proyecto entidad)
        {
            _context.proyecto.Update(entidad);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var entidad = await _context.proyecto.FindAsync(id);
            if (entidad != null)
            {
                entidad.activo = false;

                // Cascada lógica sobre las tablas puente asociadas al proyecto
                var acRelaciones = await _context.ac_proyecto.Where(r => r.proyecto == id).ToListAsync();
                foreach (var r in acRelaciones) r.activo = false;

                var aaRelaciones = await _context.aa_proyecto.Where(r => r.proyecto == id).ToListAsync();
                foreach (var r in aaRelaciones) r.activo = false;

                var aliadoRelaciones = await _context.aliado_proyecto.Where(r => r.proyecto == id).ToListAsync();
                foreach (var r in aliadoRelaciones) r.activo = false;

                var desarrollaRelaciones = await _context.desarrolla.Where(r => r.proyecto == id).ToListAsync();
                foreach (var r in desarrollaRelaciones) r.activo = false;

                var odsRelaciones = await _context.ods_proyecto.Where(r => r.proyecto == id).ToListAsync();
                foreach (var r in odsRelaciones) r.activo = false;

                var palabrasRelaciones = await _context.palabras_clave.Where(r => r.proyecto == id).ToListAsync();
                foreach (var r in palabrasRelaciones) r.activo = false;

                var lineaRelaciones = await _context.proyecto_linea.Where(r => r.proyecto == id).ToListAsync();
                foreach (var r in lineaRelaciones) r.activo = false;

                // Cascada lógica sobre productos vinculados y sus relaciones puente (docente_producto) (ERR-09 / REG-03)
                var productos = await _context.producto
                    .Where(p => p.proyecto_id == id && p.activo)
                    .ToListAsync();

                if (productos.Count > 0)
                {
                    var productoIds = productos.Select(p => p.id).ToList();
                    var docenteProductoRelaciones = await _context.docente_producto
                        .Where(dp => productoIds.Contains(dp.producto) && dp.activo)
                        .ToListAsync();

                    foreach (var dp in docenteProductoRelaciones) dp.activo = false;
                    foreach (var prod in productos) prod.activo = false;
                }

                await _context.SaveChangesAsync();
            }
        }
    }
}
