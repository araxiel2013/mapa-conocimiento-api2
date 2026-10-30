using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using mapa_conocimiento_api.Models;
using mapa_conocimiento_api.Peticiones.Reportes;

namespace mapa_conocimiento_api.Repositorios
{
    public class ReportesRepository : IReportesRepository
    {
        private readonly postgresContext _context;

        public ReportesRepository(postgresContext context)
        {
            _context = context;
        }

        // Métrica 0: KPIs Generales
        public async Task<KpisGeneralesDto> ObtenerKpisGeneralesAsync()
        {
            var totalProyectos = await _context.proyecto
                .Where(p => p.activo)
                .CountAsync();

            var totalDocentes = await _context.docente
                .Where(d => d.activo)
                .CountAsync();

            var presupuestoTotal = await _context.proyecto
                .Where(p => p.activo)
                .SumAsync(p => (decimal?)p.presupuesto) ?? 0m;

            var totalProductos = await _context.producto
                .Where(pr => pr.activo)
                .CountAsync();

            return new KpisGeneralesDto
            {
                total_proyectos_activos = totalProyectos,
                total_docentes_investigadores = totalDocentes,
                presupuesto_total_historico = presupuestoTotal,
                total_productos_entregados = totalProductos
            };
        }

        // Métrica 1: Proyectos por Gran Área
        // Cruza: proyecto -> ac_proyecto -> area_conocimiento -> ods_proyecto
        public async Task<List<GraficoBarraDto>> ObtenerProyectosPorGranAreaAsync()
        {
            return await (
                from p in _context.proyecto
                where p.activo
                join acp in _context.ac_proyecto on p.id equals acp.proyecto
                where acp.activo
                join ac in _context.area_conocimiento on acp.area_conocimiento equals ac.id
                where ac.activo
                join ods in _context.ods_proyecto on p.id equals ods.proyecto into odsGroup
                from ods in odsGroup.DefaultIfEmpty()
                where ods == null || ods.activo
                group p by ac.gran_area into g
                select new GraficoBarraDto
                {
                    etiqueta = g.Key,
                    valor = g.Select(x => x.id).Distinct().Count()
                }
            )
            .OrderByDescending(x => x.valor)
            .ToListAsync();
        }

        // Métrica 2: Presupuesto por Financiación
        // Cruza: proyecto -> aliado_proyecto -> aliado -> ac_proyecto
        public async Task<List<GraficoTortaDto>> ObtenerPresupuestoPorFinanciacionAsync()
        {
            return await (
                from p in _context.proyecto
                where p.activo
                join ap in _context.aliado_proyecto on p.id equals ap.proyecto into apGroup
                from ap in apGroup.DefaultIfEmpty()
                where ap == null || ap.activo
                join a in _context.aliado on ap.aliado equals a.nit into aGroup
                from a in aGroup.DefaultIfEmpty()
                where a == null || a.activo
                join acp in _context.ac_proyecto on p.id equals acp.proyecto into acpGroup
                from acp in acpGroup.DefaultIfEmpty()
                where acp == null || acp.activo
                select new { p.id, p.tipo_financiacion, p.presupuesto }
            )
            .Distinct()
            .GroupBy(x => x.tipo_financiacion)
            .Select(g => new GraficoTortaDto
            {
                nombre = g.Key,
                cantidad = (decimal)g.Sum(x => x.presupuesto)
            })
            .OrderByDescending(x => x.cantidad)
            .ToListAsync();
        }

        // Métrica 3: Productos por ODS
        // Cruza: producto -> proyecto -> ods_proyecto -> objetivo_desarrollo_sostenible
        public async Task<List<GraficoBarraDto>> ObtenerProductosPorOdsAsync()
        {
            return await (
                from pr in _context.producto
                where pr.activo
                join p in _context.proyecto on pr.proyecto_id equals p.id
                where p.activo
                join op in _context.ods_proyecto on p.id equals op.proyecto
                where op.activo
                join ods in _context.objetivo_desarrollo_sostenible on op.ods equals ods.id
                where ods.activo
                group pr by ods.nombre into g
                select new GraficoBarraDto
                {
                    etiqueta = g.Key,
                    valor = g.Select(x => x.id).Distinct().Count()
                }
            )
            .OrderByDescending(x => x.valor)
            .ToListAsync();
        }

        // Métrica 4: Docentes por Línea y Participación en Proyectos
        // Cruza: docente -> linea_investigacion -> desarrolla -> proyecto
        public async Task<List<GraficoBarraDto>> ObtenerDocentesPorLineaYProyectosAsync()
        {
            return await (
                from d in _context.docente
                where d.activo
                join li in _context.linea_investigacion on d.linea_investigacion_principal equals li.id
                where li.activo
                join des in _context.desarrolla on d.cedula equals des.docente
                where des.activo
                join p in _context.proyecto on des.proyecto equals p.id
                where p.activo
                group des by li.nombre into g
                select new GraficoBarraDto
                {
                    etiqueta = g.Key,
                    valor = g.Count()
                }
            )
            .OrderByDescending(x => x.valor)
            .ToListAsync();
        }

        // Métrica 5: Presupuesto por Línea de Investigación
        // Cruza: proyecto -> proyecto_linea -> linea_investigacion -> ac_proyecto
        public async Task<List<GraficoTortaDto>> ObtenerPresupuestoPorLineaInvestigacionAsync()
        {
            return await (
                from p in _context.proyecto
                where p.activo
                join pl in _context.proyecto_linea on p.id equals pl.proyecto
                where pl.activo
                join li in _context.linea_investigacion on pl.linea_investigacion equals li.id
                where li.activo
                join acp in _context.ac_proyecto on p.id equals acp.proyecto into acpGroup
                from acp in acpGroup.DefaultIfEmpty()
                where acp == null || acp.activo
                select new { p.id, Linea = li.nombre, p.presupuesto }
            )
            .Distinct()
            .GroupBy(x => x.Linea)
            .Select(g => new GraficoTortaDto
            {
                nombre = g.Key,
                cantidad = (decimal)g.Sum(x => x.presupuesto)
            })
            .OrderByDescending(x => x.cantidad)
            .ToListAsync();
        }

        // Métrica 6: Productos por Tipología
        // Cruza: producto -> tipo_producto -> proyecto -> docente_producto
        public async Task<List<GraficoBarraDto>> ObtenerProductosPorTipologiaAsync()
        {
            return await (
                from pr in _context.producto
                where pr.activo
                join tp in _context.tipo_producto on pr.tipo_producto_id equals tp.id
                where tp.activo
                join p in _context.proyecto on pr.proyecto_id equals p.id
                where p.activo
                join dp in _context.docente_producto on pr.id equals dp.producto into dpGroup
                from dp in dpGroup.DefaultIfEmpty()
                where dp == null || dp.activo
                group pr by tp.tipologia into g
                select new GraficoBarraDto
                {
                    etiqueta = g.Key,
                    valor = g.Select(x => x.id).Distinct().Count()
                }
            )
            .OrderByDescending(x => x.valor)
            .ToListAsync();
        }

        // Métrica 7: Proyectos por Área de Aplicación
        // Cruza: proyecto -> aa_proyecto -> area_aplicacion -> producto
        public async Task<List<GraficoBarraDto>> ObtenerProyectosPorAreaAplicacionAsync()
        {
            return await (
                from p in _context.proyecto
                where p.activo
                join aap in _context.aa_proyecto on p.id equals aap.proyecto
                where aap.activo
                join aa in _context.area_aplicacion on aap.area_aplicacion equals aa.id
                where aa.activo
                join pr in _context.producto on p.id equals pr.proyecto_id into prGroup
                from pr in prGroup.DefaultIfEmpty()
                where pr == null || pr.activo
                group p by aa.nombre into g
                select new GraficoBarraDto
                {
                    etiqueta = g.Key,
                    valor = g.Select(x => x.id).Distinct().Count()
                }
            )
            .OrderByDescending(x => x.valor)
            .ToListAsync();
        }

        // Métrica 8: Proyectos por Aliado
        // Cruza: aliado -> aliado_proyecto -> proyecto -> producto
        public async Task<List<GraficoBarraDto>> ObtenerProyectosPorAliadoAsync()
        {
            return await (
                from a in _context.aliado
                where a.activo
                join ap in _context.aliado_proyecto on a.nit equals ap.aliado
                where ap.activo
                join p in _context.proyecto on ap.proyecto equals p.id
                where p.activo
                join pr in _context.producto on p.id equals pr.proyecto_id into prGroup
                from pr in prGroup.DefaultIfEmpty()
                where pr == null || pr.activo
                group p by a.razon_social into g
                select new GraficoBarraDto
                {
                    etiqueta = g.Key,
                    valor = g.Select(x => x.id).Distinct().Count()
                }
            )
            .OrderByDescending(x => x.valor)
            .ToListAsync();
        }

        // Métrica 9: Producción por Año
        // Cruza: proyecto -> producto -> tipo_producto -> docente_producto
        // Formateo de etiqueta en memoria después de ToListAsync()
        public async Task<List<GraficoBarraDto>> ObtenerProduccionPorAnioAsync()
        {
            var datosAgrupados = await (
                from p in _context.proyecto
                where p.activo
                join pr in _context.producto on p.id equals pr.proyecto_id
                where pr.activo
                join tp in _context.tipo_producto on pr.tipo_producto_id equals tp.id
                where tp.activo
                join dp in _context.docente_producto on pr.id equals dp.producto into dpGroup
                from dp in dpGroup.DefaultIfEmpty()
                where dp == null || dp.activo
                group pr by p.fecha_inicio.Year into g
                select new
                {
                    Anio = g.Key,
                    Total = g.Select(x => x.id).Distinct().Count()
                }
            )
            .OrderBy(x => x.Anio)
            .ToListAsync();

            return datosAgrupados.Select(x => new GraficoBarraDto
            {
                etiqueta = x.Anio.ToString(),
                valor = x.Total
            }).ToList();
        }

        // Métrica 10: Palabras Clave más Frecuentes (Top 10)
        // Cruza: palabras_clave -> termino_clave -> proyecto -> ac_proyecto
        public async Task<List<GraficoBarraDto>> ObtenerPalabrasClaveFrecuentesAsync()
        {
            return await (
                from pk in _context.palabras_clave
                where pk.activo
                join tc in _context.termino_clave on pk.termino_clave equals tc.termino
                where tc.activo
                join p in _context.proyecto on pk.proyecto equals p.id
                where p.activo
                join acp in _context.ac_proyecto on p.id equals acp.proyecto into acpGroup
                from acp in acpGroup.DefaultIfEmpty()
                where acp == null || acp.activo
                group p by tc.termino into g
                select new GraficoBarraDto
                {
                    etiqueta = g.Key,
                    valor = g.Select(x => x.id).Distinct().Count()
                }
            )
            .OrderByDescending(x => x.valor)
            .Take(10)
            .ToListAsync();
        }
    }
}

