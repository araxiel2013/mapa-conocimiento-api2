using System.ComponentModel.DataAnnotations;

namespace mapa_conocimiento_api.Peticiones
{
    public class AaProyectoCrear
    {
        [Required] public int proyecto_id { get; set; }
        [Required] public int area_aplicacion_id { get; set; }
    }
}
