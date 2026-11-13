using System.Collections.Generic;
using System.Threading.Tasks;
using mapa_conocimiento_api.Peticiones.Reportes;
using mapa_conocimiento_api.Repositorios;

namespace mapa_conocimiento_api.Servicios
{
    public class ReportesService : IReportesService
    {
        private readonly IReportesRepository _reportesRepository;

        public ReportesService(IReportesRepository reportesRepository)
        {
            _reportesRepository = reportesRepository;
        }

        public async Task<KpisGeneralesDto> ObtenerKpisGeneralesAsync()
        {
            return await _reportesRepository.ObtenerKpisGeneralesAsync();
        }

        public async Task<List<GraficoBarraDto>> ObtenerProyectosPorGranAreaAsync()
        {
            return await _reportesRepository.ObtenerProyectosPorGranAreaAsync();
        }

        public async Task<List<GraficoTortaDto>> ObtenerPresupuestoPorFinanciacionAsync()
        {
            return await _reportesRepository.ObtenerPresupuestoPorFinanciacionAsync();
        }

        public async Task<List<GraficoBarraDto>> ObtenerProductosPorOdsAsync()
        {
            return await _reportesRepository.ObtenerProductosPorOdsAsync();
        }

        public async Task<List<GraficoBarraDto>> ObtenerDocentesPorLineaYProyectosAsync()
        {
            return await _reportesRepository.ObtenerDocentesPorLineaYProyectosAsync();
        }

        public async Task<List<GraficoTortaDto>> ObtenerPresupuestoPorLineaInvestigacionAsync()
        {
            return await _reportesRepository.ObtenerPresupuestoPorLineaInvestigacionAsync();
        }

        public async Task<List<GraficoBarraDto>> ObtenerProductosPorTipologiaAsync()
        {
            return await _reportesRepository.ObtenerProductosPorTipologiaAsync();
        }

        public async Task<List<GraficoBarraDto>> ObtenerProyectosPorAreaAplicacionAsync()
        {
            return await _reportesRepository.ObtenerProyectosPorAreaAplicacionAsync();
        }

        public async Task<List<GraficoBarraDto>> ObtenerProyectosPorAliadoAsync()
        {
            return await _reportesRepository.ObtenerProyectosPorAliadoAsync();
        }

        public async Task<List<GraficoBarraDto>> ObtenerProduccionPorAnioAsync()
        {
            return await _reportesRepository.ObtenerProduccionPorAnioAsync();
        }

        public async Task<List<GraficoBarraDto>> ObtenerPalabrasClaveFrecuentesAsync()
        {
            return await _reportesRepository.ObtenerPalabrasClaveFrecuentesAsync();
        }
    }
}

