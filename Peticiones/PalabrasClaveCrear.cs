using System.ComponentModel.DataAnnotations;

namespace mapa_conocimiento_api.Peticiones
{
    public class PalabrasClaveCrear
    {
        [Required(ErrorMessage = "El ID del proyecto es obligatorio.")]
        public int proyecto_id { get; set; }

        [Required(ErrorMessage = "El término clave es obligatorio.")]
        public required string termino_clave { get; set; }
    }
}