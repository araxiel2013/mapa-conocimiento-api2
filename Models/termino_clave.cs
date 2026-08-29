using System.ComponentModel.DataAnnotations;

namespace mapa_conocimiento_api.Models
{
    // Clase entidad que mapea a la tabla termino_clave en PostgreSQL
    public class termino_clave
    {
        // Ojo: La llave primaria es un string (VARCHAR), no un ID numérico autoincremental.
        // Usamos [Key] para que EF Core mapee correctamente este campo como identificador único.
        [Key]
        public required string termino { get; set; }

        // Atributo indispensable para implementar el borrado lógico (BOOLEAN DEFAULT TRUE)
        public bool activo { get; set; }
    }
}