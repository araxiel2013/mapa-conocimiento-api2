# Investigación y decisiones — Fase 2 (Relaciones y FKs)

> **Fase 2** · **Lectura opcional**. Funciona como registro de decisiones de arquitectura (**ADR**) para los retos específicos de integridad referencial introducidos en esta fase. Complementa a `3_plan_fase2.md`.

---

## D1 — Estrategia de carga de datos relacionados (Navegación EF Core)

**Contexto:** Al consultar tablas con llaves foráneas (ej. `producto` que depende de `tipo_producto`), el frontend necesita el nombre del tipo, no solo su ID.
**Opciones evaluadas:** 
(a) Lazy Loading (Carga perezosa) 
(b) **Eager Loading (Carga ansiosa con `.Include()`)**
(c) Explicit Loading (Carga explícita).
**Decisión: (b) Eager Loading.**
**Por qué:** Activar Lazy Loading en APIs REST suele ser un antipatrón catastrófico porque dispara el problema de "N+1 consultas" (el motor hace una consulta a Supabase por cada registro de la lista de forma individual). Exigimos el uso de `.Include()` en los repositorios para que EF Core resuelva la relación mediante un único `JOIN` en la base de datos, optimizando la red y el rendimiento.

## D2 — Prevención de Ciclos de Referencia en JSON

**Contexto:** Al usar relaciones bidireccionales en EF Core, el serializador JSON puede entrar en un bucle infinito (ej. Proyecto -> Lista de Productos -> Cada Producto apunta al Proyecto -> ...).
**Opciones evaluadas:** 
(a) Configurar `ReferenceHandler.IgnoreCycles` en `Program.cs`.
(b) **Usar Response DTOs (Data Transfer Objects de salida).**
**Decisión: Implementar (a) como red de seguridad, pero exigir (b) como buena práctica.**
**Por qué:** Ignorar los ciclos evita que la API arroje un error 500, pero genera JSONs masivos y desordenados para React. Al crear clases DTO específicas para las respuestas (ej. `ProductoListarResponse`), el desarrollador tiene control total de qué datos anidados se envían al frontend, reduciendo el consumo de ancho de banda y facilitando el trabajo en React.

## D3 — Manejo del Borrado en Cascada (Borrado Lógico)

**Contexto:** La base de datos original tiene restricciones de cascada física (ej. si se borra un proyecto, se borran sus palabras clave). Sin embargo, la Constitución prohíbe el `DELETE` físico.
**Opciones evaluadas:** 
(a) Triggers en PostgreSQL (Supabase) que intercepten el UPDATE.
(b) **Manejo manual de la cascada lógica en los Servicios (C#).**
**Decisión: (b) Manejo en la capa de Servicios.**
**Por qué:** Dado que el borrado es un UPDATE (`activo = false`), el motor de PostgreSQL no disparará las reglas automáticas de `ON DELETE CASCADE`. La capa de servicio (`ProyectoService`) será la responsable de: primero, marcar el proyecto como inactivo; y segundo, buscar y marcar como inactivas todas las relaciones en las tablas puente (ej. `ac_proyecto`, `palabras_clave`) utilizando el DbContext.

## D4 — Carga de Selectores en el Frontend (React)

**Contexto:** Los formularios de React necesitan poblar `<select>` dinámicos para las llaves foráneas.
**Opciones evaluadas:** 
(a) Cargar todas las listas maestras en un contexto global (Redux/Context API) al iniciar la app.
(b) **Cargar las listas on-demand (`useEffect`) al montar cada formulario.**
**Decisión: (b) Carga on-demand por componente.**
**Por qué:** Mantiene la arquitectura simple en esta fase. No sobrecarga la red inicial. Si un usuario solo entra a ver el Dashboard (Fase 4), no tiene sentido descargar todos los catálogos maestros. Se recomienda crear un Custom Hook (ej. `useFetchCatalog()`) en React para no repetir la lógica del `useEffect` en cada formulario.