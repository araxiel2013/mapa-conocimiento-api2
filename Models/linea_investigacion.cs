using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mapa_conocimiento_api.Models
{
    [Table("linea_investigacion")]
    public class LineaInvestigacion
    {
        [Key]
        public int id { get; set; }
        public required string nombre { get; set; }
        public required string descripcion { get; set; }
        public bool activo { get; set; }
    }
}
