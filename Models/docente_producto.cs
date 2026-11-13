using System.ComponentModel.DataAnnotations.Schema;

namespace mapa_conocimiento_api.Models
{
    [Table("docente_producto")]
    public class DocenteProducto
    {
        public int docente { get; set; }
        public int producto { get; set; }
        public bool activo { get; set; }

        [ForeignKey("docente")]
        public virtual Docente? DocenteRelacionado { get; set; }
        
        [ForeignKey("producto")]
        public virtual Producto? ProductoRelacionado { get; set; }
    }
}
