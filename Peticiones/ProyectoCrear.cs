using System;
using System.ComponentModel.DataAnnotations;

namespace mapa_conocimiento_api.Peticiones
{
    public class ProyectoCrear
    {
        [Required(ErrorMessage = "El campo titulo es obligatorio.")]
        public string titulo { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo resumen es obligatorio.")]
        public string resumen { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo presupuesto es obligatorio.")]
        [Range(0, double.MaxValue, ErrorMessage = "El presupuesto no puede ser negativo.")]
        public double? presupuesto { get; set; }

        [Required(ErrorMessage = "El campo tipo_financiacion es obligatorio.")]
        public string tipo_financiacion { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo tipo_fondos es obligatorio.")]
        public string tipo_fondos { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo fecha_inicio es obligatorio.")]
        public DateTime? fecha_inicio { get; set; }

        public DateTime? fecha_fin { get; set; }
    }
}