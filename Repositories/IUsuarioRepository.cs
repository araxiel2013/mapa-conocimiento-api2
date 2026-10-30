using System.Collections.Generic;
using System.Threading.Tasks;
using mapa_conocimiento_api.Models;

namespace mapa_conocimiento_api.Repositorios
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<Usuario>> ObtenerTodosAsync();
        Task<Usuario?> ObtenerPorIdAsync(int id);
        Task<Usuario?> ObtenerPorUsernameConRolesAsync(string username);
        Task<bool> ExisteUsernameOEmailAsync(string username, string email);
        Task<bool> ExisteUsernameOEmailIgnorandoFiltrosAsync(string username, string email);
        Task<bool> ExistenRolesActivosAsync(List<int> rolesIds);
        Task CrearAsync(Usuario entidad, List<int> roles_ids);
        Task EliminarAsync(int id);
    }
}
