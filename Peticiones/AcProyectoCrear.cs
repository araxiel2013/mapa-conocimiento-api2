using System.ComponentModel.DataAnnotations;

namespace mapa_conocimiento_api.Peticiones
{
    public class AcProyectoCrear
    {
        [Required(ErrorMessage = "El ID del proyecto es obligatorio.")]
        public int proyecto_id { get; set; }

        [Required(ErrorMessage = "El ID del área de conocimiento es obligatorio.")]
        public int area_conocimiento_id { get; set; }
    }
}