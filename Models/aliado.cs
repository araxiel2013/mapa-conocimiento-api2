using System.ComponentModel.DataAnnotations;

namespace mapa_conocimiento_api.Models
{
    // Clase entidad que mapea a la tabla aliado en PostgreSQL
    public class aliado
    {
        // La llave primaria es el NIT de la entidad externa (INT).
        // Se marca con [Key] ya que el nombre no sigue la convención automática "id".
        [Key]
        public int nit { get; set; }

        // Nombre o razón social de la entidad aliada
        public required string nombre { get; set; }

        // Atributo indispensable para implementar el borrado lógico (BOOLEAN DEFAULT TRUE)
        public bool activo { get; set; }
    }
}