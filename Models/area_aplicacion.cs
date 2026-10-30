using System.ComponentModel.DataAnnotations.Schema;

namespace mapa_conocimiento_api.Models
{
    [Table("area_aplicacion")]
    public class AreaAplicacion
    {
        public int id { get; set; }
        public required string nombre { get; set; }
        public bool activo { get; set; }
    }
}
