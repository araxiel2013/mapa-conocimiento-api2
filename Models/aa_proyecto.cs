using System.ComponentModel.DataAnnotations.Schema;

namespace mapa_conocimiento_api.Models
{
    public class aa_proyecto
    {
        public int proyecto { get; set; }
        public int area_aplicacion { get; set; }
        public bool activo { get; set; }

        [ForeignKey("proyecto")]
        public virtual proyecto? ProyectoRelacionado { get; set; }
        
        [ForeignKey("area_aplicacion")]
        public virtual area_aplicacion? AreaAplicacionRelacionada { get; set; }
    }
}