using System.ComponentModel.DataAnnotations.Schema;

namespace mapa_conocimiento_api.Models
{
    [Table("ods_proyecto")]
    public class OdsProyecto
    {
        public int proyecto { get; set; }
        public int ods { get; set; }
        public bool activo { get; set; }

        [ForeignKey("proyecto")]
        public virtual Proyecto? ProyectoRelacionado { get; set; }
        
        [ForeignKey("ods")]
        public virtual ObjetivoDesarrolloSostenible? OdsRelacionado { get; set; }
    }
}
