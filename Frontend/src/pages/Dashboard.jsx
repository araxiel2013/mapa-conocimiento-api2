import React, { useState, useEffect, useCallback } from 'react';
import LayoutFormulario from '../components/layout/LayoutFormulario';
import { 
  ChevronDown, 
  ChevronRight, 
  BarChart3, 
  Database, 
  FolderKanban, 
  Users, 
  FileCheck2, 
  Coins, 
  RotateCw, 
  TrendingUp, 
  PieChart as PieIcon,
  Sparkles
} from 'lucide-react';
import api from '../api/axiosConfig';

// Componentes Genéricos de Reportes
import TarjetaKpi from '../components/reportes/TarjetaKpi';
import GraficoBarrasGenerico from '../components/reportes/GraficoBarrasGenerico';
import GraficoTortaGenerico from '../components/reportes/GraficoTortaGenerico';
import GraficoLineasGenerico from '../components/reportes/GraficoLineasGenerico';

// Formularios Transaccionales (Fases 1 y 2)
import FormAreaConocimiento from '../components/formularios/FormAreaConocimiento';
import FormObjetivoDesarrolloSostenible from '../components/formularios/FormObjetivoDesarrolloSostenible';
import FormAreaAplicacion from '../components/formularios/FormAreaAplicacion';
import FormTerminoClave from '../components/formularios/FormTerminoClave';
import FormLineaInvestigacion from '../components/formularios/FormLineaInvestigacion';
import FormAliado from '../components/formularios/FormAliado';
import FormProyecto from '../components/formularios/FormProyecto';
import FormTipoProducto from '../components/formularios/FormTipoProducto';
import FormDocente from '../components/formularios/FormDocente';
import FormProducto from '../components/formularios/FormProducto';
import FormTablasPuente from '../components/formularios/FormTablasPuente';

const modulosFormularios = [
  { id: 'proyecto', label: 'Datos Básicos (Proyecto)' },
  { id: 'areaConocimiento', label: 'Área de Conocimiento' },
  { id: 'ods', label: 'Objetivos (ODS)' },
  { id: 'areaAplicacion', label: 'Área de Aplicación' },
  { id: 'terminoClave', label: 'Términos Clave' },
  { id: 'lineaInvestigacion', label: 'Líneas de Investigación' },
  { id: 'tipoProducto', label: 'Tipos de Producto' },
  { id: 'aliado', label: 'Aliados' },
  { id: 'docente', label: 'Docentes (Fase 2)' },
  { id: 'producto', label: 'Productos (Fase 2)' },
  { id: 'tablasPuente', label: 'Tablas Puente / Asociaciones' },
];

export default function Dashboard() {
  // Pestaña activa principal: 'reportes' o 'formularios'
  const [pestanaPrincipal, setPestanaPrincipal] = useState('reportes');

  // Submódulo activo en formularios CRUD
  const [formularioActivo, setFormularioActivo] = useState('proyecto');

  // Estados de datos de Analítica (Fase 4.4)
  const [kpis, setKpis] = useState({
    total_proyectos_activos: 0,
    total_docentes_investigadores: 0,
    presupuesto_total_historico: 0,
    total_productos_entregados: 0
  });

  const [proyectosPorGranArea, setProyectosPorGranArea] = useState([]);
  const [productosPorOds, setProductosPorOds] = useState([]);
  const [presupuestoFinanciacion, setPresupuestoFinanciacion] = useState([]);
  const [proyectosPorLinea, setProyectosPorLinea] = useState([]);

  const [cargandoReportes, setCargandoReportes] = useState(false);
  const [errorReportes, setErrorReportes] = useState('');

  // =========================================================
  // CONSULTA DE REPORTES Y ANALÍTICA (Fase 4.4)
  // =========================================================
  const cargarMetricas = useCallback(async () => {
    setCargandoReportes(true);
    setErrorReportes('');

    try {
      // Usamos allSettled para que si un endpoint específico de BD falla o viene vacío,
      // los demás reportes se rendericen sin bloquear la interfaz (Regla Anti-Crash)
      const resultados = await Promise.allSettled([
        api.get('/Reportes/kpis-generales'),
        api.get('/Reportes/proyectos-por-gran-area'),
        api.get('/Reportes/productos-por-ods'),
        api.get('/Reportes/presupuesto-por-financiacion'),
        api.get('/Reportes/proyectos-por-linea')
      ]);

      // 1. KPIs Generales
      if (resultados[0].status === 'fulfilled' && resultados[0].value?.data) {
        setKpis(resultados[0].value.data);
      }

      // 2. Gráfico Barras: Gran Área
      if (resultados[1].status === 'fulfilled' && Array.isArray(resultados[1].value?.data)) {
        setProyectosPorGranArea(resultados[1].value.data);
      }

      // 3. Gráfico Barras Horizontales: Productos por ODS
      if (resultados[2].status === 'fulfilled' && Array.isArray(resultados[2].value?.data)) {
        setProductosPorOds(resultados[2].value.data);
      }

      // 4. Gráfico Torta / Donut: Presupuesto por Financiación
      if (resultados[3].status === 'fulfilled' && Array.isArray(resultados[3].value?.data)) {
        setPresupuestoFinanciacion(resultados[3].value.data);
      }

      // 5. Gráfico Líneas / Barras: Proyectos por Línea
      if (resultados[4].status === 'fulfilled' && Array.isArray(resultados[4].value?.data)) {
        setProyectosPorLinea(resultados[4].value.data);
      }

    } catch (err) {
      setErrorReportes('Hubo una incidencia al sincronizar algunos reportes analíticos.');
    } finally {
      setCargandoReportes(false);
    }
  }, []);

  useEffect(() => {
    if (pestanaPrincipal === 'reportes') {
      cargarMetricas();
    }
  }, [pestanaPrincipal, cargarMetricas]);

  // =========================================================
  // RENDERIZADO DE FORMULARIOS CRUD (Fases 1 y 2)
  // =========================================================
  const renderizarFormulario = () => {
    switch (formularioActivo) {
      case 'proyecto': return <FormProyecto />;
      case 'areaConocimiento': return <FormAreaConocimiento />;
      case 'ods': return <FormObjetivoDesarrolloSostenible />;
      case 'areaAplicacion': return <FormAreaAplicacion />;
      case 'terminoClave': return <FormTerminoClave />;
      case 'lineaInvestigacion': return <FormLineaInvestigacion />;
      case 'tipoProducto': return <FormTipoProducto />;
      case 'aliado': return <FormAliado />;
      case 'docente': return <FormDocente />;
      case 'producto': return <FormProducto />;
      case 'tablasPuente': return <FormTablasPuente />;
      default: return <div className="text-xl">Seleccione un módulo</div>;
    }
  };

  const menuNavegacionFormularios = (
    <div className="flex flex-col text-[18px] font-bold text-white w-full h-full border-r border-gris-2">
      {modulosFormularios.map((mod) => {
        const isActivo = formularioActivo === mod.id;
        return (
          <div key={mod.id} className="w-full">
            <button
              onClick={() => setFormularioActivo(mod.id)}
              className={`w-full flex items-center justify-between px-6 py-3.5 transition-all duration-200 cursor-pointer text-left ${
                isActivo 
                  ? 'bg-white/15 text-azul-1' 
                  : 'text-slate-300 hover:bg-white/5'
              }`}
            >
              <span className={isActivo ? 'text-azul-1' : 'text-slate-300'}>{mod.label}</span>
              {isActivo ? <ChevronDown size={18} className="text-azul-1" /> : <ChevronRight size={18} className="text-slate-400" />}
            </button>
            {isActivo && (
              <div className="bg-white/10 px-6 py-1.5 text-xs text-slate-300 font-normal flex flex-col gap-1">
                <span className="text-white opacity-80 pl-2">Información requerida</span>
              </div>
            )}
          </div>
        );
      })}
    </div>
  );

  return (
    <div className="min-h-screen flex flex-col bg-slate-100">
      {/* =========================================================
          BARRA DE CONMUTACIÓN DE PESTAÑAS PRINCIPALES
      ========================================================= */}
      <div className="bg-[#0D274C] text-white border-b border-slate-700/60 shadow-xs">
        <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
          <div className="flex items-center justify-between h-16">
            <div className="flex items-center gap-2 sm:gap-4">
              <button
                onClick={() => setPestanaPrincipal('reportes')}
                className={`flex items-center gap-2 px-4 py-2 rounded-xl text-sm font-bold transition-all cursor-pointer ${
                  pestanaPrincipal === 'reportes'
                    ? 'bg-[#Eb8600] text-white shadow-md'
                    : 'text-slate-300 hover:bg-white/10 hover:text-white'
                }`}
              >
                <BarChart3 size={18} />
                <span>Panel Analítico (Reportes)</span>
              </button>

              <button
                onClick={() => setPestanaPrincipal('formularios')}
                className={`flex items-center gap-2 px-4 py-2 rounded-xl text-sm font-bold transition-all cursor-pointer ${
                  pestanaPrincipal === 'formularios'
                    ? 'bg-[#Eb8600] text-white shadow-md'
                    : 'text-slate-300 hover:bg-white/10 hover:text-white'
                }`}
              >
                <Database size={18} />
                <span>Gestión de Datos (Formularios CRUD)</span>
              </button>
            </div>

            {pestanaPrincipal === 'reportes' && (
              <button
                onClick={cargarMetricas}
                disabled={cargandoReportes}
                className="hidden sm:flex items-center gap-2 bg-white/10 hover:bg-white/20 text-white text-xs font-semibold px-3 py-1.5 rounded-lg border border-white/20 transition-all cursor-pointer disabled:opacity-50"
              >
                <RotateCw size={14} className={cargandoReportes ? 'animate-spin' : ''} />
                <span>{cargandoReportes ? 'Actualizando...' : 'Actualizar métricas'}</span>
              </button>
            )}
          </div>
        </div>
      </div>

      {/* =========================================================
          CONTENIDO: PANEL ANALÍTICO (FASE 4.4)
      ========================================================= */}
      {pestanaPrincipal === 'reportes' ? (
        <div className="flex-1 max-w-7xl w-full mx-auto px-4 sm:px-6 lg:px-8 py-8 space-y-8">
          
          {/* Encabezado del Panel */}
          <div className="flex flex-col sm:flex-row sm:items-center sm:justify-between gap-4 border-b border-slate-200 pb-4">
            <div>
              <div className="inline-flex items-center gap-1.5 text-xs font-bold uppercase tracking-wider text-[#Eb8600] mb-1">
                <Sparkles size={14} />
                <span>Inteligencia de Datos · USB Colombia</span>
              </div>
              <h1 className="text-2xl sm:text-3xl font-black text-[#0D274C]">
                Tablero Gerencial de Investigación y Producción Científica
              </h1>
              <p className="text-xs sm:text-sm text-slate-500 mt-1">
                Métricas agregadas en tiempo real de proyectos, productos, docentes y su vinculación con los ODS.
              </p>
            </div>

            <button
              onClick={cargarMetricas}
              disabled={cargandoReportes}
              className="sm:hidden flex items-center justify-center gap-2 bg-[#0D274C] text-white text-xs font-bold px-4 py-2.5 rounded-xl shadow-xs"
            >
              <RotateCw size={14} className={cargandoReportes ? 'animate-spin' : ''} />
              <span>Actualizar datos</span>
            </button>
          </div>

          {/* Feedback de Carga */}
          {cargandoReportes && (
            <div className="p-4 rounded-xl bg-amber-50 border border-amber-200 text-amber-900 text-xs sm:text-sm flex items-center gap-3 animate-pulse">
              <RotateCw size={16} className="animate-spin text-[#Eb8600]" />
              <span>Sincronizando métricas institucionales con Supabase...</span>
            </div>
          )}

          {errorReportes && (
            <div className="p-4 rounded-xl bg-rose-50 border border-rose-200 text-rose-800 text-xs sm:text-sm">
              {errorReportes}
            </div>
          )}

          {/* CUADRÍCULA DE TARJETAS KPIS */}
          <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-5">
            <TarjetaKpi
              titulo="Proyectos Activos"
              valor={kpis.total_proyectos_activos}
              icono={FolderKanban}
              color="naranja"
              descripcion="Proyectos institucionales en ejecución"
            />
            <TarjetaKpi
              titulo="Docentes Investigadores"
              valor={kpis.total_docentes_investigadores}
              icono={Users}
              color="azul"
              descripcion="Profesores con CvLAC registrado"
            />
            <TarjetaKpi
              titulo="Presupuesto Histórico"
              valor={kpis.presupuesto_total_historico}
              icono={Coins}
              color="verde"
              esMoneda={true}
              descripcion="Fondos asignados a investigación"
            />
            <TarjetaKpi
              titulo="Productos Entregados"
              valor={kpis.total_productos_entregados}
              icono={FileCheck2}
              color="morado"
              descripcion="Artículos, software y patentes"
            />
          </div>

          {/* CUADRÍCULA DE GRÁFICOS ANALÍTICOS */}
          <div className="grid grid-cols-1 lg:grid-cols-2 gap-6">
            {/* Gráfico 1: Barras Verticales (Gran Área OCDE) */}
            <GraficoBarrasGenerico
              titulo="1. Proyectos por Gran Área del Conocimiento (OCDE)"
              descripcion="Distribución de investigaciones activas según las áreas Frascati / OCDE"
              datos={proyectosPorGranArea}
              colorBarra="#Eb8600"
              altura={340}
            />

            {/* Gráfico 2: Torta / Donut (Presupuesto por Financiación) */}
            <GraficoTortaGenerico
              titulo="2. Presupuesto según Fuente de Financiación"
              descripcion="Proporción monetaria de recursos internos, Minciencias y entidades cooperantes"
              datos={presupuestoFinanciacion}
              esMoneda={true}
              altura={340}
            />

            {/* Gráfico 3: Barras Horizontales (Productos por ODS) */}
            <GraficoBarrasGenerico
              titulo="3. Producción Científica Vinculada a los ODS (ONU)"
              descripcion="Resultados de investigación que aportan a los Objetivos de Desarrollo Sostenible"
              datos={productosPorOds}
              horizontal={true}
              colorBarra="#0D274C"
              altura={360}
            />

            {/* Gráfico 4: Líneas / Barras (Proyectos por Línea de Investigación) */}
            <GraficoLineasGenerico
              titulo="4. Proyectos por Línea de Investigación"
              descripcion="Tendencia y volumen temático en los programas académicos bonaventurianos"
              datos={proyectosPorLinea}
              colorLinea="#10B981"
              altura={360}
            />
          </div>

        </div>
      ) : (
        /* =========================================================
            CONTENIDO: GESTIÓN DE DATOS (FORMULARIOS CRUD FASES 1 Y 2)
        ========================================================= */
        <div className="flex-1">
          <LayoutFormulario 
            menu={menuNavegacionFormularios} 
            showGuardar={true} 
            onGuardar={() => document.getElementById('btn-submit')?.click()}
          >
            <div className="bg-white border-2 border-gris-2 rounded-[8px] p-8 w-full max-w-[900px] h-full shadow-sm">
              {renderizarFormulario()}
            </div>
          </LayoutFormulario>
        </div>
      )}
    </div>
  );
}
