using System.ComponentModel.DataAnnotations;

namespace mapa_conocimiento_api.Peticiones
{
    // DTO para validar los datos de entrada al crear (POST) una línea de investigación
    public class LineaInvestigacionCrear
    {
        public required string nombre { get; set; }
        public required string descripcion { get; set; }
    }
}