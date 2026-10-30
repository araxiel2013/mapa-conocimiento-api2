using System.ComponentModel.DataAnnotations.Schema;

namespace mapa_conocimiento_api.Models
{
    [Table("aliado_proyecto")]
    public class AliadoProyecto
    {
        public int aliado { get; set; }
        public int proyecto { get; set; }
        public bool activo { get; set; }

        [ForeignKey("aliado")]
        public virtual Aliado? AliadoRelacionado { get; set; }
        
        [ForeignKey("proyecto")]
        public virtual Proyecto? ProyectoRelacionado { get; set; }
    }
}
