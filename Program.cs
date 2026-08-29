using Microsoft.EntityFrameworkCore;
using mapa_conocimiento_api.Models;
using mapa_conocimiento_api.Repositorios;
using mapa_conocimiento_api.Servicios;

var builder = WebApplication.CreateBuilder(args);

// Agregar soporte para controladores
builder.Services.AddControllers();

// Configuración de Swagger para documentar la API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

// ====================================================================

var app = builder.Build();

// Configuración del pipeline de peticiones HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();