using System.ComponentModel.DataAnnotations.Schema;

namespace mapa_conocimiento_api.Models
{
    [Table("proyecto_linea")]
    public class ProyectoLinea
    {
        public int proyecto { get; set; }
        public int linea_investigacion { get; set; }
        public bool activo { get; set; }

        [ForeignKey("proyecto")]
        public virtual Proyecto? ProyectoRelacionado { get; set; }
        
        [ForeignKey("linea_investigacion")]
        public virtual LineaInvestigacion? LineaInvestigacionRelacionada { get; set; }
    }
}
