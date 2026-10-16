# Quickstart — Fase 3: arranque y smoke test

> **Fase 3** · Validación rápida de la seguridad, autenticación (JWT) y el control de roles. Si aún no hay código construido para esta fase, empiece por `8_tasks_fase3.md`.

---

## 1. Arranque del entorno de desarrollo

El entorno requiere que el secreto del JWT esté correctamente configurado en el backend y que exista un usuario administrador inicial en Supabase.

### Backend (C# API)
Verifique que su `appsettings.json` tenga la sección `JwtSettings`. Luego levante la API:
```powershell
cd mapa_conocimiento_api
dotnet watch
```
*(Levanta en `http://localhost:5000`)*

### Frontend (React)
```powershell
cd mapa_conocimiento_client
npm run dev
```
*(Levanta en `http://localhost:5173`)*

## 2. Smoke test (Prueba de Seguridad en API)

Puede usar `curl`, Postman o Swagger. Swagger es altamente recomendado si configuró el botón de "Authorize" (candado).

```powershell
# 1. Prueba de Bloqueo (Acceso Anónimo Denegado)
curl.exe -i http://localhost:5000/api/AreaConocimiento
# → DEBE responder 401 Unauthorized. Ningún dato debe ser filtrado.

# 2. Login Exitoso (Generación de Token)
curl.exe -X POST http://localhost:5000/api/Auth/login -H "Content-Type: application/json" -d "{\"username\": \"admin\", \"password\": \"MiPasswordSeguro123\"}"
# → DEBE responder 200 OK y devolver un JSON con el campo "token".
# Copie ese token generado para los siguientes pasos.

# 3. Acceso Permitido (Con Token Bearer)
# Reemplace <EL_TOKEN_AQUI> con el token real obtenido en el paso 2.
curl.exe -H "Authorization: Bearer <EL_TOKEN_AQUI>" http://localhost:5000/api/AreaConocimiento
# → DEBE responder 200 OK y retornar el listado de áreas.

# 4. Prueba de Autorización por Roles (RBAC)
# Intente acceder al CRUD de usuarios con el token de un usuario rol "Consulta".
curl.exe -i -H "Authorization: Bearer <TOKEN_DE_USUARIO_CONSULTA>" http://localhost:5000/api/Usuario
# → DEBE responder 403 Forbidden.
```

## 3. Smoke test (Prueba de Frontend - React)

1. **Prueba de Redirección (Rutas Protegidas):**
   - Abra una ventana de incógnito en su navegador.
   - Intente navegar directamente a `http://localhost:5173/proyectos`.
   - El sistema debe redirigirlo forzosamente a `http://localhost:5173/login`.
2. **Prueba de Persistencia (LocalStorage):**
   - Inicie sesión con credenciales válidas.
   - Abra las herramientas de desarrollador (F12) -> Pestaña "Application" (o Almacenamiento) -> Local Storage.
   - Debe ver una clave llamada `token` guardada allí.
   - Presione F5 (Refrescar). La sesión debe mantenerse activa sin volverle a pedir credenciales.
3. **Prueba de Renderizado Dinámico de Menús:**
   - Inicie sesión con una cuenta que **NO** sea Administrador.
   - Verifique que la opción "Gestión de Usuarios" o "Panel Admin" **no existe** en el menú de navegación.

## 4. Resolución de problemas comunes de la Fase 3

| Síntoma | Causa probable |
|---|---|
| Login devuelve `401 Credenciales Inválidas` pero la clave es correcta | El registro en la base de datos (Supabase) tiene la contraseña en texto plano en lugar del hash generado por BCrypt. BCrypt siempre fallará si intenta comparar contra texto plano. |
| API rechaza el token válido con `401 Unauthorized` | El header HTTP está mal formateado. Asegúrese de que el Frontend envíe el encabezado exactamente como: `Authorization: Bearer <token_aqui>` (con el espacio y la palabra Bearer). |
| API devuelve `403 Forbidden` a un Administrador | El Claim de rol en la generación del token (C#) tiene un nombre distinto (ej. `Role` vs `roles`) o hay errores tipográficos en el decorador (ej. `[Authorize(Roles="admin")]` en vez de `"Administrador"`). |
| React pierde la sesión al recargar la página | El estado inicial del `AuthContext` no está leyendo el `localStorage` en su primera carga (al montarse), por lo que asume falsamente que el usuario no está logueado. |