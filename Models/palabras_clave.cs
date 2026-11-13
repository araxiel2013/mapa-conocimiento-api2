using System.ComponentModel.DataAnnotations.Schema;

namespace mapa_conocimiento_api.Models
{
    [Table("palabras_clave")]
    public class PalabrasClave
    {
        public int proyecto { get; set; }
        public required string termino_clave { get; set; }
        public bool activo { get; set; }

        [ForeignKey("proyecto")]
        public virtual Proyecto? ProyectoRelacionado { get; set; }
        
        [ForeignKey("termino_clave")]
        public virtual TerminoClave? TerminoClaveRelacionado { get; set; }
    }
}
