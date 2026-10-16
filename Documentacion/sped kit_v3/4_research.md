# Investigación y decisiones — Fase 3 (Seguridad y Usuarios)

> **Fase 3** · **Lectura opcional**. Funciona como registro de decisiones de arquitectura (**ADR**) para los retos específicos de Autenticación (AuthN) y Autorización (AuthZ). Complementa a `3_plan_fase3.md`.

---

## D1 — Estrategia de Autenticación: JWT vs Cookies

**Contexto:** La API necesita saber quién hace la petición para autorizarla o rechazarla.
**Opciones evaluadas:** 
(a) Sesiones basadas en servidor (Cookies HTTP-Only).
(b) **JSON Web Tokens (JWT) sin estado (Stateless).**
**Decisión: (b) JWT.**
**Por qué:** La Constitución del proyecto (Artículo 4) exige "Backend y Frontend totalmente separados". JWT es el estándar moderno para arquitecturas desacopladas (SPA como React + API REST). A diferencia de las cookies de sesión tradicionales, la API (C#) no necesita guardar la sesión del usuario en memoria o en Supabase; simplemente valida que la firma matemática del token sea correcta, ahorrando recursos del servidor.

## D2 — Almacenamiento Seguro de Contraseñas

**Contexto:** Los usuarios deben tener una contraseña para iniciar sesión. Guardarlas en texto plano es una violación crítica de seguridad.
**Opciones evaluadas:** 
(a) MD5 o SHA-256 (Algoritmos de Hash rápidos).
(b) **BCrypt (Algoritmo de Hash lento con Salting).**
**Decisión: (b) BCrypt.**
**Por qué:** Los algoritmos como SHA-256 son demasiado rápidos, haciéndolos vulnerables a ataques de fuerza bruta. BCrypt es "lento" por diseño e incluye un *Salt* (una cadena aleatoria) único por cada contraseña. Esto significa que si dos usuarios tienen la contraseña "123456", los hashes almacenados en Supabase serán completamente distintos. Se utilizará el paquete `BCrypt.Net-Next` en C#.

## D3 — Persistencia del Token en React

**Contexto:** Cuando el backend retorna el JWT al hacer login, el frontend debe guardarlo para usarlo en peticiones futuras.
**Opciones evaluadas:** 
(a) `localStorage`.
(b) `sessionStorage`.
(c) Memoria de React (Variables de estado / Context).
**Decisión: (a) `localStorage`.**
**Por qué:** Guardarlo en la memoria de React (`useState`) hace que el usuario pierda la sesión (se desloguee) si presiona F5 o refresca la página. `sessionStorage` lo pierde si cierra la pestaña. `localStorage` persiste la sesión hasta que el JWT expira, ofreciendo la mejor experiencia de usuario para este proyecto académico, aunque requiera precauciones contra ataques XSS.

## D4 — Carga y Decodificación de Roles en el Cliente

**Contexto:** React necesita saber si el usuario es "Administrador" para mostrarle el botón de "Gestión de Usuarios".
**Opciones evaluadas:** 
(a) Hacer una petición `GET /api/Auth/me` en cada carga de página.
(b) **Decod