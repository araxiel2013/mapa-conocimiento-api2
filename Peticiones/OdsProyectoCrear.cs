using System.ComponentModel.DataAnnotations;

namespace mapa_conocimiento_api.Peticiones
{
    public class OdsProyectoCrear
    {
        [Required] public int proyecto_id { get; set; }
        [Required] public int ods_id { get; set; }
    }
}
