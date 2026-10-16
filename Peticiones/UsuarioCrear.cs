using System.ComponentModel.DataAnnotations;

namespace mapa_conocimiento_api.Peticiones
{
    public class UsuarioCrear
    {
        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        public required string username { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
        public required string password { get; set; }

        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "Formato de correo inválido.")]
        public required string email { get; set; }

        public string? nombre_completo { get; set; }

        // Recibimos un arreglo de IDs para asignar múltiples roles de una vez
        [Required(ErrorMessage = "Debe asignar al menos un rol.")]
        public required List<int> roles_ids { get; set; }
    }
}