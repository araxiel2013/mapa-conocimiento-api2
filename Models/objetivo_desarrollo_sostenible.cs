using System.ComponentModel.DataAnnotations.Schema;

namespace mapa_conocimiento_api.Models
{
    [Table("objetivo_desarrollo_sostenible")]
    public class ObjetivoDesarrolloSostenible
    {
        public int id { get; set; }
        public required string nombre { get; set; }
        public required string categoria { get; set; }
        public bool activo { get; set; }
    }
}
