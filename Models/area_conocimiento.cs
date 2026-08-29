namespace mapa_conocimiento_api.Models
{
    // Clase entidad que mapea a la tabla area_conocimiento en PostgreSQL
    public class area_conocimiento
    {
        // Identificador y llave primaria (INT)
        public int id { get; set; }

        // Atributos de texto requeridos por el modelo de datos
        public required string gran_area { get; set; }
        public required string area { get; set; }
        public required string disciplina { get; set; }

        // Atributo indispensable para implementar el borrado lógico (BOOLEAN DEFAULT TRUE)
        public bool activo { get; set; }
    }
}