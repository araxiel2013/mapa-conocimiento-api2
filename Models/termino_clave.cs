using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mapa_conocimiento_api.Models
{
    [Table("termino_clave")]
    public class TerminoClave
    {
        [Key]
        public required string termino { get; set; }
        public bool activo { get; set; }
    }
}
