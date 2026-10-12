using System;

namespace mapa_conocimiento_api.Peticiones
{
    public class DocenteResponse
    {
        public int cedula { get; set; }
        public required string nombres { get; set; }
        public required string apellidos { get; set; }
        public string? genero { get; set; }
        public string? cargo { get; set; }
        public DateTime fecha_nacimiento { get; set; }
        public string? correo { get; set; }
        public string? telefono { get; set; }
        public string? url_cvlac { get; set; }
        public DateTime fecha_actualizacion { get; set; }
        public string? escalafon { get; set; }
        public string? perfil { get; set; }
        public string? cat_minciencia { get; set; }
        public string? conv_minciencia { get; set; }
        public string? nacionalidad { get; set; }
        public bool activo { get; set; }

        public ItemRelacionadoResponse? linea_investigacion { get; set; }
    }
}