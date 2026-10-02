using System.ComponentModel.DataAnnotations;

namespace mapa_conocimiento_api.Peticiones
{
    public class DocenteCrear
    {
        [Required(ErrorMessage = "La cédula es obligatoria.")]
        public int cedula { get; set; }

        [Required(ErrorMessage = "Los nombres son obligatorios.")]
        public required string nombres { get; set; }

        [Required(ErrorMessage = "Los apellidos son obligatorios.")]
        public required string apellidos { get; set; }

        public string? genero { get; set; }
        public string? cargo { get; set; }
        
        [EmailAddress(ErrorMessage = "El formato del correo no es válido.")]
        public string? correo { get; set; }

        // Aquí solo recibimos el ID que React envía desde el <select>
        public int? linea_investigacion_principal { get; set; }
    }
}