using System.Collections.Generic;
using System.Threading.Tasks;
using mapa_conocimiento_api.Peticiones.Reportes;

namespace mapa_conocimiento_api.Repositorios
{
    public interface IReportesRepository
    {
        Task<KpisGeneralesDto> ObtenerKpisGeneralesAsync();
        Task<List<GraficoBarraDto>> ObtenerProyectosPorGranAreaAsync();
        Task<List<GraficoTortaDto>> ObtenerPresupuestoPorFinanciacionAsync();
        Task<List<GraficoBarraDto>> ObtenerProductosPorOdsAsync();
        Task<List<GraficoBarraDto>> ObtenerDocentesPorLineaYProyectosAsync();
        Task<List<GraficoTortaDto>> ObtenerPresupuestoPorLineaInvestigacionAsync();
        Task<List<GraficoBarraDto>> ObtenerProductosPorTipologiaAsync();
        Task<List<GraficoBarraDto>> ObtenerProyectosPorAreaAplicacionAsync();
        Task<List<GraficoBarraDto>> ObtenerProyectosPorAliadoAsync();
        Task<List<GraficoBarraDto>> ObtenerProduccionPorAnioAsync();
        Task<List<GraficoBarraDto>> ObtenerPalabrasClaveFrecuentesAsync();
    }
}

