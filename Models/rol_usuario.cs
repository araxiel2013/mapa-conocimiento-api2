using System.ComponentModel.DataAnnotations.Schema;

namespace mapa_conocimiento_api.Models
{
    [Table("rol_usuario")]
    public class RolUsuario
    {
        public int usuario_id { get; set; }
        public int rol_id { get; set; }
        public bool activo { get; set; }

        [ForeignKey("usuario_id")]
        public virtual Usuario? UsuarioRelacionado { get; set; }
        
        [ForeignKey("rol_id")]
        public virtual Rol? RolRelacionado { get; set; }
    }
}
