using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mapa_conocimiento_api.Models
{
    public class producto
    {
        [Key]
        public int id { get; set; }
        
        public required string nombre { get; set; }
        public required string categoria { get; set; }

        [Column(TypeName = "date")]
        public DateTime fecha_entrega { get; set; }
        
        // Llaves foráneas físicas
        [Column("proyecto")]
        public int? proyecto_id { get; set; }

        [Column("tipo_producto")]
        public int tipo_producto_id { get; set; }
        
        public bool activo { get; set; } = true;

        // Propiedades de Navegación Virtual
        [ForeignKey("proyecto_id")]
        public virtual proyecto? ProyectoRelacionado { get; set; }
        
        [ForeignKey("tipo_producto_id")]
        public virtual tipo_producto? TipoProductoRelacionado { get; set; }
    }
}