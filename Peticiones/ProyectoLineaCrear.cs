using System.ComponentModel.DataAnnotations;

namespace mapa_conocimiento_api.Peticiones
{
    public class ProyectoLineaCrear
    {
        [Required(ErrorMessage = "El ID del proyecto es obligatorio.")]
        public int proyecto_id { get; set; }

        [Required(ErrorMessage = "El ID de la línea de investigación es obligatorio.")]
        public int linea_investigacion_id { get; set; }
    }
}