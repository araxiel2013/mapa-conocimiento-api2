using System.ComponentModel.DataAnnotations;

namespace mapa_conocimiento_api.Peticiones
{
    // DTO para validar los datos de entrada al crear (POST) un aliado
    public class AliadoCrear
    {
        // Al no ser autoincremental, el NIT debe ser enviado. 
        // Usamos Range para garantizar que sea un valor numérico positivo válido y no el 0 por defecto.
        [Required(ErrorMessage = "El campo nit es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El nit debe ser un número mayor a 0.")]
        public int nit { get; set; }

        [Required(ErrorMessage = "El campo nombre es obligatorio.")]
        [StringLength(200, ErrorMessage = "El campo nombre no puede exceder los 200 caracteres.")]
        public string nombre { get; set; } = string.Empty;
    }
}