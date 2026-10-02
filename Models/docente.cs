using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mapa_conocimiento_api.Models
{
    public class docente
    {
        // La llave primaria es la cédula, NO es autoincremental
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int cedula { get; set; }
        
        public required string nombres { get; set; }
        public required string apellidos { get; set; }
        public string? genero { get; set; }
        public string? cargo { get; set; }
        public string? correo { get; set; }
        
        // Llave foránea (puede ser nula según la base de datos)
        public int? linea_investigacion_principal { get; set; }
        
        public bool activo { get; set; }

        // Propiedad de Navegación Virtual hacia linea_investigacion
        [ForeignKey("linea_investigacion_principal")]
        public virtual linea_investigacion? LineaInvestigacionRelacionada { get; set; }
    }
}