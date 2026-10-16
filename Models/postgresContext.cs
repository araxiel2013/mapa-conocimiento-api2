using Microsoft.EntityFrameworkCore;

namespace mapa_conocimiento_api.Models
{
    public class postgresContext : DbContext
    {
        public postgresContext(DbContextOptions<postgresContext> options) : base(options) { }

        // ====================================================================
        // FASE 1: Tablas Maestras
        // ====================================================================
        public DbSet<area_conocimiento> area_conocimiento { get; set; }
        public DbSet<objetivo_desarrollo_sostenible> objetivo_desarrollo_sostenible { get; set; }
        public DbSet<area_aplicacion> area_aplicacion { get; set; }
        public DbSet<termino_clave> termino_clave { get; set; }
        public DbSet<linea_investigacion> linea_investigacion { get; set; }
        public DbSet<aliado> aliado { get; set; }
        public DbSet<proyecto> proyecto { get; set; }
        public DbSet<tipo_producto> tipo_producto { get; set; }

        // ====================================================================
        // FASE 2: Entidades Fuertes y Tablas Puente
        // ====================================================================
        public DbSet<docente> docente { get; set; }
        public DbSet<producto> producto { get; set; }
        
        public DbSet<aliado_proyecto> aliado_proyecto { get; set; }
        public DbSet<desarrolla> desarrolla { get; set; }
        public DbSet<palabras_clave> palabras_clave { get; set; }
        public DbSet<ac_proyecto> ac_proyecto { get; set; }
        public DbSet<proyecto_linea> proyecto_linea { get; set; }
        public DbSet<ods_proyecto> ods_proyecto { get; set; }
        public DbSet<aa_proyecto> aa_proyecto { get; set; }
        public DbSet<docente_producto> docente_producto { get; set; }

        // ====================================================================
        // FASE 3: Seguridad y Usuarios
        // ====================================================================
        public DbSet<usuario> usuario { get; set; }
        public DbSet<rol> rol { get; set; }
        public DbSet<rol_usuario> rol_usuario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ====================================================================
            // CONFIGURACIÓN DE LLAVES PRIMARIAS COMPUESTAS (Fase 2)
            // ====================================================================
            modelBuilder.Entity<aliado_proyecto>().HasKey(x => new { x.aliado, x.proyecto });
            modelBuilder.Entity<desarrolla>().HasKey(x => new { x.docente, x.proyecto });
            modelBuilder.Entity<palabras_clave>().HasKey(x => new { x.proyecto, x.termino_clave });
            modelBuilder.Entity<ac_proyecto>().HasKey(x => new { x.proyecto, x.area_conocimiento });
            modelBuilder.Entity<proyecto_linea>().HasKey(x => new { x.proyecto, x.linea_investigacion });
            modelBuilder.Entity<ods_proyecto>().HasKey(x => new { x.proyecto, x.ods });
            modelBuilder.Entity<aa_proyecto>().HasKey(x => new { x.proyecto, x.area_aplicacion });
            modelBuilder.Entity<docente_producto>().HasKey(x => new { x.docente, x.producto });

            // ====================================================================
            // FILTROS GLOBALES DE BORRADO LÓGICO (activo == true)
            // ====================================================================
            // Fase 1
            modelBuilder.Entity<area_conocimiento>().HasQueryFilter(x => x.activo);
            modelBuilder.Entity<objetivo_desarrollo_sostenible>().HasQueryFilter(x => x.activo);
            modelBuilder.Entity<area_aplicacion>().HasQueryFilter(x => x.activo);
            modelBuilder.Entity<termino_clave>().HasQueryFilter(x => x.activo);
            modelBuilder.Entity<linea_investigacion>().HasQueryFilter(x => x.activo);
            modelBuilder.Entity<aliado>().HasQueryFilter(x => x.activo);
            modelBuilder.Entity<proyecto>().HasQueryFilter(x => x.activo);
            modelBuilder.Entity<tipo_producto>().HasQueryFilter(x => x.activo);
            
            // Fase 2
            modelBuilder.Entity<docente>().HasQueryFilter(x => x.activo);
            modelBuilder.Entity<producto>().HasQueryFilter(x => x.activo);
            modelBuilder.Entity<aliado_proyecto>().HasQueryFilter(x => x.activo);
            modelBuilder.Entity<desarrolla>().HasQueryFilter(x => x.activo);
            modelBuilder.Entity<palabras_clave>().HasQueryFilter(x => x.activo);
            modelBuilder.Entity<ac_proyecto>().HasQueryFilter(x => x.activo);
            modelBuilder.Entity<proyecto_linea>().HasQueryFilter(x => x.activo);
            modelBuilder.Entity<ods_proyecto>().HasQueryFilter(x => x.activo);
            modelBuilder.Entity<aa_proyecto>().HasQueryFilter(x => x.activo);
            modelBuilder.Entity<docente_producto>().HasQueryFilter(x => x.activo);

            // ====================================================================
            // FASE 3: Seguridad y Usuarios
            // ====================================================================
            // Llaves Primarias Compuestas
            modelBuilder.Entity<rol_usuario>().HasKey(ru => new { ru.usuario_id, ru.rol_id });

            // Restricciones UNIQUE
            modelBuilder.Entity<usuario>().HasIndex(u => u.username).IsUnique();
            modelBuilder.Entity<usuario>().HasIndex(u => u.email).IsUnique();
            modelBuilder.Entity<rol>().HasIndex(r => r.nombre).IsUnique();

            // Filtros Globales de Borrado Lógico (activo == true)
            modelBuilder.Entity<usuario>().HasQueryFilter(x => x.activo);
            modelBuilder.Entity<rol>().HasQueryFilter(x => x.activo);
            modelBuilder.Entity<rol_usuario>().HasQueryFilter(x => x.activo);
        }
    }
}