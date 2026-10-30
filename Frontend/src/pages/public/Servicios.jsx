import React from 'react';
import { Link } from 'react-router-dom';
import { 
  FolderKanban, 
  FileText, 
  Users, 
  Target, 
  Building2, 
  Network, 
  Sparkles, 
  ArrowRight, 
  Check, 
  ShieldAlert, 
  BarChart, 
  HelpCircle 
} from 'lucide-react';

export default function Servicios() {
  const servicios = [
    {
      icono: FolderKanban,
      titulo: '1. Gestión Integral de Proyectos',
      subtitulo: 'Entidad raíz de la investigación',
      descripcion: 'Registro y seguimiento de proyectos marco institucionales con detalle de título, resumen ejecutivo, presupuesto asignado, tipo de fondos y cronograma de ejecución.',
      detalles: [
        'Parametrización de fechas de inicio y fin',
        'Clasificación por tipo de financiamiento (interno/externo)',
        'Asociación n-aria con ODS, líneas y áreas'
      ],
      color: 'bg-blue-600',
      badge: 'Fase 1'
    },
    {
      icono: FileText,
      titulo: '2. Trazabilidad de Productos Científicos',
      subtitulo: 'Alineado al Modelo Minciencias',
      descripcion: 'Catalogación de resultados científicos (artículos de revista, software, patentes, libros, ponencias) vinculados obligatoriamente a un tipo de producto formal y a su proyecto origen.',
      detalles: [
        'Validación estricta de claves foráneas',
        'Categorías: Nuevo Conocimiento, Desarrollo Tecnológico',
        'Tipologías homologadas a convocatorias nacionales'
      ],
      color: 'bg-amber-600',
      badge: 'Fase 2'
    },
    {
      icono: Users,
      titulo: '3. Red Docente y Escalafón Investigativo',
      subtitulo: 'El talento humano bonaventuriano',
      descripcion: 'Hoja de vida académica de los profesores investigadores: categorización Minciencias, escalafón docente institucional, CvLAC actualizado y línea principal de trabajo.',
      detalles: [
        'Registro completo con cédula, correo y perfil',
        'Vinculación directa a convocatorias Minciencias',
        'Filiación institucional y nacionalidad requerida'
      ],
      color: 'bg-emerald-600',
      badge: 'Fase 2'
    },
    {
      icono: Target,
      titulo: '4. Matriz de Vinculación con ODS',
      subtitulo: 'Impacto en la Agenda 2030 ONU',
      descripcion: 'Articulación transversal de cada proyecto con los 17 Objetivos de Desarrollo Sostenible, permitiendo visibilizar la contribución institucional a metas globales de desarrollo.',
      detalles: [
        '17 Objetivos catalogados por categoría',
        'Asociaciones directas en la tabla puente `ods_proyecto`',
        'Generación de métricas de impacto socioambiental'
      ],
      color: 'bg-rose-600',
      badge: 'Fase 1 y 2'
    },
    {
      icono: Building2,
      titulo: '5. Red de Aliados y Cooperación Externa',
      subtitulo: 'Relación Universidad - Empresa - Estado',
      descripcion: 'Padrón de empresas, ONG, ministerios y universidades cooperantes identificados por su NIT para rastrear alianzas, convenios de investigación y cofinanciaciones.',
      detalles: [
        'Catálogo de aliados con datos de contacto',
        'Tabla puente `aliado_proyecto` para cofinanciaciones',
        'Soporte para convocatorias conjuntas'
      ],
      color: 'bg-purple-600',
      badge: 'Fase 1 y 2'
    },
    {
      icono: Network,
      titulo: '6. Motor de Relaciones N-arias (Tablas Puente)',
      subtitulo: 'El tejido vivo del conocimiento',
      descripcion: 'Interfaz especializada para conectar entidades sin ambigüedades: asignar docentes con roles específicos (Investigador Principal, Coinvestigador) a proyectos, palabras clave y áreas OCDE.',
      detalles: [
        'Relación `desarrolla` con rol y descripción',
        'Palabras clave indexadas a productos',
        'Áreas de aplicación socioeconómica vinculadas'
      ],
      color: 'bg-[#0D274C]',
      badge: 'Fase 2'
    }
  ];

  return (
    <div className="flex flex-col py-12 lg:py-20 bg-slate-50">
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 space-y-16">
        
        {/* =========================================================
            ENCABEZADO
        ========================================================= */}
        <div className="text-center max-w-3xl mx-auto space-y-4">
          <div className="inline-flex items-center gap-2 px-3.5 py-1.5 rounded-full bg-amber-100 text-[#Eb8600] text-xs font-bold uppercase tracking-wider">
            <Sparkles size={14} />
            <span>Capacidades del Sistema</span>
          </div>
          <h1 className="text-3xl sm:text-5xl font-black text-[#0D274C] tracking-tight">
            Módulos y Servicios del Ecosistema
          </h1>
          <p className="text-base sm:text-lg text-slate-600 leading-relaxed">
            Una plataforma modular diseñada para resolver la fragmentación de la información científica, 
            garantizando integridad referencial y trazabilidad de extremo a extremo.
          </p>
        </div>

        {/* =========================================================
            GRILLA DE SERVICIOS
        ========================================================= */}
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-8">
          {servicios.map((serv, index) => {
            const Icono = serv.icono;
            return (
              <div
                key={index}
                className="bg-white rounded-3xl p-8 border border-slate-200 shadow-sm hover:shadow-xl transition-all duration-300 flex flex-col justify-between group hover:-translate-y-1"
              >
                <div>
                  <div className="flex items-center justify-between mb-6">
                    <div className={`w-14 h-14 rounded-2xl ${serv.color} text-white flex items-center justify-center shadow-md group-hover:scale-105 transition-transform`}>
                      <Icono size={28} />
                    </div>
                    <span className="text-xs font-extrabold uppercase px-3 py-1 rounded-full bg-slate-100 text-slate-700 border border-slate-200">
                      {serv.badge}
                    </span>
                  </div>

                  <h3 className="text-xl font-bold text-[#0D274C] mb-1">
                    {serv.titulo}
                  </h3>
                  <div className="text-xs font-semibold text-[#Eb8600] uppercase tracking-wider mb-4">
                    {serv.subtitulo}
                  </div>

                  <p className="text-sm text-slate-600 leading-relaxed mb-6">
                    {serv.descripcion}
                  </p>

                  <div className="space-y-2.5 pt-4 border-t border-slate-100 mb-6">
                    {serv.detalles.map((det, i) => (
                      <div key={i} className="flex items-start gap-2.5 text-xs text-slate-700">
                        <Check size={16} className="text-emerald-600 shrink-0 mt-0.5" />
                        <span>{det}</span>
                      </div>
                    ))}
                  </div>
                </div>

                <div className="pt-4 border-t border-slate-100">
                  <Link
                    to="/login"
                    className="inline-flex items-center gap-1.5 text-xs font-bold text-[#0D274C] group-hover:text-[#Eb8600] transition-colors"
                  >
                    <span>Gestionar en el Dashboard</span>
                    <ArrowRight size={14} className="group-hover:translate-x-1 transition-transform" />
                  </Link>
                </div>
              </div>
            );
          })}
        </div>

        {/* =========================================================
            FLUJO OPERATIVO: CÓMO VIAJAN LOS DATOS
        ========================================================= */}
        <div className="bg-gradient-to-r from-[#0D274C] to-[#163a6d] rounded-3xl p-8 sm:p-12 text-white shadow-xl space-y-8">
          <div className="text-center max-w-2xl mx-auto space-y-2">
            <h2 className="text-2xl sm:text-3xl font-extrabold">
              Ciclo de Vida de los Datos en la Plataforma
            </h2>
            <p className="text-sm text-slate-300">
              Flujo unificado para transformar capturas primarias en analítica institucional
            </p>
          </div>

          <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-6 pt-4">
            <div className="bg-white/10 rounded-2xl p-6 border border-white/10 space-y-3">
              <span className="text-3xl font-black text-amber-400">01</span>
              <h3 className="font-bold text-base text-white">Catálogos Maestros</h3>
              <p className="text-xs text-slate-300 leading-relaxed">
                Poblado de tablas independientes (ODS, líneas, áreas del conocimiento y aliados).
              </p>
            </div>

            <div className="bg-white/10 rounded-2xl p-6 border border-white/10 space-y-3">
              <span className="text-3xl font-black text-amber-400">02</span>
              <h3 className="font-bold text-base text-white">Entidades Fuertes</h3>
              <p className="text-xs text-slate-300 leading-relaxed">
                Creación de proyectos y registro de docentes con validación de existencia previa.
              </p>
            </div>

            <div className="bg-white/10 rounded-2xl p-6 border border-white/10 space-y-3">
              <span className="text-3xl font-black text-amber-400">03</span>
              <h3 className="font-bold text-base text-white">Tejido Relacional</h3>
              <p className="text-xs text-slate-300 leading-relaxed">
                Vinculación mediante tablas puente (desarrolla, aliado_proyecto, palabras_clave).
              </p>
            </div>

            <div className="bg-white/10 rounded-2xl p-6 border border-white/10 space-y-3">
              <span className="text-3xl font-black text-amber-400">04</span>
              <h3 className="font-bold text-base text-white">Analítica y Toma de Decisiones</h3>
              <p className="text-xs text-slate-300 leading-relaxed">
                Consultas multicampus en tiempo real para reportes ante Minciencias y acreditación.
              </p>
            </div>
          </div>
        </div>

      </div>
    </div>
  );
}

