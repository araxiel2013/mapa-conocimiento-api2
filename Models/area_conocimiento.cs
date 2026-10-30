using System.ComponentModel.DataAnnotations.Schema;

namespace mapa_conocimiento_api.Models
{
    [Table("area_conocimiento")]
    public class AreaConocimiento
    {
        public int id { get; set; }
        public required string gran_area { get; set; }
        public required string area { get; set; }
        public required string disciplina { get; set; }
        public bool activo { get; set; }
    }
}
