namespace mapa_conocimiento_api.Models
{
    // Clase entidad que mapea a la tabla objetivo_desarrollo_sostenible en PostgreSQL
    public class objetivo_desarrollo_sostenible
    {
        // Identificador y llave primaria (INT)
        public int id { get; set; }

        // Nombre descriptivo del ODS
        public required string nombre { get; set; }

        // Campo específico indicado en el modelo de datos para clasificar el ODS
        public required string categoria { get; set; }

        // Atributo indispensable para implementar el borrado lógico
        public bool activo { get; set; }
    }
}