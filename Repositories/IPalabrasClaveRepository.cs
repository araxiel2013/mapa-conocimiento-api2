using System.Threading.Tasks;
using mapa_conocimiento_api.Models;

namespace mapa_conocimiento_api.Repositorios
{
    public interface IPalabrasClaveRepository
    {
        Task<palabras_clave?> ObtenerPorIdsAsync(int proyecto_id, string termino_clave);
        Task CrearAsync(palabras_clave entidad);
        Task EliminarAsync(int proyecto_id, string termino_clave);
    }
}
