# Mapa de fases del proyecto

> La ruta completa del proyecto Módulo: Mapa de Conocimiento. Cada fase se especifica SOLO cuando la anterior está cerrada (commit + merge a main). Este mapa da la dirección general; los documentos de cada fase darán el detalle.

| Fase | Qué agrega | Estado |
|---|---|---|
| **Fase 0** | **Preparación y Arquitectura Base:** Creación de la BD (21 tablas) en Supabase (PostgreSQL) con el script provisto. Configuración de repositorios en GitHub (Backend y Frontend). Estructura del proyecto en C# (carpetas, EF Core context). | Sin especificar |
| **Fase 1** | **Tablas Independientes:** Desarrollo Backend (API C#) y Frontend (React) del CRUD con borrado lógico para las 8 tablas maestras (sin dependencias/FK). Ej: `area_conocimiento`, `objetivo_desarrollo_sostenible`. | **En curso** |
| **Fase 2** | **Integridad Referencial:** Implementación CRUD para las 10 tablas restantes con claves foráneas (FK). Ej: `docente`, `producto`, relaciones puente. El frontend implementará selectores (listas desplegables) dinámicos consumiendo la API. | Sin especificar |
| **Fase 3** | **Seguridad, Autenticación y Roles:** Endpoint de Login, implementación de tokens JWT, middlewares de seguridad, manejo de sesión en React, ocultamiento de menús según rol y panel CRUD de usuarios/roles. | Sin especificar |
| **Fase 4** | **Analítica, Identidad Corporativa y Despliegue:** 10 consultas multitabla complejas (JOINs) expuestas vía API. Desarrollo de página corporativa, Dashboard analítico en React y despliegue final en servidores gratuitos. | Sin especificar |

> **El destino del proyecto:** La Fase 4 dejará el sistema completo desplegado y funcional, con el backend en C# sirviendo datos mediante servicios REST desde Supabase, y el frontend en React gestionando las vistas responsivas y roles, para cumplir con el hito final de cierre y despliegue (13 de noviembre de 2026).

**Reglas del mapa** (Constitución, Artículo 1): no se anticipa nada de una fase futura; una fase cerrada no se reabre para agregar nuevas características (los ajustes van en la siguiente); la rama `main` del repositorio siempre muestra la última fase cerrada funcionando.
