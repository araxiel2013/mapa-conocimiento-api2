using System.ComponentModel.DataAnnotations;

namespace mapa_conocimiento_api.Models
{
    public class rol
    {
        [Key]
        public int id { get; set; }
        public required string nombre { get; set; }
        public string? descripcion { get; set; }
        public bool activo { get; set; }
        public DateTime fecha_creacion { get; set; }

        // Propiedad de navegación inversa
        public virtual ICollection<rol_usuario> RolesUsuario { get; set; } = new List<rol_usuario>();
    }
}