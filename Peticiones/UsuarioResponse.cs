namespace mapa_conocimiento_api.Peticiones
{
    public class UsuarioResponse
    {
        public int id { get; set; }
        public required string username { get; set; }
        public required string email { get; set; }
        public string? nombre_completo { get; set; }
        public bool activo { get; set; }
        
        // Devolvemos solo los nombres de los roles para que React los pinte fácil
        public List<string> roles { get; set; } = new List<string>();
    }
}