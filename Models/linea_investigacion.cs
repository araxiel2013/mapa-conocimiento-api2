namespace mapa_conocimiento_api.Models
{
    // Clase entidad que mapea a la tabla linea_investigacion en PostgreSQL
    public class linea_investigacion
    {
        // Identificador y llave primaria. 
        // Nota: En la base de datos de Supabase este campo es SERIAL (autoincremental).
        public int id { get; set; }

        // Nombre descriptivo de la línea de investigación
        public required string nombre { get; set; }

        // Atributo indispensable para implementar el borrado lógico (BOOLEAN DEFAULT TRUE)
        public bool activo { get; set; }
    }
}