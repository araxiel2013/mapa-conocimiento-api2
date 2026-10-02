using System.ComponentModel.DataAnnotations.Schema;

namespace mapa_conocimiento_api.Models
{
    public class aliado_proyecto
    {
        public int aliado { get; set; }
        public int proyecto { get; set; }
        public bool activo { get; set; }

        [ForeignKey("aliado")]
        public virtual aliado? AliadoRelacionado { get; set; }
        
        [ForeignKey("proyecto")]
        public virtual proyecto? ProyectoRelacionado { get; set; }
    }
}