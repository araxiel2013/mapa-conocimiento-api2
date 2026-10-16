# Investigación y decisiones — Fase 1 (C# / React / Supabase)

> **Fase 1** · **Lectura opcional** (el porqué de las decisiones del plan, con las alternativas que se evaluaron y descartaron). Complementa a `3_plan.md`; el orden de trabajo está en `8_tasks.md`.

---

## D1 — El ejecutor de la capa de datos: Entity Framework Core (un ADR completo)

> Este documento funciona como registro de decisiones de arquitectura (**ADR**, *Architecture Decision Record*): cada D# captura contexto → opciones → criterios → decisión → consecuencias.

**Contexto.** La capa de repositorios necesita (1) ejecutar consultas contra Supabase y (2) mapear filas ↔ objetos del modelo. 
**Opciones evaluadas:** (a) **Entity Framework Core (EF Core)** · (b) Dapper (micro-ejecutor) · (c) ADO.NET "crudo".
**Decisión: (a) EF Core.** Se elige EF Core principalmente por los **Global Query Filters**. Como la especificación exige estrictamente un "borrado lógico" (`activo = false`), EF Core permite configurar este filtro globalmente en el `DbContext`. Así se garantiza que ninguna consulta accidental traiga datos eliminados. Además, usar LINQ sobre las 21 tablas acelera el desarrollo.
**Consecuencias.** (+) Filtros globales automáticos. (+) Protección nativa contra inyección SQL. (−) Curva de aprendizaje ligeramente mayor para configurar el contexto.

## D2 — Capas completas desde el día 1 (y no un MVP en un solo archivo)

**Alternativa descartada:** Todo en `Program.cs` con minimal APIs y refactorizar a capas después.
**Decisión:** controller → servicio → repositorio con interfaces desde la Fase 1.
**Por qué:** El valor de la Fase 1 es el **esqueleto** sobre el que crecen las demás fases sin reescribir. Si las capas encajan para las 8 tablas maestras, las relaciones con claves foráneas (Fase 2) caerán en surcos ya hechos. 

## D3 — El ensamblador es la DI de Program.cs

**Alternativa descartada:** Escribir repositorios genéricos complejos desde el principio.
**Decisión:** Registros `AddScoped` en `Program.cs` que instancian las dependencias de cada tabla.
**Por qué:** Inyectar `IAreaConocimientoRepository` directamente al servicio mantiene las capas limpias. El framework (ASP.NET Core) se encarga de resolver el ciclo de vida, respetando la arquitectura estricta solicitada por el docente.

## D4 — La BD completa desde la Fase 1 (la API solo toca 8 tablas)

**Alternativa descartada:** Una BD mínima que crece con cada fase.
**Decisión:** El script de 21 tablas se ejecuta COMPLETO en Supabase desde el inicio. La regla es que el código de la Fase 1 solo toca las 8 tablas independientes.
**Por qué:** La BD es **infraestructura dada**. Evita problemas de migraciones (EF Core Migrations) entre entregas y deja el terreno listo para la Fase 2 (tablas con dependencias).

## D5 — La validación vive en las PETICIONES (DTOs, una por verbo)

**Alternativas descartadas:** Validar con `ifs` dentro del controlador.
**Decisión:** Clases DTO (ej. `AreaConocimientoCrear`) que DECLARAN sus reglas con anotaciones (`[Required]`, `[MaxLength]`). ASP.NET valida y responde 400/422 automático.
**Por qué:** Es la manera idiomática del framework. Protege las capas internas; un JSON con un tipo incorrecto o campo faltante ni siquiera llega a ejecutarse en el controlador.

## D6 — Supabase como motor de base de datos

**Alternativas descartadas:** PostgreSQL local en contenedor Docker (como se propuso originalmente) o SQL Server.
**Decisión:** PostgreSQL alojado en la nube con **Supabase**.
**Por qué:** Cumple automáticamente con el requisito de despliegue de la Fase 4 (servidores gratuitos antes de octubre 2026). Permite a Camilo, Tatiana y Kevin desarrollar en paralelo consumiendo la misma base de datos real, evitando conflictos de sincronización de datos locales.

## D7 — Ejecución local con Hot Reload (`dotnet watch` y Vite)

**Alternativa descartada:** Contenedor Docker pesado para el SDK de .NET.
**Decisión:** Ejecución nativa del Backend con `dotnet watch` y del Frontend con Vite (React).
**Por qué:** El ciclo del proyecto requiere agilidad. `dotnet watch` recompila el C# al guardar un archivo. Vite hace lo mismo con React en milisegundos. Esta separación de entornos de desarrollo respeta la regla de "proyectos totalmente separados".

## D8 — Proyectos separados (Backend y Frontend) desde la Fase 1

**Alternativa descartada:** Vistas integradas en ASP.NET Core (Razor Pages/MVC).
**Decisión:** Repositorios de código distintos: Uno para `mapa_conocimiento_api` y otro para el frontend en React.
**Por qué:** Cumple al pie de la letra la restricción técnica "Backend y Frontend deben ser proyectos totalmente separados" impuesta en el documento base. Obliga a que la comunicación sea exclusivamente vía HTTP y JSON.

## D9 — Los planos viven en los .md, como Mermaid (y no como imágenes)

**Alternativas:** Diagramas en PNG exportados.
**Decisión:** Diagramas **Mermaid** embebidos en los mismos `.md` del spec kit.
**Por qué:** Se versionan como código. Un cambio de arquitectura produce un diff legible en git (un PNG no). Además, Github los renderiza automáticamente en la vista de los repositorios.