namespace mapa_conocimiento_api.Peticiones
{
    // Clase auxiliar para devolver objetos anidados limpios en los GET (Eager Loading)
    public class ItemRelacionadoResponse
    {
        public int id { get; set; }
        public required string nombre { get; set; }
    }
}