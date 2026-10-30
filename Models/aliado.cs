using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mapa_conocimiento_api.Models
{
    [Table("aliado")]
    public class Aliado
    {
        [Key]
        public int nit { get; set; }
        public required string razon_social { get; set; }
        public required string nombre_contacto { get; set; }
        public required string correo { get; set; }
        public required string telefono { get; set; }
        public required string ciudad { get; set; }
        public bool activo { get; set; }
    }
}
