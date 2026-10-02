using mapa_conocimiento_api.Peticiones;

namespace mapa_conocimiento_api.Servicios
{
    public interface IProductoService
    {
        Task<IEnumerable<ProductoResponse>> ObtenerTodosAsync();
        Task<ProductoResponse?> ObtenerPorIdAsync(int id);
        Task<ProductoResponse> CrearAsync(ProductoCrear peticion);
        Task<bool> ActualizarAsync(int id, ProductoEditar peticion);
        Task<bool> EliminarAsync(int id);
    }
}