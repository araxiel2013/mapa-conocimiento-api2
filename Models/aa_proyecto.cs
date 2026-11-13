using System.ComponentModel.DataAnnotations.Schema;

namespace mapa_conocimiento_api.Models
{
    [Table("aa_proyecto")]
    public class AaProyecto
    {
        public int proyecto { get; set; }
        public int area_aplicacion { get; set; }
        public bool activo { get; set; }

        [ForeignKey("proyecto")]
        public virtual Proyecto? ProyectoRelacionado { get; set; }
        
        [ForeignKey("area_aplicacion")]
        public virtual AreaAplicacion? AreaAplicacionRelacionada { get; set; }
    }
}
