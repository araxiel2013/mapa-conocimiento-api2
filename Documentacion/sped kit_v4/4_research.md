# Investigación y decisiones — Fase 4 (Analítica y Despliegue)

> **Fase 4** · **Lectura opcional**. Funciona como registro de decisiones de arquitectura (**ADR**) para los retos específicos de la construcción de métricas y la publicación en la nube con un presupuesto de cero pesos ($0). Complementa a `3_plan_fase4.md`.

---

## D1 — Plataforma de Despliegue Backend (.NET 10 API)

**Contexto:** Se requiere alojar la API de C# en un servidor accesible públicamente sin costo, que soporte HTTPS y pueda conectarse a Supabase.
**Opciones evaluadas:** 
(a) Azure App Service (Free Tier F1).
(b) **Render (Free Web Service).**
(c) Heroku (Sin capa gratuita actualmente).
(d) AWS Elastic Beanstalk (Requiere tarjeta de crédito y configuración compleja).
**Decisión: (b) Render (Render.com).**
**Por qué:** Aunque Azure es nativo para .NET, su capa gratuita a veces es inestable para proyectos académicos que requieren compilaciones desde GitHub Actions y, a menudo, exige la creación de cuentas de facturación complejas. Render ofrece una capa gratuita excelente ("Free Web Services") que compila y despliega directamente desde un repositorio de GitHub utilizando un archivo `Dockerfile`. La única desventaja es que el servicio "se duerme" tras 15 minutos de inactividad y tarda unos 50 segundos en despertar (Cold Start), lo cual es totalmente aceptable para el alcance de este proyecto universitario.

## D2 — Plataforma de Despliegue Frontend (React/Vite)

**Contexto:** Se requiere alojar los archivos estáticos de la Single Page Application (HTML, CSS, JS generados por Vite).
**Opciones evaluadas:** 
(a) GitHub Pages.
(b) **Vercel.**
(c) Netlify.
**Decisión: (b) Vercel (o equivalentemente Netlify).**
**Por qué:** GitHub Pages es excelente pero la configuración del enrutamiento de React (SPA routing, cuando el usuario recarga la página) suele ser un dolor de cabeza (arroja error 404 en sub-rutas). Vercel, al ser los creadores de Next.js, tienen optimización nativa para proyectos de Vite/React. Solo requiere conectar el repositorio de GitHub y crear un archivo `vercel.json` de dos líneas para asegurar que todas las rutas caigan en el `index.html`. Además, ofrece un subdominio gratuito con SSL automático (ej. `mi-mapa-conocimiento.vercel.app`).

## D3 — Biblioteca de Visualización de Datos (Dashboard)

**Contexto:** El requerimiento exige renderizar gráficas visualmente atractivas en React a partir de los JSONs generados por la API.
**Opciones evaluadas:** 
(a) D3.js.
(b) Chart.js (vía `react-chartjs-2`).
(c) **Recharts.**
**Decisión: (c) Recharts.**
**Por qué:** D3.js es extremadamente poderoso pero su curva de aprendizaje es demasiado alta para el tiempo disponible en el proyecto. Chart.js es muy bueno, pero requiere manipulación del Canvas y a veces su integración con los ciclos de vida de React es tosca. **Recharts**