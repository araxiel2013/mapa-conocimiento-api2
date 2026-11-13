using System.Text.Json.Serialization;

namespace mapa_conocimiento_api.Peticiones.Reportes
{
    public class GraficoTortaDto
    {
        [JsonPropertyName("nombre")]
        public required string nombre { get; set; }

        [JsonPropertyName("cantidad")]
        public decimal cantidad { get; set; }
    }
}

