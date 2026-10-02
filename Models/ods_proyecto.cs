using System.ComponentModel.DataAnnotations.Schema;

namespace mapa_conocimiento_api.Models
{
    public class ods_proyecto
    {
        public int proyecto { get; set; }
        public int ods { get; set; }
        public bool activo { get; set; }

        [ForeignKey("proyecto")]
        public virtual proyecto? ProyectoRelacionado { get; set; }
        
        [ForeignKey("ods")]
        public virtual objetivo_desarrollo_sostenible? OdsRelacionado { get; set; }
    }
}