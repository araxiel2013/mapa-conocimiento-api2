using mapa_conocimiento_api.Models;
using mapa_conocimiento_api.Peticiones;
using mapa_conocimiento_api.Repositorios;

namespace mapa_conocimiento_api.Servicios
{
    public class DocenteService : IDocenteService
    {
        private readonly IDocenteRepository _repositorio;
        // Inyectamos el repo de Fase 1 para validar la Llave Foránea
        private readonly ILineaInvestigacionRepository _lineaInvestigacionRepository;

        public DocenteService(IDocenteRepository repositorio, ILineaInvestigacionRepository lineaInvestigacionRepository)
        {
            _repositorio = repositorio;
            _lineaInvestigacionRepository = lineaInvestigacionRepository;
        }

        // Método auxiliar privado para aplanar la entidad hacia el DTO de respuesta
        private DocenteResponse MapearAResponse(docente entidad)
        {
            return new DocenteResponse
            {
                cedula = entidad.cedula,
                nombres = entidad.nombres,
                apellidos = entidad.apellidos,
                genero = entidad.genero,
                cargo = entidad.cargo,
                correo = entidad.correo,
                activo = entidad.activo,
                linea_investigacion = entidad.LineaInvestigacionRelacionada != null 
                    ? new ItemRelacionadoResponse 
                      { 
                          id = entidad.LineaInvestigacionRelacionada.id, 
                          nombre = entidad.LineaInvestigacionRelacionada.nombre 
                      } 
                    : null
            };
        }

        public async Task<IEnumerable<DocenteResponse>> ObtenerTodosAsync()
        {
            var entidades = await _repositorio.ObtenerTodosAsync();
            return entidades.Select(MapearAResponse);
        }

        public async Task<DocenteResponse?> ObtenerPorIdAsync(int cedula)
        {
            var entidad = await _repositorio.ObtenerPorIdAsync(cedula);
            return entidad == null ? null : MapearAResponse(entidad);
        }

        public async Task<DocenteResponse> CrearAsync(DocenteCrear peticion)
        {
            // VALIDACIÓN FASE 2: ¿La llave foránea existe y está activa?
            if (peticion.linea_investigacion_principal.HasValue)
            {
                var linea = await _lineaInvestigacionRepository.ObtenerPorIdAsync(peticion.linea_investigacion_principal.Value);
                if (linea == null) 
                    throw new ArgumentException("La línea de investigación seleccionada no existe o está inactiva.");
            }

            var nuevaEntidad = new docente
            {
                cedula = peticion.cedula,
                nombres = peticion.nombres,
                apellidos = peticion.apellidos,
                genero = peticion.genero,
                cargo = peticion.cargo,
                correo = peticion.correo,
                linea_investigacion_principal = peticion.linea_investigacion_principal
            };

            await _repositorio.CrearAsync(nuevaEntidad);

            // Consultamos de nuevo para que EF Core nos traiga la propiedad de navegación llena
            var entidadCreada = await _repositorio.ObtenerPorIdAsync(nuevaEntidad.cedula);
            return MapearAResponse(entidadCreada!);
        }

        public async Task<bool> ActualizarAsync(int cedula, DocenteEditar peticion)
        {
            var entidadExistente = await _repositorio.ObtenerPorIdAsync(cedula);
            if (entidadExistente == null) return false;

            // VALIDACIÓN FASE 2: Validamos si cambiaron la llave foránea a una nueva
            if (peticion.linea_investigacion_principal.HasValue && 
                peticion.linea_investigacion_principal != entidadExistente.linea_investigacion_principal)
            {
                var linea = await _lineaInvestigacionRepository.ObtenerPorIdAsync(peticion.linea_investigacion_principal.Value);
                if (linea == null) 
                    throw new ArgumentException("La línea de investigación seleccionada no existe o está inactiva.");
            }

            entidadExistente.nombres = peticion.nombres;
            entidadExistente.apellidos = peticion.apellidos;
            entidadExistente.genero = peticion.genero;
            entidadExistente.cargo = peticion.cargo;
            entidadExistente.correo = peticion.correo;
            entidadExistente.linea_investigacion_principal = peticion.linea_investigacion_principal;

            await _repositorio.ActualizarAsync(entidadExistente);
            return true;
        }

        public async Task<bool> EliminarAsync(int cedula)
        {
            var entidadExistente = await _repositorio.ObtenerPorIdAsync(cedula);
            if (entidadExistente == null) return false;

            await _repositorio.EliminarAsync(cedula);
            return true;
        }
    }
}