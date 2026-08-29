namespace mapa_conocimiento_api.Models
{
    // Clase entidad que mapea a la tabla area_aplicacion en PostgreSQL
    public class area_aplicacion
    {
        // Identificador y llave primaria (INT)
        public int id { get; set; }

        // Nombre del sector económico o industrial
        public required string nombre { get; set; }

        // Atributo indispensable para implementar el borrado lógico (BOOLEAN DEFAULT TRUE)
        public bool activo { get; set; }
    }
}