# Modelo de datos — Fase 2: Tablas con claves foráneas (Relaciones)

> **Fase 2** · La base de datos ya existe en Supabase desde la Fase 0. Este documento detalla las 10 tablas relacionales que se implementarán en el código durante esta fase y cómo se mapean sus dependencias.

---

## 1. Tablas a implementar en la Fase 2

En esta fase, la API y React interactuarán con las tablas que dependen de las 8 tablas maestras (Fase 1) o de otras tablas relacionales.

### Tablas Principales con Dependencias
Estas tablas representan entidades fuertes que dependen de catálogos para clasificar su información.

| Tabla | Llave Primaria (PK) | Llaves Foráneas (FK) |
|---|---|---|
| `docente` | `cedula` (INT) | `linea_investigacion_principal` -> `linea_investigacion(id)` |
| `producto` | `id` (INT) | `proyecto` -> `proyecto(id)`<br>`tipo_producto` -> `tipo_producto(id)` |

### Tablas Puente (Relaciones Muchos a Muchos)
Estas tablas sirven para vincular dos entidades. En Entity Framework Core, requieren configuración de llaves primarias compuestas.

| Tabla | Llave Primaria Compuesta (PK) | Dependencias (FK) |
|---|---|---|
| `aliado_proyecto` | `(aliado, proyecto)` | `aliado(nit)`, `proyecto(id)` |
| `desarrolla` | `(docente, proyecto)` | `docente(cedula)`, `proyecto(id)` *(Nota: Incluye campos extra `rol` y `descripcion`)* |
| `palabras_clave` | `(proyecto, termino_clave)` | `proyecto(id)`, `termino_clave(termino)` |
| `ac_proyecto` | `(proyecto, area_conocimiento)` | `proyecto(id)`, `area_conocimiento(id)` |
| `proyecto_linea` | `(proyecto, linea_investigacion)` | `proyecto(id)`, `linea_investigacion(id)` |
| `ods_proyecto` | `(proyecto, ods)` | `proyecto(id)`, `objetivo_desarrollo_sostenible(id)` |
| `aa_proyecto` | `(proyecto, area_aplicacion)` | `proyecto(id)`, `area_aplicacion(id)` |
| `docente_producto` | `(docente, producto)` | `docente(cedula)`, `producto(id)` |

## 2. Modelado en Entity Framework Core (C#)

Para que EF Core entienda estas relaciones al conectarse a Supabase, los modelos en la carpeta `Models/` deben incluir propiedades de navegación.

### Ejemplo de Entidad Fuerte (`docente.cs`)
```csharp
public class docente
{
    public int cedula { get; set; }
    public required string nombres { get; set; }
    public required string apellidos { get; set; }
    // ... otros campos (genero, cargo, correo, etc.)
    
    public int? linea_investigacion_principal { get; set; }
    public bool activo { get; set; }

    // Propiedad de navegación
    public virtual linea_investigacion? LineaInvestigacionRelacionada { get; set; }
}
```

### Ejemplo de Tabla Puente (`ac_proyecto.cs`)
```csharp
public class ac_proyecto
{
    public int proyecto { get; set; }
    public int area_conocimiento { get; set; }
    public bool activo { get; set; }

    // Propiedades de navegación
    public virtual proyecto ProyectoRelacionado { get; set; }
    public virtual area_conocimiento AreaConocimientoRelacionada { get; set; }
}
```

### Configuración en `postgresContext.cs` (Fluent API)
Las llaves primarias compuestas de las tablas puente no se pueden definir solo con decoradores, requieren código en el método `OnModelCreating`:

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    // Configuración de PK compuesta
    modelBuilder.Entity<ac_proyecto>()
        .HasKey(ac => new { ac.proyecto, ac.area_conocimiento });

    // Configuración de borrado lógico global para la Fase 2
    modelBuilder.Entity<docente>().HasQueryFilter(x => x.activo == true);
    modelBuilder.Entity<producto>().HasQueryFilter(x => x.activo == true);
    modelBuilder.Entity<ac_proyecto>().HasQueryFilter(x => x.activo == true);
    // ... aplicar a las 10 tablas
}
```

## 3. Reglas estrictas de esta fase

- **Integridad:** Antes de guardar una tabla puente (ej. `ac_proyecto`), la API debe validar que tanto el proyecto como el área de conocimiento existan y estén activos (`activo == true`).
- **Borrado Lógico en Tablas Puente:** Al eliminar la relación de un docente con un producto (`docente_producto`), el registro no se borra con un `DELETE` físico. Se debe hacer un UPDATE estableciendo `activo = false`.
- **Estructura del Proyecto:** Solo se crearán Controladores, Servicios y Repositorios para estas 10 tablas. Las tablas de gestión de usuarios (`usuario`, `rol`, `rol_usuario`) pertenecen a la Fase 3.