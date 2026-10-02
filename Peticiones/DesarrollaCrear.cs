using System.ComponentModel.DataAnnotations;

namespace mapa_conocimiento_api.Peticiones
{
    public class DesarrollaCrear
    {
        [Required(ErrorMessage = "La cédula del docente es obligatoria.")]
        public int docente_cedula { get; set; }

        [Required(ErrorMessage = "El ID del proyecto es obligatorio.")]
        public int proyecto_id { get; set; }

        // Campos adicionales de esta tabla puente
        public string? rol { get; set; }
        public string? descripcion { get; set; }
    }
}