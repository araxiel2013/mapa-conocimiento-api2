using System.ComponentModel.DataAnnotations.Schema;

namespace mapa_conocimiento_api.Models
{
    [Table("tipo_producto")]
    public class TipoProducto
    {
        public int id { get; set; }
        public required string categoria { get; set; }
        public required string clase { get; set; }
        public required string nombre { get; set; }
        public required string tipologia { get; set; }
        public bool activo { get; set; }
    }
}
