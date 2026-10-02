# Plan técnico — Fase 2: Integridad Referencial y Relaciones

> **Fase 2** · CÓMO construir lo especificado en `2_spec_fase2.md`.
> Este documento rige las decisiones arquitectónicas para manejar las llaves foráneas (FK) en el Backend (C# / EF Core) y los selectores dinámicos en el Frontend (React).

---

## 1. Stack Tecnológico (Se mantiene el de la Fase 1)

*   **Backend:** C# / ASP.NET Core.
*   **Acceso a datos:** Entity Framework Core (EF Core) + Npgsql.
*   **Validación:** Data Annotations en DTOs.
*   **Base de Datos:** Supabase (PostgreSQL).
*   **Frontend:** React (Vite).

## 2. Decisiones Arquitectónicas Backend (EF Core)

El reto principal de la Fase 2 es manejar las relaciones entre tablas sin caer en problemas de rendimiento (N+1 queries) y garantizando la integridad de los datos.

### 2.1 Mapeo de Relaciones en EF Core (Navegación)

En la Fase 1, las entidades eran planas. En la Fase 2, debemos configurar las **propiedades de navegación** en los modelos (`Models/`) para que EF Core entienda las llaves foráneas.

*Ejemplo: Tabla `producto` (que depende de `proyecto` y `tipo_producto`)*

```csharp
// Models/producto.cs
public class producto
{
    public int id { get; set; }
    public required string nombre { get; set; }
    public required string categoria { get; set; }
    public DateTime fecha_entrega { get; set; }
    public bool activo { get; set; }
    
    // Llaves foráneas físicas
    public int? proyecto_id { get; set; } // Puede ser null según el script SQL
    public int tipo_producto_id { get; set; }

    // Propiedades de Navegación (Virtual) para que EF Core traiga los datos relacionados
    public virtual proyecto? ProyectoRelacionado { get; set; }
    public virtual tipo_producto TipoProductoRelacionado { get; set; }
}
```

En el `postgresContext.cs`, se debe asegurar que estas relaciones estén bien mapeadas usando Fluent API en `OnModelCreating` si las convenciones de nombres no son suficientes.

### 2.2 Eager Loading (Carga Ansiosa) con `.Include()`

Para cumplir con el **RNF1** (evitar queries N+1), los repositorios de la Fase 2 DEBEN usar `.Include()` al consultar datos.

*Ejemplo en `ProductoRepository.cs`:*

```csharp
public async Task<IEnumerable<producto>> ObtenerTodosAsync()
{
    // NO hacer esto: return await _context.producto.ToListAsync();
    
    // HACER ESTO: Traer el producto Y la información de su tipo en una sola consulta SQL
    return await _context.producto
        .Include(p => p.TipoProductoRelacionado)
        .Include(p => p.ProyectoRelacionado)
        .ToListAsync();
}
```

### 2.3 Manejo de Ciclos de Referencia (JSON)

Al usar `.Include()`, es común que se generen referencias circulares al serializar a JSON (ej: Producto -> TipoProducto -> Productos -> TipoProducto...).

Para evitar que la API explote con un error 500 durante la serialización, se debe configurar el serializador JSON en `Program.cs`:

```csharp
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Ignorar ciclos de referencia
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });
```
*(Nota: Aunque se ignore el ciclo, es mejor práctica usar DTOs de salida (Response DTOs) en el Servicio para aplanar el JSON que se envía al cliente, enviando solo lo necesario, como se muestra en los contratos).*

### 2.4 Tablas Puente (Muchos a Muchos)

Para las tablas como `ac_proyecto` (relación entre `proyecto` y `area_conocimiento`), el modelo tendrá una clave primaria compuesta.

```csharp
// postgresContext.cs
modelBuilder.Entity<ac_proyecto>()
    .HasKey(ac => new { ac.proyecto_id, ac.area_conocimiento_id });
```
La actualización (PUT) de estas tablas generalmente se maneja como una reasignación: el cliente envía la nueva lista de áreas para un proyecto, el backend borra (lógicamente) las relaciones viejas y crea las nuevas.

## 3. Decisiones Arquitectónicas Frontend (React)

El frontend debe evolucionar para que el usuario seleccione datos relacionales de forma amigable.

### 3.1 Selectores Dinámicos (`<select>`)

Al crear un formulario para una tabla con FK (ej. crear un `producto`), no se le pide al usuario que digite el ID del `tipo_producto`. Se le muestra un desplegable.

**Flujo en React:**
1.  El componente `FormularioProducto` se monta.
2.  Un `useEffect` hace un `GET /api/TipoProducto` y guarda la respuesta en una variable de estado (`tiposProducto`).
3.  El formulario renderiza un `<select>` mapeando `tiposProducto`. El `value` del option será el `id` y el texto visible será el `nombre`.
4.  Al hacer submit, el JSON enviado al backend incluye el `id` seleccionado.

### 3.2 Componentes Reutilizables (Recomendado)

Dado que habrá muchos selectores, se recomienda crear un componente React reutilizable (ej: `<SelectDinamico endpoint="/api/TipoProducto" ... />`) que encapsule la lógica de fetching, estados de carga (loading) y manejo de errores, para mantener el código limpio.