using System.ComponentModel.DataAnnotations;

namespace mapa_conocimiento_api.Peticiones
{
    // DTO para validar los datos de entrada al actualizar por completo (PUT) un proyecto
    public class ProyectoEditar
    {
        [Required(ErrorMessage = "El campo nombre es obligatorio para el reemplazo completo.")]
        [StringLength(250, ErrorMessage = "El campo nombre no puede exceder los 250 caracteres.")]
        public string nombre { get; set; } = string.Empty;
    }
}