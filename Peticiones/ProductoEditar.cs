using System.ComponentModel.DataAnnotations;

namespace mapa_conocimiento_api.Peticiones
{
    public class ProductoEditar
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public required string nombre { get; set; }

        [Required(ErrorMessage = "La categoría es obligatoria.")]
        public required string categoria { get; set; }

        [Required(ErrorMessage = "La fecha de entrega es obligatoria.")]
        public DateTime fecha_entrega { get; set; }

        public int? proyecto_id { get; set; }

        [Required(ErrorMessage = "El ID del tipo de producto es obligatorio.")]
        public int tipo_producto_id { get; set; }
    }
}