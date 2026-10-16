using System.ComponentModel.DataAnnotations;

namespace mapa_conocimiento_api.Peticiones
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        public required string username { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        public required string password { get; set; }
    }
}