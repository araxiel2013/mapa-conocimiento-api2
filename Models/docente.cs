using System;
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

        // Mapeo hacia la columna de base de datos con el nombre real 'nacionalidaad'
        [Column("nacionalidaad")]
        public required string nacionalidad { get; set; }

        // Llave foránea (puede ser nula según la base de datos)
        public int? linea_investigacion_principal { get; set; }

        public bool activo { get; set; } = true;

        // Propiedad de Navegación Virtual hacia linea_investigacion
        [ForeignKey("linea_investigacion_principal")]
        public virtual linea_investigacion? LineaInvestigacionRelacionada { get; set; }
    }
}