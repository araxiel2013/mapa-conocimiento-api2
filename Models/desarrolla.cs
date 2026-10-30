using System.ComponentModel.DataAnnotations.Schema;

namespace mapa_conocimiento_api.Models
{
    [Table("desarrolla")]
    public class Desarrolla
    {
        public int docente { get; set; }
        public int proyecto { get; set; }
        public string? rol { get; set; }
        public string? descripcion { get; set; }
        public bool activo { get; set; }

        [ForeignKey("docente")]
        public virtual Docente? DocenteRelacionado { get; set; }
        
        [ForeignKey("proyecto")]
        public virtual Proyecto? ProyectoRelacionado { get; set; }
    }
}
