using System.ComponentModel.DataAnnotations;

namespace mapa_conocimiento_api.Peticiones
{
    // DTO para validar los datos de entrada al crear (POST) un área de conocimiento
    public class AreaConocimientoCrear
    {
        [Required(ErrorMessage = "El campo gran_area es obligatorio.")]
        [StringLength(100, ErrorMessage = "El campo gran_area no puede exceder los 100 caracteres.")]
        public string gran_area { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo area es obligatorio.")]
        [StringLength(100, ErrorMessage = "El campo area no puede exceder los 100 caracteres.")]
        public string area { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo disciplina es obligatorio.")]
        [StringLength(100, ErrorMessage = "El campo disciplina no puede exceder los 100 caracteres.")]
        public string disciplina { get; set; } = string.Empty;
    }
}