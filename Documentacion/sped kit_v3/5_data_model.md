# Modelo de datos — Fase 3: Gestión de Usuarios y Roles (RBAC)

> **Fase 3** · La base de datos se mantiene en Supabase. Este documento detalla las 3 tablas del módulo de seguridad y cómo se modelan en C# para soportar contraseñas encriptadas y control de acceso basado en roles (RBAC).

---

## 1. Tablas a implementar en la Fase 3

El módulo de gestión de usuarios consta de 3 tablas que ya fueron creadas en Supabase mediante el script de la Fase 0.

| Tabla | Llave Primaria (PK) | Descripción |
|---|---|---|
| `rol` | `id` (SERIAL) | Catálogo de roles del sistema. Ej. 'Administrador', 'Consulta', 'Editor'. |
| `usuario` | `id` (SERIAL) | Almacena las credenciales de acceso. El campo `password` guarda el hash BCrypt, no el texto plano. |
| `rol_usuario` | `(usuario_id, rol_id)` | Tabla puente (Muchos a Muchos) que vincula qué roles tiene cada usuario. |

## 2. Modelado en Entity Framework Core (C#)

Las propiedades de navegación son cruciales aquí para que, al cargar un usuario durante el Login, el sistema pueda traer sus roles asociados con `.Include()`.

### Clase `usuario` (`Models/usuario.cs`)
```csharp
public class usuario
{
    public int id { get; set; }
    public required string username { get; set; }
    
    // OJO: Aquí se guarda el Hash de BCrypt (aprox 60 caracteres)
    public required string password { get; set; } 
    
    public required string email { get; set; }
    public string? nombre_completo { get; set; }
    public bool activo { get; set; }
    public DateTime fecha_creacion { get; set; }
    public DateTime fecha_actualizacion { get; set; }

    // Propiedad de navegación (Uno a Muchos hacia la tabla puente)
    public virtual ICollection<rol_usuario> RolesUsuario { get; set; } = new List<rol_usuario>();
}
```

### Clase `rol` (`Models/rol.cs`)
```csharp
public class rol
{
    public int id { get; set; }
    public required string nombre { get; set; }
    public string? descripcion { get; set; }
    public bool activo { get; set; }
    public DateTime fecha_creacion { get; set; }

    // Propiedad de navegación inversa
    public virtual ICollection<rol_usuario> RolesUsuario { get; set; } = new List<rol_usuario>();
}
```

### Tabla Puente `rol_usuario` (`Models/rol_usuario.cs`)
```csharp
public class rol_usuario
{
    public int usuario_id { get; set; }
    public int rol_id { get; set; }
    public bool activo { get; set; }

    public virtual usuario UsuarioRelacionado { get; set; }
    public virtual rol RolRelacionado { get; set; }
}
```

### Configuración en `postgresContext.cs` (Fluent API)
Se debe configurar la llave compuesta de la tabla puente y asegurar los campos UNIQUE que definió el script SQL.

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    // ... configuraciones de la Fase 1 y 2

    // PK Compuesta para rol_usuario
    modelBuilder.Entity<rol_usuario>()
        .HasKey(ru => new { ru.usuario_id, ru.rol_id });

    // Restricciones UNIQUE (Para evitar usernames o emails duplicados a nivel de BD)
    modelBuilder.Entity<usuario>()
        .HasIndex(u => u.username)
        .IsUnique();
        
    modelBuilder.Entity<usuario>()
        .HasIndex(u => u.email)
        .IsUnique();
        
    modelBuilder.Entity<rol>()
        .HasIndex(r => r.nombre)
        .IsUnique();

    // Filtros Globales de Borrado Lógico
    modelBuilder.Entity<usuario>().HasQueryFilter(x => x.activo == true);
    modelBuilder.Entity<rol>().HasQueryFilter(x => x.activo == true);
    modelBuilder.Entity<rol_usuario>().HasQueryFilter(x => x.activo == true);
}
```

## 3. Consideraciones de Negocio sobre los Datos

1. **Hashes BCrypt:** El campo `password` de la tabla `usuario` fue definido en el script SQL como `VARCHAR(255)`. Esto es correcto porque un hash BCrypt estándar ocupa 60 caracteres. **NUNCA** se debe intentar actualizar este campo con un texto plano desde un UPDATE manual en Supabase si se espera que el Login funcione.
2. **Actualización de Fechas:** La tabla `usuario` tiene un campo `fecha_actualizacion`. La capa de Servicios (C#) debe actualizar este campo con `DateTime.UtcNow` (o la hora local si está configurado así) cada vez que el perfil o los roles del usuario sean modificados por el Administrador.
3. **Desactivación de Roles:** Si un Administrador hace un DELETE (borrado lógico) de un rol específico (ej. desactiva temporalmente el rol "Editor"), cualquier usuario que tuviera ese rol perderá esos permisos en su próximo Login, ya que el Query Filter global de EF Core ignorará los roles inactivos al hacer el `.Include()`.