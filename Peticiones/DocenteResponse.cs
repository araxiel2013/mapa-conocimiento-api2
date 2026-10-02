namespace mapa_conocimiento_api.Peticiones
{
    // DTO de salida (Response): Sirve para dar formato al JSON del GET
    public class DocenteResponse
    {
        public int cedula { get; set; }
        public required string nombres { get; set; }
        public required string apellidos { get; set; }
        public string? genero { get; set; }
        public string? cargo { get; set; }
        public string? correo { get; set; }
        public bool activo { get; set; }

        // Objeto anidado que evita las referencias circulares de EF Core
        public ItemRelacionadoResponse? linea_investigacion { get; set; }
    }
}