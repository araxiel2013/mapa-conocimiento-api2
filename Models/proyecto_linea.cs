using System.ComponentModel.DataAnnotations.Schema;

namespace mapa_conocimiento_api.Models
{
    public class proyecto_linea
    {
        public int proyecto { get; set; }
        public int linea_investigacion { get; set; }
        public bool activo { get; set; }

        [ForeignKey("proyecto")]
        public virtual proyecto? ProyectoRelacionado { get; set; }
        
        [ForeignKey("linea_investigacion")]
        public virtual linea_investigacion? LineaInvestigacionRelacionada { get; set; }
    }
}