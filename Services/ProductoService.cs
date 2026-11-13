using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mapa_conocimiento_api.Models;
using mapa_conocimiento_api.Peticiones;
using mapa_conocimiento_api.Repositorios;

namespace mapa_conocimiento_api.Servicios
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _repositorio;
        private readonly IProyectoRepository _proyectoRepository;
        private readonly ITipoProductoRepository _tipoProductoRepository;

        public ProductoService(
            IProductoRepository repositorio, 
            IProyectoRepository proyectoRepository, 
            ITipoProductoRepository tipoProductoRepository)
        {
            _repositorio = repositorio;
            _proyectoRepository = proyectoRepository;
            _tipoProductoRepository = tipoProductoRepository;
        }

        private ProductoResponse MapearAResponse(Producto entidad)
        {
            return new ProductoResponse
            {
                id = entidad.id,
                nombre = entidad.nombre,
                categoria = entidad.categoria,
                fecha_entrega = entidad.fecha_entrega,
                activo = entidad.activo,
                proyecto = entidad.ProyectoRelacionado != null 
                    ? new ItemRelacionadoResponse 
                      { 
                          id = entidad.ProyectoRelacionado.id, 
                          nombre = entidad.ProyectoRelacionado.titulo 
                      } 
                    : null,
                tipo_producto = entidad.TipoProductoRelacionado != null 
                    ? new ItemRelacionadoResponse 
                      { 
                          id = entidad.TipoProductoRelacionado.id, 
                          nombre = entidad.TipoProductoRelacionado.nombre 
                      } 
                    : null
            };
        }

        public async Task<IEnumerable<ProductoResponse>> ObtenerTodosAsync()
        {
            var entidades = await _repositorio.ObtenerTodosAsync();
            return entidades.Select(MapearAResponse);
        }

        public async Task<ProductoResponse?> ObtenerPorIdAsync(int id)
        {
            var entidad = await _repositorio.ObtenerPorIdAsync(id);
            return entidad == null ? null : MapearAResponse(entidad);
        }

        public async Task<ProductoResponse> CrearAsync(ProductoCrear peticion)
        {
            var tipo = await _tipoProductoRepository.ObtenerPorIdAsync(peticion.tipo_producto_id);
            if (tipo == null) 
                throw new ArgumentException("El tipo de producto seleccionado no existe o está inactivo.");

            if (peticion.proyecto_id.HasValue)
            {
                var proyecto = await _proyectoRepository.ObtenerPorIdAsync(peticion.proyecto_id.Value);
                if (proyecto == null) 
                    throw new ArgumentException("El proyecto seleccionado no existe o está inactivo.");
            }

            var fechaEntregaUtc = DateTime.SpecifyKind(peticion.fecha_entrega.Date, DateTimeKind.Utc);

            var nuevaEntidad = new Producto
            {
                nombre = peticion.nombre,
                categoria = peticion.categoria,
                fecha_entrega = fechaEntregaUtc,
                proyecto_id = peticion.proyecto_id,
                tipo_producto_id = peticion.tipo_producto_id,
                activo = true
            };

            await _repositorio.CrearAsync(nuevaEntidad);

            var entidadCreada = await _repositorio.ObtenerPorIdAsync(nuevaEntidad.id);
            return MapearAResponse(entidadCreada!);
        }

        public async Task<bool> ActualizarAsync(int id, ProductoEditar peticion)
        {
            var entidadExistente = await _repositorio.ObtenerPorIdAsync(id);
            if (entidadExistente == null) return false;

            if (peticion.tipo_producto_id != entidadExistente.tipo_producto_id)
            {
                var tipo = await _tipoProductoRepository.ObtenerPorIdAsync(peticion.tipo_producto_id);
                if (tipo == null) 
                    throw new ArgumentException("El tipo de producto seleccionado no existe o está inactivo.");
            }

            if (peticion.proyecto_id != entidadExistente.proyecto_id)
            {
                if (peticion.proyecto_id.HasValue)
                {
                    var proyecto = await _proyectoRepository.ObtenerPorIdAsync(peticion.proyecto_id.Value);
                    if (proyecto == null) 
                        throw new ArgumentException("El proyecto seleccionado no existe o está inactivo.");
                }
            }

            entidadExistente.nombre = peticion.nombre;
            entidadExistente.categoria = peticion.categoria;
            entidadExistente.fecha_entrega = DateTime.SpecifyKind(peticion.fecha_entrega.Date, DateTimeKind.Utc);
            entidadExistente.proyecto_id = peticion.proyecto_id;
            entidadExistente.tipo_producto_id = peticion.tipo_producto_id;

            await _repositorio.ActualizarAsync(entidadExistente);
            return true;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var entidadExistente = await _repositorio.ObtenerPorIdAsync(id);
            if (entidadExistente == null) return false;

            await _repositorio.EliminarAsync(id);
            return true;
        }
    }
}
