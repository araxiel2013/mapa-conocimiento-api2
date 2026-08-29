namespace mapa_conocimiento_api.Models
{
    // Clase entidad que mapea a la tabla proyecto en PostgreSQL
    public class proyecto
    {
        // Identificador y llave primaria (INT)
        public int id { get; set; }

        // Título o nombre del proyecto de investigación
        public required string nombre { get; set; }

        // Atributo indispensable para implementar el borrado lógico (BOOLEAN DEFAULT TRUE)
        public bool activo { get; set; }
    }
}