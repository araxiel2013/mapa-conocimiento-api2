using System.Collections.Generic;

namespace mapa_conocimiento_api.Peticiones
{
    public class ColeccionResponse<T>
    {
        public required string tabla { get; set; }
        public int total { get; set; }
        public required IEnumerable<T> datos { get; set; }
    }

    public class ApiResponse<T>
    {
        public int estado { get; set; }
        public required string mensaje { get; set; }
        public T? datos { get; set; }
    }

    public class ApiMensajeResponse
    {
        public int estado { get; set; }
        public required string mensaje { get; set; }
    }
}

