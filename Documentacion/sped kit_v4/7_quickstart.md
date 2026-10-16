# Quickstart — Fase 4: Arranque, analítica y despliegue

> **Fase 4** · Validación del módulo completo. Aquí probaremos que los reportes generen datos, que el Dashboard los pinte y que el despliegue en la nube funcione correctamente.

---

## 1. Arranque y validación local

Antes de subir a la nube, debemos probar que la analítica y la nueva página pública funcionen perfectamente en tu máquina.

### Paso 1: Levantar los servicios locales
```powershell
# Terminal 1 (Backend)
cd mapa_conocimiento_api
dotnet watch

# Terminal 2 (Frontend)
cd mapa_conocimiento_client
npm run dev
```

### Paso 2: Smoke test de la Landing Page (Pública)
1. Abre tu navegador en `http://localhost:5173`.
2. Verifica que cargue la nueva **Landing Page Corporativa** (Inicio, Nosotros, Servicios, Contacto) sin pedirte credenciales.
3. El diseño debe verse bien tanto maximizado como simulando la pantalla de un celular (F12 -> Toggle Device Toolbar).
4. Haz clic en el botón de "Iniciar Sesión" para ir al Login.

### Paso 3: Smoke test del Dashboard (Privado)
1. Inicia sesión con el usuario `admin`.
2. Navega a la ruta del Dashboard (`http://localhost:5173/dashboard`).
3. Abre la pestaña **Network** (Red) de las herramientas de desarrollador.
4. Verifica que se disparen múltiples peticiones `GET` hacia `/api/Reportes/...` y que todas respondan `200 OK`.
5. Comprueba visualmente que los gráficos (Recharts o Chart.js) se rendericen usando esos datos. Si un endpoint devuelve `[]` (vacío), la gráfica no debe romper la página.

## 2. Pruebas de Analítica en la API (cURL)

Asegúrate de tener un JWT válido de un Administrador antes de probar esto. Reemplaza `<TOKEN>` por tu JWT.

```powershell
# 1. Probar el KPI General
curl.exe -H "Authorization: Bearer <TOKEN>" http://localhost:5000/api/Reportes/kpis-generales

# 2. Probar una consulta multitabla (ej. Proyectos por Gran Área)
curl.exe -H "Authorization: Bearer <TOKEN>" http://localhost:5000/api/Reportes/proyectos-por-gran-area
# → DEBE retornar el arreglo plano [{ "etiqueta": "...", "valor": ... }]
```

## 3. Pruebas de Despliegue en la Nube (Producción)

Una vez completadas las tareas de publicación (descritas en el archivo `8_tasks_fase4.md`):

1. **Prueba de Cold Start (Backend):** Ingresa a la URL pública de tu API en Render (ej. `https://mapa-api.onrender.com/swagger`). Si es la primera vez en el día, puede tardar hasta 1 minuto en cargar. Es normal en la capa gratuita ("Cold Start").
2. **Prueba de CORS y Conexión (Frontend):** Ingresa a la URL de Vercel/Netlify (ej. `https://mapa-conocimiento.vercel.app`).
   - Haz Login. Si falla, presiona F12 y revisa la consola. Si dice "CORS Error", faltó configurar el dominio de Vercel en el `Program.cs` del backend desplegado.
3. **Prueba de Enrutamiento SPA:** Navega a la pestaña de "Nosotros" en el frontend desplegado y presiona **F5**. Si sale un error `404 Not Found`, faltó el archivo `vercel.json` o `_redirects` en React para forzar la carga del `index.html`.

## 4. Resolución de problemas comunes (Fase 4)

| Síntoma | Causa probable |
|---|---|
| El Dashboard está en blanco y la consola dice `Cannot read properties of undefined (reading 'map')` | El componente de React intentó renderizar el gráfico antes de que la petición HTTP terminara. Agrega validaciones de carga (Loading state) o chequeos de nulidad (ej. `datos && datos.map(...)`). |
| Error `500` en Render pero en `localhost` funciona perfecto | La variable de entorno con la cadena de conexión a Supabase o el Secreto del JWT no se configuró en el panel de **Environment Variables** de Render, o su formato es incorrecto. |
| La consulta LINQ devuelve un error de traducción SQL | Estás usando métodos puros de C# dentro del `.Select()` (como `.ToString()` para formatos de fecha) que PostgreSQL no entiende. Haz esas conversiones en memoria DESPUÉS de hacer `.ToListAsync()`. |