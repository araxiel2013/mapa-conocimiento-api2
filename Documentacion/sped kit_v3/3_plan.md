# Plan técnico — Fase 3: Seguridad, Autenticación y Roles

> **Fase 3** · CÓMO construir lo especificado en `2_spec_fase3.md`.
> Este documento rige las decisiones arquitectónicas para la implementación de JSON Web Tokens (JWT) en el Backend y la gestión del estado de sesión en el Frontend (React).

---

## 1. Stack Tecnológico de Seguridad

| Pieza | Herramienta | Por qué |
|---|---|---|
| Autenticación API | **Microsoft.AspNetCore.Authentication.JwtBearer** | Paquete oficial de ASP.NET Core para validar JWT de forma nativa en los middlewares. |
| Generación de JWT | **System.IdentityModel.Tokens.Jwt** | Para crear (firmar) el token al hacer un Login exitoso. |
| Hashing de Contraseñas | **BCrypt.Net-Next** | Estándar de la industria para encriptar contraseñas. Incluye algoritmos de *salting* automático (previene ataques de diccionario/rainbow tables). |
| Interceptor HTTP | **Axios (React)** | Permite inyectar el token automáticamente en el Header `Authorization` de todas las peticiones que salen del navegador. |

## 2. Decisiones Arquitectónicas Backend (C#)

### 2.1 Modelo de Datos (Usuarios y Roles)
El script de base de datos incluye 3 tablas clave para la seguridad:
1. `usuario`: Almacena el `username`, el `email` y el `password` (que será un hash BCrypt, no texto plano).
2. `rol`: Catálogo de permisos (ej. "Administrador", "Consulta", "Editor").
3. `rol_usuario`: Tabla puente (Muchos a Muchos). Un usuario puede tener múltiples roles.

Al igual que en las fases anteriores, estas tablas deben mapearse en `postgresContext.cs` utilizando Entity Framework Core.

### 2.2 Creación del Token (AuthService)
Cuando el usuario envía sus credenciales al endpoint `/api/Auth/login`:
1. El `AuthService` busca el usuario por email o username en la BD.
2. Compara la contraseña usando `BCrypt.Verify(passwordPlano, hashAlmacenado)`.
3. Si es correcto, carga la lista de roles del usuario desde `rol_usuario`.
4. Construye el JWT agregando **Claims** (Reclamaciones/Datos):
   - `ClaimTypes.Name`: username
   - `ClaimTypes.Email`: email
   - `ClaimTypes.Role`: Administrador (se agrega un claim por cada rol).
5. Firma el token con una clave secreta (guardada en `appsettings.json`) y lo retorna.

### 2.3 Middlewares y Decoradores (Autorización)
Para proteger la API, se configura la autenticación en `Program.cs`. 
Luego, los controladores se protegen mediante atributos:

- Proteger toda una clase: `[Authorize]` sobre `AreaConocimientoController` (requiere estar logueado, sin importar el rol).
- Restringir por Rol (RBAC):
  ```csharp
  // Solo los administradores pueden gestionar usuarios
  [Authorize(Roles = "Administrador")]
  [HttpPost]
  public async Task<IActionResult> CrearUsuario(UsuarioCrearDto dto) { ... }
  ```
  ```csharp
  // Editores y Administradores pueden editar proyectos
  [Authorize(Roles = "Administrador, Editor")]
  [HttpPut("{id}")]
  public async Task<IActionResult> EditarProyecto(int id, ProyectoEditarDto dto) { ... }
  ```

## 3. Decisiones Arquitectónicas Frontend (React)

### 3.1 Almacenamiento del Token
El JWT recibido en el Login se guardará en el **`localStorage`** o `sessionStorage` del navegador.
*Nota de seguridad:* Aunque el `localStorage` es vulnerable a ataques XSS (Cross-Site Scripting), es el estándar para arquitecturas SPA (Single Page Applications) desacopladas como esta fase.

### 3.2 Interceptor de Axios
Para no repetir código, se configurará una instancia de `axios` que atrape cada petición antes de salir y le inyecte el token:
```javascript
import axios from 'axios';

const api = axios.create({ baseURL: import.meta.env.VITE_API_URL });

api.interceptors.request.use(config => {
    const token = localStorage.getItem('token');
    if (token) {
        config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
});
```

### 3.3 Contexto de Autenticación (AuthContext)
Se implementará un `AuthContext` (React Context API) para mantener el estado global del usuario (ej. `{ isAuthenticated: true, user: { username, roles } }`).
- Si `isAuthenticated` es falso, el enrutador (`react-router-dom`) redirige a `/login` protegiendo las rutas internas (Protected Routes).
- Los componentes de UI pueden leer los `roles` desde el contexto para ocultar botones. (Ej. `{user.roles.includes('Administrador') && <button>Borrar</button>}`).

## 4. Configuración Segura (Appsettings)
El archivo `appsettings.json` debe incluir una clave robusta para firmar los tokens. **Esta clave NUNCA debe subirse a GitHub** en un entorno de producción (se ignoraría en `.gitignore` o se usarían variables de entorno reales de la nube), pero para fines del entorno de desarrollo local, se estructurará así:

```json
{
  "JwtSettings": {
    "Key": "UnaClaveSecretaMuyLargaYComplejaParaElModuloMapaConocimiento123!*",
    "Issuer": "MapaConocimientoAPI",
    "Audience": "MapaConocimientoReactClient",
    "DurationInMinutes": 480
  }
}
```