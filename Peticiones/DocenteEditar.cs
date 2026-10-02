using System.ComponentModel.DataAnnotations;

namespace mapa_conocimiento_api.Peticiones
{
    public class DocenteEditar
    {
        // No incluimos la cédula porque es la llave primaria y no se debe cambiar
        [Required(ErrorMessage = "Los nombres son obligatorios.")]
        public required string nombres { get; set; }

        [Required(ErrorMessage = "Los apellidos son obligatorios.")]
        public required string apellidos { get; set; }

        public string? genero { get; set; }
        public string? cargo { get; set; }

        [EmailAddress(ErrorMessage = "El formato del correo no es válido.")]
        public string? correo { get; set; }

        public int? linea_investigacion_principal { get; set; }
    }
}