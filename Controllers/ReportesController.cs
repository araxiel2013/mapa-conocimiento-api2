using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using mapa_conocimiento_api.Peticiones.Reportes;
using mapa_conocimiento_api.Servicios;

namespace mapa_conocimiento_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // FASE 4: Reportes y Analítica protegidos por roles RBAC (Administrador y Consulta)
    [Authorize(Roles = "Administrador, Consulta, Administrador,Consulta")]
    public class ReportesController : ControllerBase
    {
        private readonly IReportesService _reportesService;

        public ReportesController(IReportesService reportesService)
        {
            _reportesService = reportesService;
        }

        // 1. KPIs Generales
        [HttpGet("kpis-generales")]
        public async Task<IActionResult> ObtenerKpisGenerales()
        {
            try
            {
                var resultado = await _reportesService.ObtenerKpisGeneralesAsync();
                return Ok(resultado ?? new KpisGeneralesDto());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    estado = 500,
                    mensaje = "Error interno al obtener los KPIs generales.",
                    detalle = ex.Message
                });
            }
        }

        // 2. Proyectos por Gran Área de Conocimiento
        [HttpGet("proyectos-por-gran-area")]
        public async Task<IActionResult> ObtenerProyectosPorGranArea()
        {
            try
            {
                var resultado = await _reportesService.ObtenerProyectosPorGranAreaAsync();
                return Ok(resultado ?? new List<GraficoBarraDto>());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    estado = 500,
                    mensaje = "Error interno al obtener proyectos por gran área.",
                    detalle = ex.Message
                });
            }
        }

        // 3. Presupuesto por Fuente/Tipo de Financiación
        [HttpGet("presupuesto-por-financiacion")]
        public async Task<IActionResult> ObtenerPresupuestoPorFinanciacion()
        {
            try
            {
                var resultado = await _reportesService.ObtenerPresupuestoPorFinanciacionAsync();
                return Ok(resultado ?? new List<GraficoTortaDto>());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    estado = 500,
                    mensaje = "Error interno al obtener presupuesto por financiación.",
                    detalle = ex.Message
                });
            }
        }

        // 4. Productos por ODS
        [HttpGet("productos-por-ods")]
        public async Task<IActionResult> ObtenerProductosPorOds()
        {
            try
            {
                var resultado = await _reportesService.ObtenerProductosPorOdsAsync();
                return Ok(resultado ?? new List<GraficoBarraDto>());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    estado = 500,
                    mensaje = "Error interno al obtener productos por ODS.",
                    detalle = ex.Message
                });
            }
        }

        // 5. Docentes por Línea y Participación en Proyectos
        [HttpGet("docentes-por-linea-proyectos")]
        [HttpGet("proyectos-por-linea")]
        public async Task<IActionResult> ObtenerDocentesPorLineaYProyectos()
        {
            try
            {
                var resultado = await _reportesService.ObtenerDocentesPorLineaYProyectosAsync();
                return Ok(resultado ?? new List<GraficoBarraDto>());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    estado = 500,
                    mensaje = "Error interno al obtener docentes por línea y participación en proyectos.",
                    detalle = ex.Message
                });
            }
        }

        // 6. Presupuesto por Línea de Investigación
        [HttpGet("presupuesto-por-linea-investigacion")]
        public async Task<IActionResult> ObtenerPresupuestoPorLineaInvestigacion()
        {
            try
            {
                var resultado = await _reportesService.ObtenerPresupuestoPorLineaInvestigacionAsync();
                return Ok(resultado ?? new List<GraficoTortaDto>());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    estado = 500,
                    mensaje = "Error interno al obtener presupuesto por línea de investigación.",
                    detalle = ex.Message
                });
            }
        }

        // 7. Productos por Tipología
        [HttpGet("productos-por-tipologia")]
        public async Task<IActionResult> ObtenerProductosPorTipologia()
        {
            try
            {
                var resultado = await _reportesService.ObtenerProductosPorTipologiaAsync();
                return Ok(resultado ?? new List<GraficoBarraDto>());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    estado = 500,
                    mensaje = "Error interno al obtener productos por tipología.",
                    detalle = ex.Message
                });
            }
        }

        // 8. Proyectos por Área de Aplicación
        [HttpGet("proyectos-por-area-aplicacion")]
        public async Task<IActionResult> ObtenerProyectosPorAreaAplicacion()
        {
            try
            {
                var resultado = await _reportesService.ObtenerProyectosPorAreaAplicacionAsync();
                return Ok(resultado ?? new List<GraficoBarraDto>());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    estado = 500,
                    mensaje = "Error interno al obtener proyectos por área de aplicación.",
                    detalle = ex.Message
                });
            }
        }

        // 9. Proyectos por Aliado
        [HttpGet("proyectos-por-aliado")]
        public async Task<IActionResult> ObtenerProyectosPorAliado()
        {
            try
            {
                var resultado = await _reportesService.ObtenerProyectosPorAliadoAsync();
                return Ok(resultado ?? new List<GraficoBarraDto>());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    estado = 500,
                    mensaje = "Error interno al obtener proyectos por aliado.",
                    detalle = ex.Message
                });
            }
        }

        // 10. Producción por Año
        [HttpGet("produccion-por-anio")]
        public async Task<IActionResult> ObtenerProduccionPorAnio()
        {
            try
            {
                var resultado = await _reportesService.ObtenerProduccionPorAnioAsync();
                return Ok(resultado ?? new List<GraficoBarraDto>());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    estado = 500,
                    mensaje = "Error interno al obtener producción por año.",
                    detalle = ex.Message
                });
            }
        }

        // 11. Palabras Clave más Frecuentes
        [HttpGet("palabras-clave-frecuentes")]
        public async Task<IActionResult> ObtenerPalabrasClaveFrecuentes()
        {
            try
            {
                var resultado = await _reportesService.ObtenerPalabrasClaveFrecuentesAsync();
                return Ok(resultado ?? new List<GraficoBarraDto>());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    estado = 500,
                    mensaje = "Error interno al obtener palabras clave más frecuentes.",
                    detalle = ex.Message
                });
            }
        }
    }
}

