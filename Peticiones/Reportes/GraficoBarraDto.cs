using System.Text.Json.Serialization;

namespace mapa_conocimiento_api.Peticiones.Reportes
{
    public class GraficoBarraDto
    {
        [JsonPropertyName("etiqueta")]
        public required string etiqueta { get; set; }

        [JsonPropertyName("valor")]
        public decimal valor { get; set; }
    }
}

