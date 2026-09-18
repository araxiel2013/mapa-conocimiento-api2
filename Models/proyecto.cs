using System;

namespace mapa_conocimiento_api.Models
{
    // Clase entidad que mapea a la tabla proyecto en PostgreSQL
    public class proyecto
    {
        public int id { get; set; }
        public required string titulo { get; set; }
        public string? resumen { get; set; }
        public double? presupuesto { get; set; }
        public string? tipo_financiacion { get; set; }
        public string? tipo_fondos { get; set; }
        public DateTime? fecha_inicio { get; set; }
        public DateTime? fecha_fin { get; set; }
        public bool activo { get; set; } = true;
    }
}