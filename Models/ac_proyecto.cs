using System.ComponentModel.DataAnnotations.Schema;

namespace mapa_conocimiento_api.Models
{
    [Table("ac_proyecto")]
    public class AcProyecto
    {
        public int proyecto { get; set; }
        public int area_conocimiento { get; set; }
        public bool activo { get; set; }

        [ForeignKey("proyecto")]
        public virtual Proyecto? ProyectoRelacionado { get; set; }
        
        [ForeignKey("area_conocimiento")]
        public virtual AreaConocimiento? AreaConocimientoRelacionada { get; set; }
    }
}
