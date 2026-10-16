# Quickstart — Fase 2: arranque y smoke test

> **Fase 2** · Validación rápida de la integridad referencial y las tablas puente. Si aún no hay código construido para esta fase, empiece por `8_tasks_fase2.md`.

---

## 1. Arranque del entorno de desarrollo

El entorno se levanta exactamente igual que en la Fase 1. Asegúrese de que ambos servidores estén corriendo para poder probar los selectores dinámicos.

### Backend (C# API)
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

## 2. Smoke test (Prueba de API - Criterios de Fase 2)

Puede usar `curl`, Postman o Swagger (`http://localhost:5000/swagger`). Estas pruebas asumen que ya existen registros en las tablas de la Fase 1 (ej. un `tipo_producto` con ID 2 y un `proyecto` con ID 10).

```powershell
# 1. Validar Integridad Referencial (Rechazo) - POST con FK inválida
# Intentamos crear un producto apuntando a un tipo de producto que NO existe (9999).
curl.exe -i -X POST http://localhost:5000/api/Producto -H "Content-Type: application/json" -d "{\"nombre\": \"Software X\", \"categoria\": \"App\", \"fecha_entrega\": \"2026-05-15\", \"proyecto_id\": 10, \"tipo_producto_id\": 9999}"
# → DEBE responder 400 Bad Request con mensaje de error de FK, NO un error 500 del servidor.

# 2. POST exitoso con FK válida
# Asumiendo que el tipo_producto 2 sí existe.
curl.exe -X POST http://localhost:5000/api/Producto -H "Content-Type: application/json" -d "{\"nombre\": \"Software X\", \"categoria\": \"App\", \"fecha_entrega\": \"2026-05-15\", \"proyecto_id\": 10, \"tipo_producto_id\": 2}"
# → DEBE responder 201 Created.

# 3. Validar Eager Loading (Include) - GET con anidamiento
curl.exe http://localhost:5000/api/Producto
# → En el JSON de respuesta, además de "proyecto_id", debe venir el objeto "tipo_producto_relacionado" con sus detalles (nombre, categoría, etc.).

# 4. Prueba de Tabla Puente - POST
# Asignar el docente 10102020 al proyecto 10
curl.exe -X POST http://localhost:5000/api/Desarrolla -H "Content-Type: application/json" -d "{\"docente_cedula\": 10102020, \"proyecto_id\": 10, \"rol\": \"Investigador\", \"descripcion\": \"Apoyo general\"}"

# 5. Prueba de Tabla Puente - DELETE Lógico
curl.exe -X DELETE "http://localhost:5000/api/Desarrolla?docenteCedula=10102020&proyectoId=10"
# → DEBE responder 200 OK y actualizar activo = false en Supabase.
```

## 3. Smoke test (Prueba de Frontend - React)

1. **Navegue a la vista "Crear Docente".**
2. **Abra las Herramientas de Desarrollador (F12) -> Pestaña "Network" (Red).**
3. Verifique que, al cargar la página, se dispara automáticamente una petición `GET /api/LineaInvestigacion`.
4. Haga clic en el campo desplegable "Línea de investigación principal".
5. Confirme que las opciones mostradas corresponden **exactamente** a las líneas activas en su base de datos de Supabase, y no a datos quemados (hardcodeados) en el código de React.

## 4. Resolución de problemas comunes de la Fase 2

| Síntoma | Causa probable |
|---|---|
| API responde `500` con error de "Referencia Circular" (Reference Cycle) al hacer GET | Faltó configurar `ReferenceHandler.IgnoreCycles` en `Program.cs` o no se usó un Response DTO para limpiar la salida del `.Include()`. |
| El GET de un Docente devuelve `LineaInvestigacionRelacionada: null` pero el ID sí está | Faltó agregar `.Include(d => d.LineaInvestigacionRelacionada)` en el método `ObtenerTodosAsync` del Repositorio. |
| El frontend no muestra datos en el `<select>` | El endpoint maestro devolvió un objeto (ej. `{ tabla, total, datos }`) y en React se intentó mapear el objeto raíz en lugar de `respuesta.datos`. |
| API responde `500` en lugar de `400` al enviar una FK inválida | El Servicio o Repositorio no validó la existencia de la FK antes de hacer `_context.SaveChangesAsync()`. El error reventó directo en Supabase. |