using System.ComponentModel.DataAnnotations.Schema;

namespace mapa_conocimiento_api.Models
{
    public class palabras_clave
    {
        public int proyecto { get; set; }
        public required string termino_clave { get; set; }
        public bool activo { get; set; }

        [ForeignKey("proyecto")]
        public virtual proyecto? ProyectoRelacionado { get; set; }
        
        [ForeignKey("termino_clave")]
        public virtual termino_clave? TerminoClaveRelacionado { get; set; }
    }
}