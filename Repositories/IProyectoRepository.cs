using System.Collections.Generic;
using System.Threading.Tasks;
using mapa_conocimiento_api.Models;

namespace mapa_conocimiento_api.Repositorios
{
    public interface IProyectoRepository
    {
        Task<IEnumerable<proyecto>> ObtenerTodosAsync();
        Task<proyecto?> ObtenerPorIdAsync(int id);
        Task CrearAsync(proyecto entidad);
        Task ActualizarAsync(proyecto entidad);
        Task EliminarAsync(int id);
    }
}

