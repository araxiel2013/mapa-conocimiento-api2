using Microsoft.EntityFrameworkCore;
using mapa_conocimiento_api.Models;
using mapa_conocimiento_api.Repositorios;
using mapa_conocimiento_api.Servicios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Soporte para puerto dinámico asignado por Render u otros proveedores cloud (PORT)
var portEnv = Environment.GetEnvironmentVariable("PORT");
if (!string.IsNullOrEmpty(portEnv))
{
    builder.WebHost.UseUrls($"http://+:{portEnv}");
}

// Agregar soporte para controladores
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Evita ciclos infinitos al serializar JSON en Eager Loading (Fase 2)
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

// Configuración de Swagger para documentar la API
builder.Services.AddEndpointsApiExplorer();
// --- CONFIGURACIÓN SWAGGER CON CANDADO (FASE 3) ---
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Mapa de Conocimiento API", Version = "v1" });
    
    // Definimos el esquema de seguridad para Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header usando el esquema Bearer. \r\n\r\n Escribe 'Bearer' [espacio] y luego tu token.\r\n\r\nEjemplo: \"Bearer 12345abcdef\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

// --- CONFIGURACIÓN DE AUTENTICACIÓN JWT (FASE 3 / PRODUCCIÓN) ---
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = builder.Configuration["JwtSettings:Key"]
    ?? builder.Configuration["JwtSettings__Key"]
    ?? builder.Configuration["JWT_SECRET_KEY"]
    ?? jwtSettings.GetValue<string>("Key")
    ?? "ClavePorDefectoSuperSeguraParaDesarrolloLocalMinimo32Caracteres!";

var jwtIssuer = builder.Configuration["JwtSettings:Issuer"]
    ?? builder.Configuration["JwtSettings__Issuer"]
    ?? jwtSettings.GetValue<string>("Issuer")
    ?? "MapaConocimientoAPI";

var jwtAudience = builder.Configuration["JwtSettings:Audience"]
    ?? builder.Configuration["JwtSettings__Audience"]
    ?? jwtSettings.GetValue<string>("Audience")
    ?? "MapaConocimientoReactClient";

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            // Convierte el string de la clave en un arreglo de bytes para la firma criptográfica
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
        };
    });

// --- CONFIGURACIÓN DEL DBCONTEXT CON POSTGRESQL (SUPABASE / PRODUCCIÓN) ---
var supabaseConnection = builder.Configuration.GetConnectionString("SupabaseConnection")
    ?? builder.Configuration.GetConnectionString("Supabase")
    ?? builder.Configuration["ConnectionStrings__Supabase"]
    ?? builder.Configuration["ConnectionStrings:Supabase"]
    ?? builder.Configuration["ConnectionStrings__SupabaseConnection"]
    ?? builder.Configuration["ConnectionStrings:SupabaseConnection"]
    ?? builder.Configuration["DATABASE_URL"]
    ?? builder.Configuration["SUPABASE_CONNECTION_STRING"];

builder.Services.AddDbContext<postgresContext>(options =>
    options.UseNpgsql(supabaseConnection));

// ====================================================================
// REGISTRO DE INYECCIÓN DE DEPENDENCIAS (Repositorios y Servicios)
// ====================================================================

// 1. Área de Conocimiento
builder.Services.AddScoped<IAreaConocimientoRepository, AreaConocimientoRepository>();
builder.Services.AddScoped<IAreaConocimientoService, AreaConocimientoService>();

// 2. Objetivos de Desarrollo Sostenible (ODS)
builder.Services.AddScoped<IObjetivoDesarrolloSostenibleRepository, ObjetivoDesarrolloSostenibleRepository>();
builder.Services.AddScoped<IObjetivoDesarrolloSostenibleService, ObjetivoDesarrolloSostenibleService>();

// 3. Área de Aplicación
builder.Services.AddScoped<IAreaAplicacionRepository, AreaAplicacionRepository>();
builder.Services.AddScoped<IAreaAplicacionService, AreaAplicacionService>();

// 4. Términos Clave
builder.Services.AddScoped<ITerminoClaveRepository, TerminoClaveRepository>();
builder.Services.AddScoped<ITerminoClaveService, TerminoClaveService>();

// 5. Líneas de Investigación
builder.Services.AddScoped<ILineaInvestigacionRepository, LineaInvestigacionRepository>();
builder.Services.AddScoped<ILineaInvestigacionService, LineaInvestigacionService>();

// 6. Aliados
builder.Services.AddScoped<IAliadoRepository, AliadoRepository>();
builder.Services.AddScoped<IAliadoService, AliadoService>();

// 7. Proyecto
builder.Services.AddScoped<IProyectoRepository, ProyectoRepository>();
builder.Services.AddScoped<IProyectoService, ProyectoService>();

// 8. Tipo de Producto
builder.Services.AddScoped<ITipoProductoRepository, TipoProductoRepository>();
builder.Services.AddScoped<ITipoProductoService, TipoProductoService>();

// ====================================================================
// INYECCIÓN DE DEPENDENCIAS - FASE 2
// ====================================================================

// Tabla: docente (Entidad fuerte)
builder.Services.AddScoped<IDocenteRepository, DocenteRepository>();
builder.Services.AddScoped<IDocenteService, DocenteService>();

// Tabla: producto (Entidad fuerte)
builder.Services.AddScoped<IProductoRepository, ProductoRepository>();
builder.Services.AddScoped<IProductoService, ProductoService>();

// Tabla puente: aliado_proyecto (Relación Aliado - Proyecto)
builder.Services.AddScoped<IAliadoProyectoRepository, AliadoProyectoRepository>();
builder.Services.AddScoped<IAliadoProyectoService, AliadoProyectoService>();

// Tabla puente: desarrolla (Relación Docente - Proyecto)
builder.Services.AddScoped<IDesarrollaRepository, DesarrollaRepository>();
builder.Services.AddScoped<IDesarrollaService, DesarrollaService>();

// Tabla puente: palabras_clave (Relación Proyecto - Término Clave)
builder.Services.AddScoped<IPalabrasClaveRepository, PalabrasClaveRepository>();
builder.Services.AddScoped<IPalabrasClaveService, PalabrasClaveService>();

// Tabla puente: ac_proyecto (Relación Área de Conocimiento - Proyecto)
builder.Services.AddScoped<IAcProyectoRepository, AcProyectoRepository>();
builder.Services.AddScoped<IAcProyectoService, AcProyectoService>();

// Tabla puente: proyecto_linea (Relación Proyecto - Línea de Investigación)
builder.Services.AddScoped<IProyectoLineaRepository, ProyectoLineaRepository>();
builder.Services.AddScoped<IProyectoLineaService, ProyectoLineaService>();

// Tabla puente: ods_proyecto (Relación ODS - Proyecto)
builder.Services.AddScoped<IOdsProyectoRepository, OdsProyectoRepository>();
builder.Services.AddScoped<IOdsProyectoService, OdsProyectoService>();

// Tabla puente: aa_proyecto (Relación Área de Aplicación - Proyecto)
builder.Services.AddScoped<IAaProyectoRepository, AaProyectoRepository>();
builder.Services.AddScoped<IAaProyectoService, AaProyectoService>();

// Tabla puente: docente_producto (Relación Docente - Producto)
builder.Services.AddScoped<IDocenteProductoRepository, DocenteProductoRepository>();
builder.Services.AddScoped<IDocenteProductoService, DocenteProductoService>();

// ====================================================================
// INYECCIÓN DE DEPENDENCIAS - FASE 3
// ====================================================================
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();

// ====================================================================
// INYECCIÓN DE DEPENDENCIAS - FASE 4 (Reportes y Analítica)
// ====================================================================
builder.Services.AddScoped<IReportesRepository, ReportesRepository>();
builder.Services.AddScoped<IReportesService, ReportesService>();

// ====================================================================
// CONFIGURACIÓN DE CORS - FASE 3 / SEGURIDAD Y PRODUCCIÓN (Render & Vercel)
// ====================================================================
var allowedOriginsSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
{
    "http://localhost:5173",
    "http://localhost:3000",
    "https://localhost:5173",
    "https://localhost:3000"
};

// 1. Orígenes definidos como array en appsettings.json (CorsOrigins)
var corsConfigArray = builder.Configuration.GetSection("CorsOrigins").Get<string[]>();
if (corsConfigArray != null)
{
    foreach (var origin in corsConfigArray)
    {
        if (!string.IsNullOrWhiteSpace(origin))
            allowedOriginsSet.Add(origin.Trim().TrimEnd('/'));
    }
}

// 2. Orígenes definidos como variable de entorno (ALLOWED_ORIGINS o CorsOrigins)
var envAllowedOrigins = builder.Configuration["ALLOWED_ORIGINS"] 
    ?? builder.Configuration["CorsOrigins"]
    ?? Environment.GetEnvironmentVariable("ALLOWED_ORIGINS")
    ?? Environment.GetEnvironmentVariable("CorsOrigins");

if (!string.IsNullOrWhiteSpace(envAllowedOrigins))
{
    var split = envAllowedOrigins.Split(new[] { ',', ';', ' ' }, StringSplitOptions.RemoveEmptyEntries);
    foreach (var origin in split)
    {
        if (!string.IsNullOrWhiteSpace(origin))
            allowedOriginsSet.Add(origin.Trim().TrimEnd('/'));
    }
}

builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirFrontend", policy =>
    {
        policy.SetIsOriginAllowed(origin =>
        {
            if (string.IsNullOrWhiteSpace(origin)) return false;
            var cleanOrigin = origin.TrimEnd('/');
            
            // Permite los orígenes configurados explícitamente (localhost, dominios de producción)
            if (allowedOriginsSet.Contains(cleanOrigin)) return true;
            
            // Permite dinámicamente cualquier subdominio de Vercel (*.vercel.app)
            if (Uri.TryCreate(origin, UriKind.Absolute, out var uri) && 
                (uri.Host.EndsWith(".vercel.app", StringComparison.OrdinalIgnoreCase) || 
                 uri.Host.Equals("vercel.app", StringComparison.OrdinalIgnoreCase)))
            {
                return true;
            }

            return false;
        })
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var app = builder.Build();

// ====================================================================
// MANEJO GLOBAL DE EXCEPCIONES (MIDDLEWARE) - BE-11
// ====================================================================
app.UseExceptionHandler(exceptionHandlerApp =>
{
    exceptionHandlerApp.Run(async context =>
    {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/json";

        var exceptionHandlerPathFeature = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerPathFeature>();
        var exception = exceptionHandlerPathFeature?.Error;

        var logger = context.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger("GlobalExceptionHandler");
        if (exception != null)
        {
            logger.LogError(exception, "Excepción no controlada en la ruta {Path}: {Message}", exceptionHandlerPathFeature?.Path, exception.Message);
        }

        var errorResponse = new
        {
            estado = 500,
            mensaje = "Error interno en el servidor."
        };

        await context.Response.WriteAsJsonAsync(errorResponse);
    });
});

// Soporte para encabezados reenviados (X-Forwarded-For, X-Forwarded-Proto) detrás del proxy inverso de Render
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

// Swagger habilitado tanto en Development como en Production (exigido por 7_quickstart.md para validar Cold Start en Render)
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mapa de Conocimiento API v1");
    c.RoutePrefix = "swagger";
});

// Redirección HTTPS solo en desarrollo local (Render maneja la terminación TLS/SSL externamente)
if (!app.Environment.IsProduction())
{
    app.UseHttpsRedirection();
}

app.UseCors("PermitirFrontend");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// Redirigir la raíz directamente a la documentación de Swagger
app.MapGet("/", () => Results.Redirect("/swagger"));

app.Run();
