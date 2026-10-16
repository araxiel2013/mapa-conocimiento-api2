namespace mapa_conocimiento_api.Peticiones
{
    // Estructura exacta que espera React según el contrato
    public class LoginResponse
    {
        public required string token { get; set; }
        public required UsuarioAuthDto usuario { get; set; }
    }

    public class UsuarioAuthDto
    {
        public required string username { get; set; }
        public string? nombre_completo { get; set; }
        public required List<string> roles { get; set; }
    }
}