using System.ComponentModel.DataAnnotations;

namespace mapa_conocimiento_api.Peticiones
{
    public class RecuperarContrasenaRequest
    {
        [Required(ErrorMessage = "El usuario o correo institucional es obligatorio")]
        public string identificador { get; set; } = string.Empty;

        [Required(ErrorMessage = "La nueva contraseña es obligatoria")]
        [MinLength(6, ErrorMessage = "La nueva contraseña debe tener al menos 6 caracteres")]
        public string nueva_contrasena { get; set; } = string.Empty;
    }
}
