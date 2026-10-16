using mapa_conocimiento_api.Models;

namespace mapa_conocimiento_api.Repositorios
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<usuario>> ObtenerTodosAsync();
        Task<usuario?> ObtenerPorIdAsync(int id);
        Task<usuario?> ObtenerPorUsernameConRolesAsync(string username);
        Task<bool> ExisteUsernameOEmailAsync(string username, string email);
        Task<bool> ExisteUsernameOEmailIgnorandoFiltrosAsync(string username, string email);
        Task<bool> ExistenRolesActivosAsync(List<int> rolesIds);
        Task CrearAsync(usuario entidad, List<int> roles_ids);
        Task EliminarAsync(int id);
    }
}