using System.ComponentModel.DataAnnotations;

namespace mapa_conocimiento_api.Peticiones
{
    // DTO para validar los datos de entrada al actualizar por completo (PUT) un área de aplicación
    public class AreaAplicacionEditar
    {
        [Required(ErrorMessage = "El campo nombre es obligatorio para el reemplazo completo.")]
        [StringLength(150, ErrorMessage = "El campo nombre no puede exceder los 150 caracteres.")]
        public string nombre { get; set; } = string.Empty;
    }
}