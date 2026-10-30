import React, { useContext } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { AuthContext } from '../../context/AuthContext';
import { 
  Sparkles, 
  ArrowRight, 
  Layers, 
  Award, 
  Target, 
  Users, 
  ShieldCheck, 
  BarChart3, 
  FileCheck2, 
  CheckCircle, 
  Database,
  Search
} from 'lucide-react';

export default function Inicio() {
  const { isAuthenticated } = useContext(AuthContext);
  const navigate = useNavigate();

  const estadisticas = [
    { valor: '21', etiqueta: 'Tablas Relacionales', descripcion: 'Modelo de datos estructurado en PostgreSQL' },
    { valor: '17', etiqueta: 'ODS Integrados', descripcion: 'Articulación con metas globales de la ONU' },
    { valor: '100%', etiqueta: 'Trazabilidad Minciencias', descripcion: 'Clasificación de productos y docentes' },
    { valor: '4', etiqueta: 'Fases Metodológicas', descripcion: 'Desde cimientos CRUD hasta analítica de datos' },
  ];

  const pilares = [
    {
      icono: Target,
      titulo: 'Alineación Estratégica con ODS',
      descripcion: 'Vincula cada proyecto investigativo con los 17 Objetivos de Desarrollo Sostenible de las Naciones Unidas, visibilizando el impacto social, ecológico y económico de la universidad.',
      color: 'bg-emerald-500'
    },
    {
      icono: Award,
      titulo: 'Modelo Oficial Minciencias',
      descripcion: 'Mapeo riguroso de productos científicos categorizados en Generación de Nuevo Conocimiento, Desarrollo Tecnológico, Apropiación Social y Formación del Recurso Humano.',
      color: 'bg-amber-500'
    },
    {
      icono: Users,
      titulo: 'Red de Docentes y Alianzas',
      descripcion: 'Conecta grupos de investigación, docentes con sus respectivos perfiles CvLAC y aliados estratégicos del sector público y privado en esquemas de cooperación n-arios.',
      color: 'bg-blue-600'
    },
    {
      icono: BarChart3,
      titulo: 'Analítica Multicampus',
      descripcion: 'Consultas relacionales complejas y dashboards en tiempo real para la toma de decisiones basada en evidencia por parte de decanaturas y la Vicerrectoría.',
      color: 'bg-indigo-600'
    }
  ];

  return (
    <div className="flex flex-col">
      {/* =========================================================
          HERO SECTION PRINCIPAL
      ========================================================= */}
      <section className="relative overflow-hidden bg-gradient-to-b from-[#0D274C] via-[#0f2e5a] to-slate-900 text-white pt-16 pb-24 lg:pt-24 lg:pb-32">
        {/* Adorno visual de fondo */}
        <div className="absolute top-0 left-1/2 -translate-x-1/2 w-full max-w-7xl h-full opacity-10 pointer-events-none">
          <div className="absolute -top-24 -left-24 w-96 h-96 rounded-full bg-[#Eb8600] blur-3xl"></div>
          <div className="absolute top-1/2 -right-24 w-96 h-96 rounded-full bg-cyan-400 blur-3xl"></div>
        </div>

        <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 relative z-10">
          <div className="text-center max-w-4xl mx-auto space-y-8">
            {/* Badge de estado */}
            <div className="inline-flex items-center gap-2 px-4 py-2 rounded-full bg-white/10 border border-white/20 text-xs sm:text-sm font-semibold tracking-wide text-amber-300 shadow-inner">
              <Sparkles size={16} className="text-amber-400" />
              <span>Plataforma Oficial de Inteligencia Investigativa · USB Colombia</span>
            </div>

            {/* Titular Principal */}
            <h1 className="text-4xl sm:text-5xl lg:text-6xl font-extrabold tracking-tight leading-tight text-white">
              Mapeo Estratégico del Conocimiento, la <span className="text-transparent bg-clip-text bg-gradient-to-r from-amber-400 via-[#Eb8600] to-orange-400">Innovación</span> y la Ciencia
            </h1>

            {/* Subtítulo descriptivo */}
            <p className="text-lg sm:text-xl text-slate-300 max-w-3xl mx-auto leading-relaxed">
              Consolida, articula y mide la producción intelectual y científica de la Universidad de San Buenaventura. 
              Un ecosistema unificado para conectar investigadores, proyectos, aliados y metas de sostenibilidad global.
            </p>

            {/* Botones de Llamada a la Acción (CTA) */}
            <div className="flex flex-col sm:flex-row items-center justify-center gap-4 pt-4">
              {isAuthenticated ? (
                <button
                  onClick={() => navigate('/dashboard')}
                  className="w-full sm:w-auto flex items-center justify-center gap-3 bg-[#Eb8600] hover:bg-[#d47800] text-white px-8 py-4 rounded-xl text-base font-bold shadow-xl hover:shadow-2xl transition-all duration-200 transform hover:-translate-y-1 cursor-pointer"
                >
                  <span>Acceder al Dashboard Administrativo</span>
                  <ArrowRight size={20} />
                </button>
              ) : (
                <button
                  onClick={() => navigate('/login')}
                  className="w-full sm:w-auto flex items-center justify-center gap-3 bg-[#Eb8600] hover:bg-[#d47800] text-white px-8 py-4 rounded-xl text-base font-bold shadow-xl hover:shadow-2xl transition-all duration-200 transform hover:-translate-y-1 cursor-pointer"
                >
                  <span>Iniciar Sesión en el Portal</span>
                  <ArrowRight size={20} />
                </button>
              )}

              <Link
                to="/servicios"
                className="w-full sm:w-auto flex items-center justify-center gap-2 bg-white/10 hover:bg-white/20 text-white border border-white/20 px-8 py-4 rounded-xl text-base font-semibold transition-all duration-200"
              >
                <Layers size={20} className="text-amber-400" />
                <span>Explorar Módulos y Servicios</span>
              </Link>
            </div>
          </div>

          {/* Tarjetas de Métricas Institucionales */}
          <div className="grid grid-cols-2 lg:grid-cols-4 gap-4 sm:gap-6 mt-16 lg:mt-24">
            {estadisticas.map((stat, idx) => (
              <div 
                key={idx}
                className="bg-white/10 backdrop-blur-md border border-white/15 rounded-2xl p-6 text-center hover:bg-white/15 transition-all duration-200 shadow-lg"
              >
                <div className="text-3xl sm:text-4xl lg:text-5xl font-black text-amber-400 mb-2">
                  {stat.valor}
                </div>
                <div className="text-sm sm:text-base font-bold text-white mb-1">
                  {stat.etiqueta}
                </div>
                <div className="text-xs text-slate-300">
                  {stat.descripcion}
                </div>
              </div>
            ))}
          </div>
        </div>
      </section>

      {/* =========================================================
          SECCIÓN: PILARES Y BENEFICIOS INSTITUCIONALES
      ========================================================= */}
      <section className="py-20 lg:py-28 bg-white text-slate-800">
        <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
          <div className="text-center max-w-3xl mx-auto mb-16 space-y-4">
            <h2 className="text-xs uppercase font-extrabold tracking-widest text-[#Eb8600]">
              Excelencia e Integración
            </h2>
            <p className="text-3xl sm:text-4xl font-extrabold text-[#0D274C] tracking-tight">
              ¿Por qué el Módulo Mapa de Conocimiento transforma nuestra gestión?
            </p>
            <p className="text-base sm:text-lg text-slate-600">
              Diseñado desde cero bajo principios de arquitectura limpia, integridad referencial y preservación histórica de datos.
            </p>
          </div>

          <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-8">
            {pilares.map((pilar, index) => {
              const Icono = pilar.icono;
              return (
                <div 
                  key={index} 
                  className="bg-slate-50 border border-slate-200 rounded-2xl p-7 hover:shadow-xl transition-all duration-300 flex flex-col justify-between group hover:-translate-y-1"
                >
                  <div>
                    <div className={`w-14 h-14 rounded-xl ${pilar.color} text-white flex items-center justify-center mb-6 shadow-md group-hover:scale-110 transition-transform`}>
                      <Icono size={28} />
                    </div>
                    <h3 className="text-xl font-bold text-[#0D274C] mb-3">
                      {pilar.titulo}
                    </h3>
                    <p className="text-sm text-slate-600 leading-relaxed mb-6">
                      {pilar.descripcion}
                    </p>
                  </div>
                  <div className="pt-4 border-t border-slate-200/80 flex items-center text-xs font-bold text-[#Eb8600] group-hover:translate-x-1 transition-transform">
                    <span>Módulo Integrado</span>
                    <ArrowRight size={14} className="ml-1" />
                  </div>
                </div>
              );
            })}
          </div>
        </div>
      </section>

      {/* =========================================================
          SECCIÓN: FLUJO DE INFORMACIÓN Y TRAZABILIDAD
      ========================================================= */}
      <section className="py-20 bg-slate-100 border-y border-slate-200">
        <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
          <div className="grid grid-cols-1 lg:grid-cols-2 gap-12 items-center">
            <div className="space-y-6">
              <span className="text-xs uppercase font-extrabold tracking-widest text-[#Eb8600]">
                Estructura Metodológica
              </span>
              <h2 className="text-3xl sm:text-4xl font-extrabold text-[#0D274C] tracking-tight leading-tight">
                De datos independientes a redes vivas de conocimiento
              </h2>
              <p className="text-slate-600 text-base leading-relaxed">
                El sistema no almacena registros aislados: cada entidad nutre un grafo relacional que conecta 
                la cédula del docente investigador con su línea temática, sus productos aprobados y las empresas aliadas.
              </p>

              <div className="space-y-4 pt-2">
                <div className="flex items-start gap-3">
                  <div className="p-1 rounded bg-amber-100 text-[#Eb8600] shrink-0 mt-0.5">
                    <CheckCircle size={18} />
                  </div>
                  <p className="text-sm text-slate-700">
                    <strong>Integridad Referencial Estricta:</strong> Previene registros huérfanos validando en backend la existencia de cada clave foránea.
                  </p>
                </div>

                <div className="flex items-start gap-3">
                  <div className="p-1 rounded bg-amber-100 text-[#Eb8600] shrink-0 mt-0.5">
                    <CheckCircle size={18} />
                  </div>
                  <p className="text-sm text-slate-700">
                    <strong>Borrado Lógico Institucional:</strong> Ningún dato histórico es destruido físicamente; el atributo <code className="text-xs bg-slate-200 px-1.5 py-0.5 rounded font-mono">activo = false</code> preserva la memoria de la universidad.
                  </p>
                </div>

                <div className="flex items-start gap-3">
                  <div className="p-1 rounded bg-amber-100 text-[#Eb8600] shrink-0 mt-0.5">
                    <CheckCircle size={18} />
                  </div>
                  <p className="text-sm text-slate-700">
                    <strong>Seguridad por Roles (RBAC):</strong> Control granular de accesos entre Administradores, Editores y Docentes mediante tokens criptográficos JWT.
                  </p>
                </div>
              </div>

              <div className="pt-4">
                <Link
                  to="/nosotros"
                  className="inline-flex items-center gap-2 text-sm font-bold text-[#0D274C] hover:text-[#Eb8600] transition-colors"
                >
                  <span>Conocer más sobre nuestra filosofía de desarrollo</span>
                  <ArrowRight size={16} />
                </Link>
              </div>
            </div>

            {/* Ilustración de Tarjeta de Arquitectura */}
            <div className="bg-[#0D274C] text-white p-8 sm:p-10 rounded-3xl shadow-2xl space-y-6 border border-slate-700">
              <div className="flex items-center justify-between border-b border-slate-700 pb-4">
                <div className="flex items-center gap-3">
                  <Database size={24} className="text-amber-400" />
                  <span className="font-bold text-lg">Topología del Sistema</span>
                </div>
                <span className="text-xs bg-emerald-500/20 text-emerald-300 font-semibold px-2.5 py-1 rounded-full border border-emerald-500/30">
                  En Línea
                </span>
              </div>

              <div className="space-y-4 text-sm font-mono text-slate-300">
                <div className="bg-slate-800/80 p-4 rounded-xl border border-slate-700">
                  <div className="text-amber-400 font-bold mb-1">// 1. Capa de Presentación</div>
                  <div>React 19 + Vite + Tailwind CSS (SPA Desacoplada)</div>
                </div>
                <div className="bg-slate-800/80 p-4 rounded-xl border border-slate-700">
                  <div className="text-cyan-400 font-bold mb-1">// 2. API Backend REST</div>
                  <div>ASP.NET Core (.NET) + Entity Framework Core</div>
                </div>
                <div className="bg-slate-800/80 p-4 rounded-xl border border-slate-700">
                  <div className="text-emerald-400 font-bold mb-1">// 3. Persistencia de Datos</div>
                  <div>Supabase (PostgreSQL Cloud Relacional)</div>
                </div>
              </div>

              <div className="pt-2 text-xs text-slate-400 text-center">
                Cumplimiento estricto de la Constitución del Proyecto (Artículos 1 al 8)
              </div>
            </div>
          </div>
        </div>
      </section>

      {/* =========================================================
          BANNER CTA FINAL
      ========================================================= */}
      <section className="bg-gradient-to-r from-[#0D274C] to-[#123668] text-white py-16">
        <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 text-center space-y-6">
          <h2 className="text-3xl sm:text-4xl font-extrabold tracking-tight">
            ¿Listo para gestionar y mapear la producción científica?
          </h2>
          <p className="text-slate-300 max-w-2xl mx-auto text-base sm:text-lg">
            Inicia sesión con tus credenciales institucionales para acceder a los formularios de captura, 
            asociaciones n-arias y panel de control.
          </p>
          <div className="flex flex-col sm:flex-row items-center justify-center gap-4 pt-4">
            <Link
              to="/login"
              className="w-full sm:w-auto bg-[#Eb8600] hover:bg-[#d47800] text-white px-8 py-3.5 rounded-xl font-bold text-base shadow-lg hover:shadow-xl transition-all duration-200"
            >
              Ingresar al Módulo
            </Link>
            <Link
              to="/contacto"
              className="w-full sm:w-auto bg-transparent hover:bg-white/10 text-white border border-white/30 px-8 py-3.5 rounded-xl font-semibold text-base transition-all duration-200"
            >
              Contactar Soporte Técnico
            </Link>
          </div>
        </div>
      </section>
    </div>
  );
}

