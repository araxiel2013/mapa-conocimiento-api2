using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mapa_conocimiento_api.Models
{
    [Table("docente")]
    public class Docente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int cedula { get; set; }

        public required string nombres { get; set; }
        public required string apellidos { get; set; }
        public required string genero { get; set; }
        public required string cargo { get; set; }

        [Column(TypeName = "date")]
        public DateTime fecha_nacimiento { get; set; }

        public required string correo { get; set; }
        public required string telefono { get; set; }
        public required string url_cvlac { get; set; }

        [Column(TypeName = "date")]
        public DateTime fecha_actualizacion { get; set; }

        public required string escalafon { get; set; }
        public required string perfil { get; set; }
        public string? cat_minciencia { get; set; }
        public required string conv_minciencia { get; set; }

        [Column("nacionalidaad")]
        public required string nacionalidad { get; set; }

        public int? linea_investigacion_principal { get; set; }

        public bool activo { get; set; } = true;

        [ForeignKey("linea_investigacion_principal")]
        public virtual LineaInvestigacion? LineaInvestigacionRelacionada { get; set; }
    }
}
