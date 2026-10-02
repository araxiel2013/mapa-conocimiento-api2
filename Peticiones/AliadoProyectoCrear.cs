using System.ComponentModel.DataAnnotations;

namespace mapa_conocimiento_api.Peticiones
{
    public class AliadoProyectoCrear
    {
        [Required(ErrorMessage = "El NIT del aliado es obligatorio.")]
        public int aliado_nit { get; set; }

        [Required(ErrorMessage = "El ID del proyecto es obligatorio.")]
        public int proyecto_id { get; set; }
    }
}