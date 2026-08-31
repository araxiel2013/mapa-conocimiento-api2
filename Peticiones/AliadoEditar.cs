using System.ComponentModel.DataAnnotations;

namespace mapa_conocimiento_api.Peticiones // O el namespace que estés usando
{
    public class AliadoEditar
    {
        public required string razon_social { get; set; }
        public required string nombre_contacto { get; set; }
        public required string correo { get; set; }
        public required string telefono { get; set; }
        public required string ciudad { get; set; }
    }
}