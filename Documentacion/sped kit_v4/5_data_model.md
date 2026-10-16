# Modelo de datos — Fase 4: Consultas Analíticas (El Grafo Completo)

> **Fase 4** · En esta fase no se crean tablas nuevas. El objetivo es explotar la estructura relacional de las 21 tablas construidas en las Fases 1, 2 y 3 para extraer valor (Analítica y KPIs).

---

## 1. El Grafo Relacional (Visión General)

El sistema transaccional ya garantiza que los datos ingresen limpios y con integridad referencial. Ahora, la capa de Servicios Analíticos (en C#) navegará por estas relaciones.

El centro gravitacional del modelo es la tabla **`proyecto`**. Casi todas las métricas pivotan alrededor de esta entidad.

### Rutas de Navegación Analítica (Ejemplos)

Para que el Dashboard muestre métricas útiles, EF Core (vía LINQ) debe recorrer estas "rutas" mediante `Include` y `ThenInclude`:

*   **Ruta Académica:** `proyecto` -> `ac_proyecto` (puente) -> `area_conocimiento` -> (Agrupación por `gran_area` o `disciplina`).
*   **Ruta de Producción:** `proyecto` -> `producto` -> `tipo_producto` -> (Agrupación por `clase` o `tipologia`).
*   **Ruta de Impacto:** `proyecto` -> `ods_proyecto` (puente) -> `objetivo_desarrollo_sostenible` -> (Agrupación por `categoria`).
*   **Ruta de Personal:** `docente` -> `linea_investigacion` // `docente` -> `desarrolla` (puente) -> `proyecto`.

## 2. Requerimientos de LINQ en Entity Framework Core

Dado que no crearemos Vistas (Views) en Supabase, el motor de C# debe traducir el código LINQ a SQL puro que PostgreSQL pueda ejecutar eficientemente.

### 2.1 Uso intensivo de `Select()` (Proyección de Datos)
El mayor riesgo en esta fase es hacer esto:
❌ `var data = await _context.proyecto.Include(...).Include(...).ToListAsync();`
*(Esto trae cientos de megabytes de datos a la memoria RAM de Render).*

Lo correcto es proyectar (seleccionar **únicamente** las columnas necesarias) ANTES de ejecutar la consulta contra la base de datos:
✅
```csharp
var datosKpi = await _context.proyecto
    .Where(p => p.activo)
    .Select(p => new {
        // Solo traemos 2 columnas de las 15 que tiene el proyecto
        Año = p.fecha_inicio.Year,
        Presupuesto = p.presupuesto_total
    })
    .ToListAsync();
```

### 2.2 Funciones de Agregación
Las consultas del `ReportesService.cs` harán uso masivo de:
*   `.GroupBy(x => x.campo)` (Agrupar por categorías).
*   `.Count()` (Contar registros, ej. cantidad de proyectos).
*   `.Sum(x => x.presupuesto)` (Sumar valores monetarios o numéricos).

## 3. Ejemplo de Estructura de Respuesta (DTO Analítico)

Las clases DTO en esta fase no son para validación de entrada (como en la Fase 1), sino para **tipar la salida** de las gráficas.

```csharp
// Peticiones/Reportes/GraficoBarraDto.cs
public class GraficoBarraDto
{
    // El eje X (ej. "Ciencias Naturales", "Salud", "Fin de la pobreza")
    public required string Etiqueta { get; set; } 
    
    // El eje Y (ej. 15 proyectos, 500 millones)
    public decimal Valor { get; set; } 
}
```

Al obligar a todas las consultas a retornar arreglos de `GraficoBarraDto`, el Frontend en React puede tener un solo componente genérico `<GraficoBarras data={datosDelEndpoint} />` que sirva para renderizar 5 o 6 reportes distintos sin cambiar una línea de código en React.