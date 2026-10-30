using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using mapa_conocimiento_api.Models;
using mapa_conocimiento_api.Peticiones;
using mapa_conocimiento_api.Repositorios;

namespace mapa_conocimiento_api.Servicios
{
    public class ProyectoService : IProyectoService
    {
        private readonly IProyectoRepository _repositorio;

        public ProyectoService(IProyectoRepository repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<IEnumerable<Proyecto>> ObtenerTodosAsync() => await _repositorio.ObtenerTodosAsync();

        public async Task<Proyecto?> ObtenerPorIdAsync(int id) => await _repositorio.ObtenerPorIdAsync(id);

        public async Task<Proyecto> CrearAsync(ProyectoCrear peticion)
        {
            if (!peticion.fecha_inicio.HasValue)
                throw new ArgumentException("La fecha de inicio es obligatoria.");

            if (!peticion.presupuesto.HasValue)
                throw new ArgumentException("El presupuesto es obligatorio.");

            var fechaInicioUtc = DateTime.SpecifyKind(peticion.fecha_inicio.Value.Date, DateTimeKind.Utc);
            DateTime? fechaFinUtc = peticion.fecha_fin.HasValue
                ? DateTime.SpecifyKind(peticion.fecha_fin.Value.Date, DateTimeKind.Utc)
                : null;

            if (fechaFinUtc.HasValue && fechaFinUtc.Value < fechaInicioUtc)
            {
                throw new ArgumentException("La fecha de finalización no puede ser anterior a la fecha de inicio.");
            }

            var nuevaEntidad = new Proyecto
            { 
                titulo = peticion.titulo,
                resumen = peticion.resumen,
                presupuesto = peticion.presupuesto.Value,
                tipo_financiacion = peticion.tipo_financiacion,
                tipo_fondos = peticion.tipo_fondos,
                fecha_inicio = fechaInicioUtc,
                fecha_fin = fechaFinUtc,
                activo = true
            };

            await _repositorio.CrearAsync(nuevaEntidad);
            return nuevaEntidad;
        }

        public async Task<bool> ActualizarAsync(int id, ProyectoEditar peticion)
        {
            var entidadExistente = await _repositorio.ObtenerPorIdAsync(id);
            if (entidadExistente == null) return false;

            if (!peticion.fecha_inicio.HasValue)
                throw new ArgumentException("La fecha de inicio es obligatoria.");

            if (!peticion.presupuesto.HasValue)
                throw new ArgumentException("El presupuesto es obligatorio.");

            var fechaInicioUtc = DateTime.SpecifyKind(peticion.fecha_inicio.Value.Date, DateTimeKind.Utc);
            DateTime? fechaFinUtc = peticion.fecha_fin.HasValue
                ? DateTime.SpecifyKind(peticion.fecha_fin.Value.Date, DateTimeKind.Utc)
                : null;

            if (fechaFinUtc.HasValue && fechaFinUtc.Value < fechaInicioUtc)
            {
                throw new ArgumentException("La fecha de finalización no puede ser anterior a la fecha de inicio.");
            }

            entidadExistente.titulo = peticion.titulo;
            entidadExistente.resumen = peticion.resumen;
            entidadExistente.presupuesto = peticion.presupuesto.Value;
            entidadExistente.tipo_financiacion = peticion.tipo_financiacion;
            entidadExistente.tipo_fondos = peticion.tipo_fondos;
            entidadExistente.fecha_inicio = fechaInicioUtc;
            entidadExistente.fecha_fin = fechaFinUtc;

            await _repositorio.ActualizarAsync(entidadExistente);
            return true;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            if (await _repositorio.ObtenerPorIdAsync(id) == null) return false;
            await _repositorio.EliminarAsync(id);
            return true;
        }
    }
}
