using mapa_conocimiento_api.Models;
using mapa_conocimiento_api.Peticiones;
using mapa_conocimiento_api.Repositorios;

namespace mapa_conocimiento_api.Servicios
{
    // Implementación de las reglas de negocio para aliado
    public class AliadoService : IAliadoService
    {
        private readonly IAliadoRepository _repositorio;

        // Inyección de dependencias del repositorio
        public AliadoService(IAliadoRepository repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<IEnumerable<aliado>> ObtenerTodosAsync()
        {
            return await _repositorio.ObtenerTodosAsync();
        }

        public async Task<aliado?> ObtenerPorIdAsync(int nit)
        {
            return await _repositorio.ObtenerPorIdAsync(nit);
        }

        public async Task<aliado> CrearAsync(AliadoCrear peticion)
        {
            // Mapeo manual del DTO de entrada hacia la entidad
            // CRÍTICO: Asignamos el nit porque la llave primaria no es autoincremental
            var nuevaEntidad = new aliado
            {
                nit = peticion.nit, // Asegúrate de que tu petición tenga el NIT
                razon_social = peticion.razon_social,
                nombre_contacto = peticion.nombre_contacto,
                correo = peticion.correo,
                telefono = peticion.telefono,
                ciudad = peticion.ciudad,
                activo = true // Por defecto activo al crear
            };

            await _repositorio.CrearAsync(nuevaEntidad);
            return nuevaEntidad;
        }

            // 1. Agregamos <bool> aquí
        public async Task<bool> ActualizarAsync(int nit, AliadoEditar peticion) 
        {
            var entidad = await _repositorio.ObtenerPorIdAsync(nit); 
    
            if (entidad == null)
            {
                return false; // 2. Retornamos falso si no existe
            }

            entidad.razon_social = peticion.razon_social;
            entidad.nombre_contacto = peticion.nombre_contacto;
            entidad.correo = peticion.correo;
            entidad.telefono = peticion.telefono;
            entidad.ciudad = peticion.ciudad;

            await _repositorio.ActualizarAsync(entidad);
    
            return true; // 3. Retornamos verdadero indicando éxito al final
        }

        public async Task<bool> EliminarAsync(int nit)
        {
            // Verificamos si existe antes de intentar hacer el borrado lógico
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