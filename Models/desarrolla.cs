using System.ComponentModel.DataAnnotations.Schema;

namespace mapa_conocimiento_api.Models
{
    public class desarrolla
    {
        public int docente { get; set; }
        public int proyecto { get; set; }
        
        // Esta tabla puente tiene campos extra
        public string? rol { get; set; }
        public string? descripcion { get; set; }
        public bool activo { get; set; }

        [ForeignKey("docente")]
        public virtual docente? DocenteRelacionado { get; set; }
        
        [ForeignKey("proyecto")]
        public virtual proyecto? ProyectoRelacionado { get; set; }
    }
}