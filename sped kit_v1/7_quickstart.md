# Quickstart — Fase 1: arranque y smoke test

> **Fase 1** · Validación rápida de la versión ya construida. Si aún no hay nada construido, empiece por `8_tasks.md`.

---

## 1. Arranque del entorno de desarrollo

Dado que el Backend y el Frontend son proyectos separados y la base de datos está en la nube (Supabase), el arranque requiere levantar ambos servicios localmente.

### Paso 1: Configurar Supabase
Asegúrese de haber ejecutado el script SQL inicial (`bdfacturas_postgres.sql` adaptado a mapa_conocimiento) en el panel de su proyecto en Supabase y de tener la cadena de conexión en el `appsettings.json` del backend.

### Paso 2: Levantar la API (C#)
Abra una terminal, navegue a la carpeta del backend y ejecute:
```powershell
cd mapa_conocimiento_api
dotnet watch
```
*(La API levantará, por ejemplo, en `http://localhost:5000`. `dotnet watch` recompilará automáticamente si guarda un archivo `.cs`).*

### Paso 3: Levantar el Frontend (React)
Abra una segunda terminal, navegue a la carpeta del frontend y ejecute:
```powershell
cd mapa_conocimiento_client
npm run dev
```
*(El frontend levantará, por ejemplo, en `http://localhost:5173`).*

## 2. Smoke test (Prueba de los criterios de aceptación)

Puede ejecutar estas pruebas con `curl` en la terminal o usando herramientas como Postman / Swagger UI (`http://localhost:5000/swagger`).

```powershell
# 1. Diagnóstico / Swagger
# Verifique que la documentación de Swagger cargue correctamente en el navegador.

# 2. Listar registros (GET) - Ejemplo con AreaConocimiento
curl.exe http://localhost:5000/api/AreaConocimiento

# 3. Obtener un registro específico (GET por ID)
curl.exe http://localhost:5000/api/AreaConocimiento/1

# 4. Crear un registro (POST) - Ejemplo con TipoProducto
curl.exe -X POST http://localhost:5000/api/TipoProducto -H "Content-Type: application/json" -d "{\"id\": 99, \"categoria\": \"Investigación\", \"clase\": \"Artículos\", \"nombre\": \"Artículo Q1\", \"tipologia\": \"A1\"}"

# 5. Actualizar un registro (PUT)
curl.exe -X PUT http://localhost:5000/api/TipoProducto/99 -H "Content-Type: application/json" -d "{\"categoria\": \"Investigación\", \"clase\": \"Artículos\", \"nombre\": \"Artículo Q1 Revisado\", \"tipologia\": \"A1\"}"

# 6. Validar la frontera de la petición (DTO) - POST inválido (Falta 'nombre')
curl.exe -i -X POST http://localhost:5000/api/TipoProducto -H "Content-Type: application/json" -d "{\"id\": 100, \"categoria\": \"Investigación\"}" 
# → Debe responder 400 o 422 con los errores de validación.

# 7. Borrado Lógico (DELETE)
curl.exe -X DELETE http://localhost:5000/api/TipoProducto/99
# → Debe responder 200 indicando éxito.
# Si vuelve a ejecutar el GET Listar (Paso 2), el registro 99 ya no debe aparecer (gracias al filtro de EF Core).
```

## 3. Resolución de problemas comunes

| Síntoma | Causa probable |
|---|---|
| La API responde 500 en todo | La conexión a Supabase falló. Verifique la cadena de conexión en `appsettings.json` o si su IP está bloqueada en Supabase. |
| React muestra error de CORS al hacer fetch | Faltó configurar la política de CORS en `Program.cs` del backend para permitir el origen `http://localhost:5173`. |
| El GET devuelve campos vacíos | Los nombres de las propiedades en los modelos de C# no coinciden exactamente con las columnas de Supabase. Verifique mayúsculas/minúsculas. |
| Guardo un `.cs` y no pasa nada | Verifique que levantó la API usando `dotnet watch` y no solo `dotnet run`. |