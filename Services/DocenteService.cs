using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mapa_conocimiento_api.Models;
using mapa_conocimiento_api.Peticiones;
using mapa_conocimiento_api.Repositorios;

namespace mapa_conocimiento_api.Servicios
{
    public class DocenteService : IDocenteService
    {
        private readonly IDocenteRepository _repositorio;
        private readonly ILineaInvestigacionRepository _lineaInvestigacionRepository;

        public DocenteService(IDocenteRepository repositorio, ILineaInvestigacionRepository lineaInvestigacionRepository)
        {
            _repositorio = repositorio;
            _lineaInvestigacionRepository = lineaInvestigacionRepository;
        }

        private DocenteResponse MapearAResponse(docente entidad)
        {
            return new DocenteResponse
            {
                cedula = entidad.cedula,
                nombres = entidad.nombres,
                apellidos = entidad.apellidos,
                genero = entidad.genero,
                cargo = entidad.cargo,
                fecha_nacimiento = entidad.fecha_nacimiento,
                correo = entidad.correo,
                telefono = entidad.telefono,
                url_cvlac = entidad.url_cvlac,
                fecha_actualizacion = entidad.fecha_actualizacion,
                escalafon = entidad.escalafon,
                perfil = entidad.perfil,
                cat_minciencia = entidad.cat_minciencia,
                conv_minciencia = entidad.conv_minciencia,
                nacionalidad = entidad.nacionalidad,
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
            // Validar cédula no duplicada
            var docenteExistente = await _repositorio.ObtenerPorIdAsync(peticion.cedula);
            if (docenteExistente != null)
                throw new ArgumentException($"Ya existe un docente registrado con la cédula {peticion.cedula}.");

            // VALIDACIÓN FASE 2: ¿La llave foránea existe y está activa?
            if (peticion.linea_investigacion_principal.HasValue)
            {
                var linea = await _lineaInvestigacionRepository.ObtenerPorIdAsync(peticion.linea_investigacion_principal.Value);
                if (linea == null) 
                    throw new ArgumentException("La línea de investigación seleccionada no existe o está inactiva.");
            }

            if (string.IsNullOrWhiteSpace(peticion.nacionalidad))
                throw new ArgumentException("La nacionalidad es obligatoria.");

            var nacionalidadValida = peticion.nacionalidad.Trim();

            var nuevaEntidad = new docente
            {
                cedula = peticion.cedula,
                nombres = peticion.nombres,
                apellidos = peticion.apellidos,
                genero = peticion.genero,
                cargo = peticion.cargo,
                fecha_nacimiento = DateTime.SpecifyKind(peticion.fecha_nacimiento.Date, DateTimeKind.Utc),
                correo = peticion.correo,
                telefono = peticion.telefono,
                url_cvlac = peticion.url_cvlac,
                fecha_actualizacion = peticion.fecha_actualizacion.HasValue
                    ? DateTime.SpecifyKind(peticion.fecha_actualizacion.Value.Date, DateTimeKind.Utc)
                    : DateTime.UtcNow.Date,
                escalafon = peticion.escalafon,
                perfil = peticion.perfil,
                cat_minciencia = peticion.cat_minciencia,
                conv_minciencia = peticion.conv_minciencia,
                nacionalidad = nacionalidadValida,
                linea_investigacion_principal = peticion.linea_investigacion_principal,
                activo = true
            };

            await _repositorio.CrearAsync(nuevaEntidad);

            var entidadCreada = await _repositorio.ObtenerPorIdAsync(nuevaEntidad.cedula);
            return MapearAResponse(entidadCreada!);
        }

        public async Task<bool> ActualizarAsync(int cedula, DocenteEditar peticion)
        {
            var entidadExistente = await _repositorio.ObtenerPorIdAsync(cedula);
            if (entidadExistente == null) return false;

            if (peticion.linea_investigacion_principal.HasValue && 
                peticion.linea_investigacion_principal != entidadExistente.linea_investigacion_principal)
            {
                var linea = await _lineaInvestigacionRepository.ObtenerPorIdAsync(peticion.linea_investigacion_principal.Value);
                if (linea == null) 
                    throw new ArgumentException("La línea de investigación seleccionada no existe o está inactiva.");
            }

            if (string.IsNullOrWhiteSpace(peticion.nacionalidad))
                throw new ArgumentException("La nacionalidad es obligatoria.");

            var nacionalidadValida = peticion.nacionalidad.Trim();

            entidadExistente.nombres = peticion.nombres;
            entidadExistente.apellidos = peticion.apellidos;
            entidadExistente.genero = peticion.genero;
            entidadExistente.cargo = peticion.cargo;
            entidadExistente.fecha_nacimiento = DateTime.SpecifyKind(peticion.fecha_nacimiento.Date, DateTimeKind.Utc);
            entidadExistente.correo = peticion.correo;
            entidadExistente.telefono = peticion.telefono;
            entidadExistente.url_cvlac = peticion.url_cvlac;
            entidadExistente.fecha_actualizacion = peticion.fecha_actualizacion.HasValue
                ? DateTime.SpecifyKind(peticion.fecha_actualizacion.Value.Date, DateTimeKind.Utc)
                : DateTime.UtcNow.Date;
            entidadExistente.escalafon = peticion.escalafon;
            entidadExistente.perfil = peticion.perfil;
            entidadExistente.cat_minciencia = peticion.cat_minciencia;
            entidadExistente.conv_minciencia = peticion.conv_minciencia;
            entidadExistente.nacionalidad = nacionalidadValida;
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