using System.ComponentModel.DataAnnotations;

namespace mapa_conocimiento_api.Peticiones
{
    // DTO para validar los datos de entrada al actualizar por completo (PUT) un ODS
    public class ObjetivoDesarrolloSostenibleEditar
    {
        [Required(ErrorMessage = "El campo nombre es obligatorio para el reemplazo completo.")]
        [StringLength(200, ErrorMessage = "El campo nombre no puede exceder los 200 caracteres.")]
        public string nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo categoria es obligatorio para el reemplazo completo.")]
        [StringLength(100, ErrorMessage = "El campo categoria no puede exceder los 100 caracteres.")]
        public string categoria { get; set; } = string.Empty;
    }
}