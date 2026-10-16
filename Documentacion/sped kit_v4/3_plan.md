# Plan técnico — Fase 4: Analítica y Despliegue

> **Fase 4** · CÓMO construir lo especificado en `2_spec_fase4.md`.
> Este documento rige las decisiones sobre la estructuración de consultas analíticas (Reportes) en C#, el diseño visual en React y la estrategia para publicar el aplicativo en internet.

---

## 1. Stack Tecnológico (Cierre de Proyecto)

El stack transaccional es el mismo, pero sumamos herramientas visuales y de alojamiento.

| Pieza | Herramienta / Servicio | Función |
|---|---|---|
| Gráficos en Frontend | **Recharts** o **Chart.js** (vía `react-chartjs-2`) | Librerías de React ligeras para pintar el Dashboard (gráficos de barras, tortas, líneas). |
| Estilos y Layout | **Tailwind CSS** o **Bootstrap** | Para garantizar que la Landing Page y el Dashboard sean *Responsive* de forma rápida. |
| Hosting Base de Datos | **Supabase (PostgreSQL)** | (Ya configurado desde Fase 0). Soporta el aplicativo público. |
| Hosting API (Backend) | **Render / MonsterASP / Azure (Free Tier)** | Alojamiento de la API compilada en C#. |
| Hosting Cliente (Frontend) | **Vercel / Netlify** | Despliegue estático ultra-rápido para el proyecto construido con Vite. |

## 2. Decisiones Arquitectónicas Backend (Reportes)

El principal desafío técnico de la Fase 4 no es el CRUD (que ya funciona), sino procesar información cruzando muchas tablas para generar métricas institucionales.

### 2.1 El Controlador de Analítica
Se creará un único controlador llamado `ReportesController.cs`. Todos sus métodos serán accesibles solo para roles autorizados (ej. `[Authorize(Roles = "Administrador, Consulta")]`). 

No usará los repositorios transaccionales de las fases anteriores, sino un servicio dedicado a consultas masivas (ej. `IReportesService`).

### 2.2 Consultas Multitabla Complejas en EF Core (LINQ)

Para lograr consultas que involucren 4 o más tablas (como pide la especificación), usaremos las propiedades de navegación (`Include`, `ThenInclude`) combinadas con agrupaciones (`GroupBy`) y proyecciones (`Select`).

*Ejemplo de concepto (Métrica: Cantidad de proyectos por Gran Área del Conocimiento):*
*(Tablas involucradas: `gran_area` <- `area_conocimiento` <- `ac_proyecto` <- `proyecto`)*

```csharp