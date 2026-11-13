using System.Collections.Generic;
using System.Threading.Tasks;
using mapa_conocimiento_api.Models;

namespace mapa_conocimiento_api.Repositorios
{
    public interface IProyectoRepository
    {
        Task<IEnumerable<Proyecto>> ObtenerTodosAsync();
        Task<Proyecto?> ObtenerPorIdAsync(int id);
        Task CrearAsync(Proyecto entidad);
        Task ActualizarAsync(Proyecto entidad);
        Task EliminarAsync(int id);
    }
}
