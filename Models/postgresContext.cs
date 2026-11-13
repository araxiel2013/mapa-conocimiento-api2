using Microsoft.EntityFrameworkCore;

namespace mapa_conocimiento_api.Models
{
    public class postgresContext : DbContext
    {
        public postgresContext(DbContextOptions<postgresContext> options) : base(options) { }

        // ====================================================================
        // FASE 1: Tablas Maestras
        // ====================================================================
        public DbSet<AreaConocimiento> area_conocimiento { get; set; }
        public DbSet<ObjetivoDesarrolloSostenible> objetivo_desarrollo_sostenible { get; set; }
        public DbSet<AreaAplicacion> area_aplicacion { get; set; }
        public DbSet<TerminoClave> termino_clave { get; set; }
        public DbSet<LineaInvestigacion> linea_investigacion { get; set; }
        public DbSet<Aliado> aliado { get; set; }
        public DbSet<Proyecto> proyecto { get; set; }
        public DbSet<TipoProducto> tipo_producto { get; set; }

        // ====================================================================
        // FASE 2: Entidades Fuertes y Tablas Puente
        // ====================================================================
        public DbSet<Docente> docente { get; set; }
        public DbSet<Producto> producto { get; set; }
        
        public DbSet<AliadoProyecto> aliado_proyecto { get; set; }
        public DbSet<Desarrolla> desarrolla { get; set; }
        public DbSet<PalabrasClave> palabras_clave { get; set; }
        public DbSet<AcProyecto> ac_proyecto { get; set; }
        public DbSet<ProyectoLinea> proyecto_linea { get; set; }
        public DbSet<OdsProyecto> ods_proyecto { get; set; }
        public DbSet<AaProyecto> aa_proyecto { get; set; }
        public DbSet<DocenteProducto> docente_producto { get; set; }

        // ====================================================================
        // FASE 3: Seguridad y Usuarios
        // ====================================================================
        public DbSet<Usuario> usuario { get; set; }
        public DbSet<Rol> rol { get; set; }
        public DbSet<RolUsuario> rol_usuario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ====================================================================
            // CONFIGURACIÓN DE LLAVES PRIMARIAS COMPUESTAS (Fase 2)
            // ====================================================================
            modelBuilder.Entity<AliadoProyecto>().HasKey(x => new { x.aliado, x.proyecto });
            modelBuilder.Entity<Desarrolla>().HasKey(x => new { x.docente, x.proyecto });
            modelBuilder.Entity<PalabrasClave>().HasKey(x => new { x.proyecto, x.termino_clave });
            modelBuilder.Entity<AcProyecto>().HasKey(x => new { x.proyecto, x.area_conocimiento });
            modelBuilder.Entity<ProyectoLinea>().HasKey(x => new { x.proyecto, x.linea_investigacion });
            modelBuilder.Entity<OdsProyecto>().HasKey(x => new { x.proyecto, x.ods });
            modelBuilder.Entity<AaProyecto>().HasKey(x => new { x.proyecto, x.area_aplicacion });
            modelBuilder.Entity<DocenteProducto>().HasKey(x => new { x.docente, x.producto });

            // ====================================================================
            // FILTROS GLOBALES DE BORRADO LÓGICO (activo == true)
            // ====================================================================
            // Fase 1
            modelBuilder.Entity<AreaConocimiento>().HasQueryFilter(x => x.activo);
            modelBuilder.Entity<ObjetivoDesarrolloSostenible>().HasQueryFilter(x => x.activo);
            modelBuilder.Entity<AreaAplicacion>().HasQueryFilter(x => x.activo);
            modelBuilder.Entity<TerminoClave>().HasQueryFilter(x => x.activo);
            modelBuilder.Entity<LineaInvestigacion>().HasQueryFilter(x => x.activo);
            modelBuilder.Entity<Aliado>().HasQueryFilter(x => x.activo);
            modelBuilder.Entity<Proyecto>().HasQueryFilter(x => x.activo);
            modelBuilder.Entity<TipoProducto>().HasQueryFilter(x => x.activo);
            
            // Fase 2
            modelBuilder.Entity<Docente>().HasQueryFilter(x => x.activo);
            modelBuilder.Entity<Producto>().HasQueryFilter(x => x.activo);
            modelBuilder.Entity<AliadoProyecto>().HasQueryFilter(x => x.activo);
            modelBuilder.Entity<Desarrolla>().HasQueryFilter(x => x.activo);
            modelBuilder.Entity<PalabrasClave>().HasQueryFilter(x => x.activo);
            modelBuilder.Entity<AcProyecto>().HasQueryFilter(x => x.activo);
            modelBuilder.Entity<ProyectoLinea>().HasQueryFilter(x => x.activo);
            modelBuilder.Entity<OdsProyecto>().HasQueryFilter(x => x.activo);
            modelBuilder.Entity<AaProyecto>().HasQueryFilter(x => x.activo);
            modelBuilder.Entity<DocenteProducto>().HasQueryFilter(x => x.activo);

            // ====================================================================
            // FASE 3: Seguridad y Usuarios
            // ====================================================================
            // Llaves Primarias Compuestas
            modelBuilder.Entity<RolUsuario>().HasKey(ru => new { ru.usuario_id, ru.rol_id });

            // Restricciones UNIQUE
            modelBuilder.Entity<Usuario>().HasIndex(u => u.username).IsUnique();
            modelBuilder.Entity<Usuario>().HasIndex(u => u.email).IsUnique();
            modelBuilder.Entity<Rol>().HasIndex(r => r.nombre).IsUnique();

            // Filtros Globales de Borrado Lógico (activo == true)
            modelBuilder.Entity<Usuario>().HasQueryFilter(x => x.activo);
            modelBuilder.Entity<Rol>().HasQueryFilter(x => x.activo);
            modelBuilder.Entity<RolUsuario>().HasQueryFilter(x => x.activo);
        }
    }
}
