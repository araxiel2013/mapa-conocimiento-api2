using System.ComponentModel.DataAnnotations.Schema;

namespace mapa_conocimiento_api.Models
{
    public class docente_producto
    {
        public int docente { get; set; }
        public int producto { get; set; }
        public bool activo { get; set; }

        [ForeignKey("docente")]
        public virtual docente? DocenteRelacionado { get; set; }
        
        [ForeignKey("producto")]
        public virtual producto? ProductoRelacionado { get; set; }
    }
}