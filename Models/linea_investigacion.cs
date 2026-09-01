using System.ComponentModel.DataAnnotations;

namespace mapa_conocimiento_api.Models
{
    public class linea_investigacion
    {
        [Key]
        public int id { get; set; }
        public required string nombre { get; set; }
        public required string descripcion { get; set; }
        public bool activo { get; set; }
    }
}