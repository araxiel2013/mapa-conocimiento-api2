using Microsoft.EntityFrameworkCore;
using mapa_conocimiento_api.Models;
using System.Threading.Tasks;

namespace mapa_conocimiento_api.Repositorios
{
    public class DocenteProductoRepository : IDocenteProductoRepository
    {
        private readonly postgresContext _context;
        public DocenteProductoRepository(postgresContext context) => _context = context;

        public async Task<docente_producto?> ObtenerPorIdsAsync(int docente_cedula, int producto_id) =>
            await _context.docente_producto.FirstOrDefaultAsync(d => d.docente == docente_cedula && d.producto == producto_id);

        public async Task CrearAsync(docente_producto entidad)
        {
            entidad.activo = true;
            await _context.docente_producto.AddAsync(entidad);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int docente_cedula, int producto_id)
        {
            var entidad = await ObtenerPorIdsAsync(docente_cedula, producto_id);
            if (entidad != null) { entidad.activo = false; await _context.SaveChangesAsync(); }
        }
    }
}
