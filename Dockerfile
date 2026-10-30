# ==============================================================================
# Dockerfile Multi-stage para ASP.NET Core 10 - Despliegue en Render (Web Service)
# ==============================================================================

# ------------------------------------------------------------------------------
# Fase 1: Compilación y Publicación (SDK)
# ------------------------------------------------------------------------------
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copiar archivo de proyecto y restaurar paquetes NuGet (optimiza cache de capas)
COPY ["mapa_conocimiento_api.csproj", "./"]
RUN dotnet restore "mapa_conocimiento_api.csproj"

# Copiar el código fuente restante
COPY . .

# Compilar y publicar los binarios optimizados en modo Release
RUN dotnet publish "mapa_conocimiento_api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# ------------------------------------------------------------------------------
# Fase 2: Imagen Final Ligera para Producción (Runtime ASP.NET)
# ------------------------------------------------------------------------------
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

# Configuración de variables de entorno para Render y ASP.NET Core
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_FORWARDEDHEADERS_ENABLED=true

# Exponer el puerto estándar que Render detectará automáticamente
EXPOSE 8080

# Copiar los archivos compilados desde la etapa build
COPY --from=build /app/publish .

# Comando de inicio del servicio
ENTRYPOINT ["dotnet", "mapa_conocimiento_api.dll"]

