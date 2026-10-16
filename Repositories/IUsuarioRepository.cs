using mapa_conocimiento_api.Models;

namespace mapa_conocimiento_api.Repositorios
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<usuario>> ObtenerTodosAsync();
        Task<usuario?> ObtenerPorIdAsync(int id);
        Task<bool> ExisteUsernameOEmailAsync(string username, string email);
        Task CrearAsync(usuario entidad, List<int> roles_ids);
        Task EliminarAsync(int id);
    }
}