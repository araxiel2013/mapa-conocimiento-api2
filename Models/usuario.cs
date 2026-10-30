using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mapa_conocimiento_api.Models
{
    [Table("usuario")]
    public class Usuario
    {
        [Key]
        public int id { get; set; }
        public required string username { get; set; }
        
        // OJO: Aquí se guarda el Hash de BCrypt (aprox 60 caracteres), NO texto plano
        public required string password { get; set; } 
        
        public required string email { get; set; }
        public string? nombre_completo { get; set; }
        public bool activo { get; set; }
        public DateTime fecha_creacion { get; set; }
        public DateTime fecha_actualizacion { get; set; }

        // Propiedad de navegación (Uno a Muchos hacia la tabla puente)
        public virtual ICollection<RolUsuario> RolesUsuario { get; set; } = new List<RolUsuario>();
    }
}
