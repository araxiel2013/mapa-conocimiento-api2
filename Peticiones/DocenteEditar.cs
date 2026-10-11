using System;
using System.ComponentModel.DataAnnotations;

namespace mapa_conocimiento_api.Peticiones
{
    public class DocenteEditar
    {
        [Required(ErrorMessage = "Los nombres son obligatorios.")]
        public required string nombres { get; set; }

        [Required(ErrorMessage = "Los apellidos son obligatorios.")]
        public required string apellidos { get; set; }

        [Required(ErrorMessage = "El género es obligatorio.")]
        public required string genero { get; set; }

        [Required(ErrorMessage = "El cargo es obligatorio.")]
        public required string cargo { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
        public DateTime fecha_nacimiento { get; set; }

        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "El formato del correo no es válido.")]
        public required string correo { get; set; }

        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        public required string telefono { get; set; }

        [Required(ErrorMessage = "La URL CvLAC es obligatoria.")]
        public required string url_cvlac { get; set; }

        public DateTime? fecha_actualizacion { get; set; }

        [Required(ErrorMessage = "El escalafón es obligatorio.")]
        public required string escalafon { get; set; }

        [Required(ErrorMessage = "El perfil es obligatorio.")]
        public required string perfil { get; set; }

        public string? cat_minciencia { get; set; }

        [Required(ErrorMessage = "La convocatoria Minciencias es obligatoria.")]
        public required string conv_minciencia { get; set; }

        public string? nacionalidad { get; set; }
        public string? nacionalidaad { get; set; }

        public int? linea_investigacion_principal { get; set; }
    }
}