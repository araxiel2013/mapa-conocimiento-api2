# Modelo de datos — Fase 1: La BD completa (dada) y las tablas maestras

> **Fase 1** · La base de datos NO se diseña por partes: **viene dada y se crea completa desde el inicio** (`4_research.md` D4). Este documento describe lo que hay en Supabase y lo único que la Fase 1 puede tocar.

---

## 1. La base de datos `mapa_conocimiento` (dada, completa)

El script SQL provisto crea la base de datos completa en **Supabase** (PostgreSQL). Se debe ejecutar de forma manual en el panel de SQL de Supabase antes de iniciar el desarrollo.

**21 tablas en total**, divididas lógicamente así:

```text
TABLAS MAESTRAS (FASE 1)             TABLAS CON FK (FASE 2)             GESTIÓN USUARIOS (FASE 3)
area_conocimiento                    docente                            usuario
objetivo_desarrollo_sostenible       producto                           rol
area_aplicacion                      aliado_proyecto                    rol_usuario
termino_clave                        desarrolla
linea_investigacion                  palabras_clave
aliado                               ac_proyecto
tipo_producto                        proyecto_linea
proyecto                             ods_proyecto
                                     aa_proyecto
                                     docente_producto
```

Todas las tablas incluyen el campo `activo BOOLEAN DEFAULT TRUE` para cumplir con el **borrado lógico** exigido en el sistema.

## 2. Lo ÚNICO que la Fase 1 puede nombrar: Las 8 Tablas Independientes

En la Fase 1, el código de la API y de React solo puede interactuar con estas 8 tablas, las cuales no tienen llaves foráneas (FK) hacia otras tablas:

| Tabla | Llave Primaria (PK) | Reglas / Notas importantes |
|---|---|---|
| `area_conocimiento` | `id` (INT) | Jerarquía: gran_area, area, disciplina. |
| `objetivo_desarrollo_sostenible` | `id` (INT) | 17 ODS de la ONU. Tiene campo `categoria`. |
| `area_aplicacion` | `id` (INT) | Sectores económicos/industriales. |
| `termino_clave` | `termino` (VARCHAR) | **Ojo:** La PK es un string, no un autoincremental. |
| `linea_investigacion` | `id` (SERIAL) | **Ojo:** Única tabla maestra con ID autoincremental (SERIAL) en el script inicial. |
| `aliado` | `nit` (INT) | Entidades externas. La PK es el NIT. |
| `tipo_producto` | `id` (INT) | Categoría, clase, tipología de Minciencias. |
| `proyecto` | `id` (INT) | Proyectos de investigación (sin FKs en su tabla origen). |

### El modelo en C# (Entity Framework Core)

Para cada tabla, existirá una clase en la carpeta `Models/` que mapea exactamente estas columnas. Por ejemplo, para `area_conocimiento`:

```csharp
public class area_conocimiento
{
    public int id { get; set; }
    public required string gran_area { get; set; }
    public required string area { get; set; }
    public required string disciplina { get; set; }
    public bool activo { get; set; } // El campo de borrado lógico
}
```

## 3. Las dos murallas de validación

Para asegurar la integridad de los datos, el sistema cuenta con dos barreras:

1. **La API (ASP.NET Core con DTOs):** Valida la forma, tipos y longitudes de las cadenas a través de las peticiones (ej. `AreaConocimientoCrear`). Si falla, devuelve `400 Bad Request` ANTES de tocar EF Core.
2. **La BD (Supabase):** La última línea de defensa. Restricciones `NOT NULL`, `UNIQUE` y Tipos de Datos definidos en el script SQL. Si la API deja pasar un NIT duplicado, Supabase lo rechaza y la API devuelve un `500`.

## 4. Reglas estrictas de esta fase

- El código de la Fase 1 **solo puede nombrar las 8 tablas maestras** — las otras 13 tablas existen en Supabase pero son territorio de la Fase 2 y Fase 3. No se deben crear controladores ni modelos para ellas aún.
- La base de datos **no se modifica**: ni columnas nuevas, ni índices distintos a los del script. Si algo parece faltar, es porque pertenece a otra fase.
- El borrado es **100% lógico**. El método DELETE de la API debe ejecutar un UPDATE sobre el campo `activo`, poniéndolo en `false`.