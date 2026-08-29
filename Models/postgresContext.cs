using Microsoft.EntityFrameworkCore;

namespace mapa_conocimiento_api.Models
{
    // El DbContext que mapea nuestras clases hacia la base de datos de PostgreSQL en Supabase
    public class postgresContext : DbContext
    {
        public postgresContext(DbContextOptions<postgresContext> options) : base(options)
        {
        }

        // DbSets para las 8 tablas maestras de la Fase 1
        public DbSet<area_conocimiento> area_conocimiento { get; set; }
        public DbSet<objetivo_desarrollo_sostenible> objetivo_desarrollo_sostenible { get; set; }
        public DbSet<area_aplicacion> area_aplicacion { get; set; }
        public DbSet<termino_clave> termino_clave { get; set; }
        public DbSet<linea_investigacion> linea_investigacion { get; set; }
        public DbSet<aliado> aliado { get; set; }
        public DbSet<tipo_producto> tipo_producto { get; set; }
        public DbSet<proyecto> proyecto { get; set; }

        // Configuración de reglas globales y modelado de datos
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Filtro Global de Borrado Lógico:
            // EF Core inyectará automáticamente un "WHERE activo = true" en todas las consultas (GET).
            modelBuilder.Entity<area_conocimiento>().HasQueryFilter(e => e.activo);
            modelBuilder.Entity<objetivo_desarrollo_sostenible>().HasQueryFilter(e => e.activo);
            modelBuilder.Entity<area_aplicacion>().HasQueryFilter(e => e.activo);
            modelBuilder.Entity<termino_clave>().HasQueryFilter(e => e.activo);
            modelBuilder.Entity<linea_investigacion>().HasQueryFilter(e => e.activo);
            modelBuilder.Entity<aliado>().HasQueryFilter(e => e.activo);
            modelBuilder.Entity<tipo_producto>().HasQueryFilter(e => e.activo);
            modelBuilder.Entity<proyecto>().HasQueryFilter(e => e.activo);
        }
    }
}