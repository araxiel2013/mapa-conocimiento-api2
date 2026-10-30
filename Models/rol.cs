using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mapa_conocimiento_api.Models
{
    [Table("rol")]
    public class Rol
    {
        [Key]
        public int id { get; set; }
        public required string nombre { get; set; }
        public string? descripcion { get; set; }
        public bool activo { get; set; }
        public DateTime fecha_creacion { get; set; }

        // Propiedad de navegación inversa
        public virtual ICollection<RolUsuario> RolesUsuario { get; set; } = new List<RolUsuario>();
    }
}
