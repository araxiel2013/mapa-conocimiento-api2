using System.ComponentModel.DataAnnotations;

namespace mapa_conocimiento_api.Peticiones
{
    // DTO para validar los datos de entrada al crear (POST) un área de aplicación
    public class AreaAplicacionCrear
    {
        [Required(ErrorMessage = "El campo nombre es obligatorio.")]
        [StringLength(150, ErrorMessage = "El campo nombre no puede exceder los 150 caracteres.")]
        public string nombre { get; set; } = string.Empty;
    }
}