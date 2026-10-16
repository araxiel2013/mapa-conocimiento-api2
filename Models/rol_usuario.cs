using System.ComponentModel.DataAnnotations.Schema;

namespace mapa_conocimiento_api.Models
{
    public class rol_usuario
    {
        public int usuario_id { get; set; }
        public int rol_id { get; set; }
        public bool activo { get; set; }

        [ForeignKey("usuario_id")]
        public virtual usuario? UsuarioRelacionado { get; set; }
        
        [ForeignKey("rol_id")]
        public virtual rol? RolRelacionado { get; set; }
    }
}