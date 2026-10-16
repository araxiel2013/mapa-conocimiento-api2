# Contratos HTTP — Fase 4: Analítica y Reportes

> **Fase 4** · Base en la nube (Ejemplo): `https://mapa-api.onrender.com`. 
> Todos estos endpoints están protegidos y requieren el JWT Bearer Token (Rol: Administrador o Consulta).
> La regla principal: La API debe entregar la data "masticada" (agrupada y sumada), lista para ser inyectada directamente en los componentes de gráficos de React (ej. Recharts).

---

## 0. Convenciones para Gráficos

Para simplificar el frontend, el backend estandarizará la salida. En lugar de devolver objetos anidados complejos, devolverá arreglos planos.

-   **Para Gráficos de Barras / Líneas:** Arreglo de `{ etiqueta, valor }`.
-   **Para Gráficos de Torta (Pie / Donut):** Arreglo de `{ nombre, cantidad }`.
-   **Para KPIs (Tarjetas de resumen):** Objeto simple con totales `{ totalProyectos, totalPresupuesto, ... }`.

## 1. `GET /api/Reportes/kpis-generales` — Tarjetas de Resumen

Métrica global para la parte superior del Dashboard. Cruza información básica de las tablas `proyecto`, `docente` y `producto`.

```http
GET /api/Reportes/kpis-generales
Authorization: Bearer <TOKEN>
```
**Respuesta Exitosa (200 OK):**
```json
{
  "total_proyectos_activos": 142,
  "total_docentes_investigadores": 35,
  "presupuesto_total_historico": 850000000,
  "total_productos_entregados": 89
}
```

## 2. `GET /api/Reportes/proyectos-por-gran-area` — Gráfico de Barras

Agrupación utilizando las tablas puente `ac_proyecto` y `area_conocimiento`.

```http
GET /api/Reportes/proyectos-por-gran-area
Authorization: Bearer <TOKEN>
```
**Respuesta Exitosa (200 OK):**
```json
[
  { "etiqueta": "Ingeniería y Tecnología", "valor": 45 },
  { "etiqueta": "Ciencias Sociales", "valor": 30 },
  { "etiqueta": "Ciencias Médicas y de la Salud", "valor": 25 },
  { "etiqueta": "Ciencias Naturales", "valor": 42 }
]
```
*(Nota para React: Este JSON se le pasa directamente a la propiedad `data` del `<BarChart>` de Recharts, configurando el `<XAxis dataKey="etiqueta">` y el `<Bar dataKey="valor">`).*

## 3. `GET /api/Reportes/presupuesto-por-financiacion` — Gráfico de Torta (Donut)

Agrupación monetaria. Requiere cruzar `proyecto` con la tabla `tipo_financiacion`.

```http
GET /api/Reportes/presupuesto-por-financiacion
Authorization: Bearer <TOKEN>
```
**Respuesta Exitosa (200 OK):**
```json
[
  { "nombre": "Recursos Internos", "cantidad": 300000000 },
  { "nombre": "Minciencias", "cantidad": 450000000 },
  { "nombre": "SENA - Sennova", "cantidad": 100000000 }
]
```

## 4. `GET /api/Reportes/productos-por-ods` — Gráfico de Barras Horizontales

Consulta compleja: Cuántos productos (Software, Artículos, etc.) se han generado apoyando cada Objetivo de Desarrollo Sostenible (ODS). Requiere cruzar: `producto` -> `proyecto` -> `ods_proyecto` -> `objetivo_desarrollo_sostenible`.

```http
GET /api/Reportes/productos-por-ods
Authorization: Bearer <TOKEN>
```
**Respuesta Exitosa (200 OK):**
```json
[
  { "etiqueta": "Educación de calidad", "valor": 15 },
  { "etiqueta": "Industria, innovación e infraestructura", "valor": 22 },
  { "etiqueta": "Ciudades y comunidades sostenibles", "valor": 8 }
]
```

## 5. (y hasta 10 métricas adicionales...)

El equipo de desarrollo debe implementar al menos 10 endpoints similares a estos en `ReportesController`, asegurando que cada uno cruce (JOIN) al menos 4 tablas y use LINQ para las agrupaciones (`GroupBy`) y conteos (`Count`, `Sum`). Todos seguirán la estructura plana de `etiqueta/valor` o `nombre/cantidad`.