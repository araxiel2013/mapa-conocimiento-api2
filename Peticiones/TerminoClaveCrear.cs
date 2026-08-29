using System.ComponentModel.DataAnnotations;

namespace mapa_conocimiento_api.Peticiones
{
    // DTO para validar los datos de entrada al crear (POST) un término clave
    public class TerminoClaveCrear
    {
        // Al ser la llave primaria, el término debe ser enviado por el cliente en el POST
        [Required(ErrorMessage = "El campo termino es obligatorio, ya que funciona como identificador.")]
        [StringLength(100, ErrorMessage = "El campo termino no puede exceder los 100 caracteres.")]
        public string termino { get; set; } = string.Empty;
    }
}