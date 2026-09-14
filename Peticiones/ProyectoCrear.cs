using System;
using System.ComponentModel.DataAnnotations;

namespace mapa_conocimiento_api.Peticiones
{
    public class ProyectoCrear
    {
        [Required(ErrorMessage = "El campo titulo es obligatorio.")]
        public string titulo { get; set; } = string.Empty;
        
        public string? resumen { get; set; }
        public double? presupuesto { get; set; }
        public string? tipo_financiacion { get; set; }
        public string? tipo_fondos { get; set; }
        public DateTime? fecha_inicio { get; set; }
        public DateTime? fecha_fin { get; set; }
    }
}