# Contratos HTTP — Fase 3: Autenticación y Gestión de Usuarios

> **Fase 3** · Base local (Ejemplo): `http://localhost:5000`. Estos contratos definen la estructura estricta para la generación del token JWT (Login) y el CRUD administrativo de usuarios.

---

## 0. Convenciones de Seguridad (Middlewares)

- **Endpoints Protegidos:** A partir de esta fase, TODOS los endpoints de la Fase 1 y Fase 2 requieren que el cliente envíe el token en los encabezados HTTP.
  - Header requerido: `Authorization: Bearer <TOKEN_JWT_AQUI>`
  - Si falta el header o el token expiró → **401 Unauthorized**
  - Si el token es válido pero el rol no tiene permisos → **403 Forbidden**

## 1. `POST /api/Auth/login` — Autenticación Pública

Endpoint público (no requiere token). Recibe las credenciales y devuelve el JWT si son válidas.

```http
POST /api/Auth/login
Content-Type: application/json

{
  "username": "admin",
  "password": "MiPasswordSeguro123"
}
```

**Respuesta Exitosa (200 OK):**
```json
{
  "estado": 200,
  "mensaje": "Autenticación exitosa",
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJhZG1pbiIsInJvbGVzIjpbIkFkbWluaXN0cmFkb3IiXSwiZXhwIjoxNjk1NDMxMjAwfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c",
  "usuario": {
    "username": "admin",
    "nombre_completo": "Camilo Alejandro M.",
    "roles": ["Administrador"]
  }
}
```
*(Nota para React: Guardar el string del `token` en `localStorage` y la información del `usuario` en el Contexto global).*

**Respuesta Credenciales Inválidas (401 Unauthorized):**
```json
{
  "estado": 401,
  "mensaje": "Credenciales inválidas",
  "detalle": "El usuario o la contraseña no coinciden."
}
```

## 2. `GET /api/Usuario` — Listar Usuarios (Solo Administrador)

Endpoint protegido. Solo accesible si el JWT contiene el rol "Administrador".

```http
GET /api/Usuario
Authorization: Bearer <TOKEN>
```

**Respuesta (200 OK):**
```json
{
  "tabla": "