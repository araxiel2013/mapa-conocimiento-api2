using System.ComponentModel.DataAnnotations;

namespace mapa_conocimiento_api.Peticiones
{
    // DTO para validar los datos de entrada al actualizar por completo (PUT) un aliado
    public class AliadoEditar
    {
        // El nit no se incluye aquí porque se envía a través del parámetro de la ruta (URL)
        [Required(ErrorMessage = "El campo nombre es obligatorio para el reemplazo completo.")]
        [StringLength(200, ErrorMessage = "El campo nombre no puede exceder los 200 caracteres.")]
        public string nombre { get; set; } = string.Empty;
    }
}