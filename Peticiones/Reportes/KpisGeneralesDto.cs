using System.Text.Json.Serialization;

namespace mapa_conocimiento_api.Peticiones.Reportes
{
    public class KpisGeneralesDto
    {
        [JsonPropertyName("total_proyectos_activos")]
        public int total_proyectos_activos { get; set; }

        [JsonPropertyName("total_docentes_investigadores")]
        public int total_docentes_investigadores { get; set; }

        [JsonPropertyName("presupuesto_total_historico")]
        public decimal presupuesto_total_historico { get; set; }

        [JsonPropertyName("total_productos_entregados")]
        public int total_productos_entregados { get; set; }
    }
}

