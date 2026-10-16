using Microsoft.EntityFrameworkCore;
using mapa_conocimiento_api.Models;
using mapa_conocimiento_api.Repositorios;
using mapa_conocimiento_api.Servicios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
    
var builder = WebApplication.CreateBuilder(args);

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

// --- CONFIGURACIÓN DE AUTENTICACIÓN JWT (FASE 3) ---
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings.GetValue<string>("Key");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.GetValue<string>("Issuer"),
            ValidAudience = jwtSettings.GetValue<string>("Audience"),
            // Convierte el string de la clave en un arreglo de bytes para la firma criptográfica
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!))
        };
    });

// Configuración del DbContext con PostgreSQL (Supabase)
builder.Services.AddDbContext<postgresContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("SupabaseConnection")));

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
// CONFIGURACIÓN DE CORS - FASE 3 / SEGURIDAD
// ====================================================================
var corsOriginsConfigurados = builder.Configuration.GetSection("CorsOrigins").Get<string[]>();
var allowedOrigins = (corsOriginsConfigurados != null && corsOriginsConfigurados.Length > 0)
    ? corsOriginsConfigurados
    : new[] { "http://localhost:5173", "http://localhost:3000" };

builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirFrontend", policy =>
    {
        policy.WithOrigins(allowedOrigins)
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configuración del pipeline de peticiones HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("PermitirFrontend");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// Redirigir la raíz directamente a la documentación de Swagger
app.MapGet("/", () => Results.Redirect("/swagger"));

app.Run();