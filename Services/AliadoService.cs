using mapa_conocimiento_api.Models;
using mapa_conocimiento_api.Peticiones;
using mapa_conocimiento_api.Repositorios;

namespace mapa_conocimiento_api.Servicios
{
    public class AliadoService : IAliadoService
    {
        private readonly IAliadoRepository _repositorio;

        public AliadoService(IAliadoRepository repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<IEnumerable<Aliado>> ObtenerTodosAsync()
        {
            return await _repositorio.ObtenerTodosAsync();
        }

        public async Task<Aliado?> ObtenerPorIdAsync(int nit)
        {
            return await _repositorio.ObtenerPorIdAsync(nit);
        }

        public async Task<Aliado> CrearAsync(AliadoCrear peticion)
        {
            var nuevaEntidad = new Aliado
            {
                nit = peticion.nit,
                razon_social = peticion.razon_social,
                nombre_contacto = peticion.nombre_contacto,
                correo = peticion.correo,
                telefono = peticion.telefono,
                ciudad = peticion.ciudad,
                activo = true
            };

            await _repositorio.CrearAsync(nuevaEntidad);
            return nuevaEntidad;
        }

        public async Task<bool> ActualizarAsync(int nit, AliadoEditar peticion) 
        {
            var entidad = await _repositorio.ObtenerPorIdAsync(nit); 
            if (entidad == null)
            {
                return false;
            }

            entidad.razon_social = peticion.razon_social;
            entidad.nombre_contacto = peticion.nombre_contacto;
            entidad.correo = peticion.correo;
            entidad.telefono = peticion.telefono;
            entidad.ciudad = peticion.ciudad;

            await _repositorio.ActualizarAsync(entidad);
            return true;
        }

        public async Task<bool> EliminarAsync(int nit)
        {
            var entidadExistente = await _repositorio.ObtenerPorIdAsync(nit);
            if (entidadExistente == null) 
            {
                return false; 
            }

            await _repositorio.EliminarAsync(nit);
            return true;
        }
    }
}
