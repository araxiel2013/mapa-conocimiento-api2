using System.ComponentModel.DataAnnotations.Schema;

namespace mapa_conocimiento_api.Models
{
    public class ac_proyecto
    {
        public int proyecto { get; set; }
        public int area_conocimiento { get; set; }
        public bool activo { get; set; }

        [ForeignKey("proyecto")]
        public virtual proyecto? ProyectoRelacionado { get; set; }
        
        [ForeignKey("area_conocimiento")]
        public virtual area_conocimiento? AreaConocimientoRelacionada { get; set; }
    }
}