namespace mapa_conocimiento_api.Peticiones
{
    // DTO de salida (Response): Aplanamos las relaciones para entregar un JSON limpio
    public class ProductoResponse
    {
        public int id { get; set; }
        public required string nombre { get; set; }
        public required string categoria { get; set; }
        public DateTime fecha_entrega { get; set; }
        public bool activo { get; set; }

        // Reutilizamos el comodín creado anteriormente
        public ItemRelacionadoResponse? proyecto { get; set; }
        
        // Relación con tipo_producto (puede ser nulo si la entidad relacionada fue inactivada o no existe)
        public ItemRelacionadoResponse? tipo_producto { get; set; }
    }
}