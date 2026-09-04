using System.ComponentModel.DataAnnotations;

namespace mapa_conocimiento_api.Peticiones
{
    // DTO para validar los datos de entrada al crear (POST) un aliado
public class AliadoCrear
    {
    public int nit { get; set; }
    public required string razon_social { get; set; }
    public required string nombre_contacto { get; set; }
    public required string correo { get; set; }
    public required string telefono { get; set; }
    public required string ciudad { get; set; }
    }
}