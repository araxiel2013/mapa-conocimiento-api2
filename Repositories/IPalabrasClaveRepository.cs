using System.Threading.Tasks;
using mapa_conocimiento_api.Models;

namespace mapa_conocimiento_api.Repositorios
{
    public interface IPalabrasClaveRepository
    {
        Task<PalabrasClave?> ObtenerPorIdsAsync(int proyecto_id, string termino_clave);
        Task CrearAsync(PalabrasClave entidad);
        Task EliminarAsync(int proyecto_id, string termino_clave);
    }
}
