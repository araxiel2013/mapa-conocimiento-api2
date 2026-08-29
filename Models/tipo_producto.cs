namespace mapa_conocimiento_api.Models
{
    // Clase entidad que mapea a la tabla tipo_producto en PostgreSQL
    public class tipo_producto
    {
        // Identificador y llave primaria (INT)
        public int id { get; set; }

        // Campos de clasificación según los lineamientos de Minciencias
        public required string categoria { get; set; }
        public required string clase { get; set; }
        public required string nombre { get; set; }
        public required string tipologia { get; set; }

        // Atributo indispensable para implementar el borrado lógico (BOOLEAN DEFAULT TRUE)
        public bool activo { get; set; }
    }
}