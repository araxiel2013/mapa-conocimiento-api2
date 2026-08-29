using System.ComponentModel.DataAnnotations;

namespace mapa_conocimiento_api.Peticiones
{
    // DTO para validar los datos de entrada al crear (POST) un tipo de producto
    public class TipoProductoCrear
    {
        [Required(ErrorMessage = "El campo categoria es obligatorio.")]
        [StringLength(100, ErrorMessage = "El campo categoria no puede exceder los 100 caracteres.")]
        public string categoria { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo clase es obligatorio.")]
        [StringLength(100, ErrorMessage = "El campo clase no puede exceder los 100 caracteres.")]
        public string clase { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo nombre es obligatorio.")]
        [StringLength(200, ErrorMessage = "El campo nombre no puede exceder los 200 caracteres.")]
        public string nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El campo tipologia es obligatorio.")]
        [StringLength(100, ErrorMessage = "El campo tipologia no puede exceder los 100 caracteres.")]
        public string tipologia { get; set; } = string.Empty;
    }
}