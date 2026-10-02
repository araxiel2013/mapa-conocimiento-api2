using System.ComponentModel.DataAnnotations;

namespace mapa_conocimiento_api.Peticiones
{
    public class DocenteProductoCrear
    {
        [Required] public int docente_cedula { get; set; }
        [Required] public int producto_id { get; set; }
    }
}
