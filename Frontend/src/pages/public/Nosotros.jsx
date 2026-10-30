import React from 'react';
import { 
  History, 
  Target, 
  Eye, 
  ShieldCheck, 
  Code2, 
  GraduationCap, 
  Database, 
  GitBranch, 
  CheckCircle2, 
  Sparkles 
} from 'lucide-react';

export default function Nosotros() {
  const principios = [
    {
      titulo: 'Integridad Referencial',
      descripcion: 'El sistema previene la existencia de registros huérfanos mediante validación de claves foráneas tanto en la capa de servicios de la API como en la base de datos PostgreSQL.',
    },
    {
      titulo: 'Preservación Histórica (Borrado Lógico)',
      descripcion: 'Prohibición estricta de borrados destructivos SQL. Toda entidad implementa el atributo `activo = false`, garantizando trazabilidad y auditoría a largo plazo.',
    },
    {
      titulo: 'Desacoplamiento Arquitectónico',
      descripcion: 'Separación total entre el Frontend (React 19) y el Backend (C# / .NET). El cliente web jamás se conecta directamente al motor de base de datos; todo viaja por HTTP/JSON.',
    },
    {
      titulo: 'Seguridad Granular y RBAC',
      descripcion: 'Manejo de sesiones mediante tokens criptográficos JWT y validación de roles de usuario (Administrador, Docente, Consulta), protegiendo endpoints sensibles.',
    },
  ];

  const fases = [
    { fase: 'Fase 0', nombre: 'Preparación y Base de Datos', detalle: 'Creación de las 21 tablas en Supabase con script SQL estructurado.' },
    { fase: 'Fase 1', nombre: 'Tablas Maestras Independientes', detalle: 'CRUD con borrado lógico para las 8 entidades raíz (Áreas OCDE, ODS, Proyectos, Aliados).' },
    { fase: 'Fase 2', nombre: 'Integridad y Tablas Puente', detalle: 'Entidades fuertes relacionales (Docentes, Productos) y 8 relaciones Muchos a Muchos.' },
    { fase: 'Fase 3', nombre: 'Seguridad y Roles (JWT)', detalle: 'Login institucional, encriptación BCrypt, interceptores Axios y panel de administración de cuentas.' },
    { fase: 'Fase 4', nombre: 'Analítica, Identidad y Despliegue', detalle: 'Portal institucional público, 10 consultas analíticas multitabla y despliegue en la nube.' },
  ];

  return (
    <div className="flex flex-col py-12 lg:py-20 bg-slate-50">
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 space-y-16">
        
        {/* =========================================================
            ENCABEZADO DE SECCIÓN
        ========================================================= */}
        <div className="text-center max-w-3xl mx-auto space-y-4">
          <div className="inline-flex items-center gap-2 px-3.5 py-1.5 rounded-full bg-amber-100 text-[#Eb8600] text-xs font-bold uppercase tracking-wider">
            <History size={14} />
            <span>Nuestra Identidad Institucional</span>
          </div>
          <h1 className="text-3xl sm:text-5xl font-black text-[#0D274C] tracking-tight">
            Sobre el Módulo Mapa de Conocimiento
          </h1>
          <p className="text-base sm:text-lg text-slate-600 leading-relaxed">
            Una iniciativa estratégica concebida para visibilizar, consolidar y articular el capital intelectual, 
            tecnológico y científico de la Universidad de San Buenaventura.
          </p>
        </div>

        {/* =========================================================
            MISIÓN Y VISIÓN
        ========================================================= */}
        <div className="grid grid-cols-1 md:grid-cols-2 gap-8">
          {/* Tarjeta Misión */}
          <div className="bg-white rounded-3xl p-8 sm:p-10 border border-slate-200 shadow-sm hover:shadow-md transition-shadow relative overflow-hidden">
            <div className="w-14 h-14 rounded-2xl bg-amber-500/10 text-[#Eb8600] flex items-center justify-center mb-6">
              <Target size={30} />
            </div>
            <h2 className="text-2xl font-bold text-[#0D274C] mb-4">Nuestra Misión</h2>
            <p className="text-slate-600 leading-relaxed text-base">
              Proveer una plataforma unificada, segura y accesible que capture y trace la producción investigativa 
              de la universidad, conectando docentes, proyectos, áreas del saber y aliados con los desafíos 
              de sostenibilidad de la Agenda 2030, fortaleciendo el impacto y visibilidad de nuestra comunidad académica.
            </p>
          </div>

          {/* Tarjeta Visión */}
          <div className="bg-white rounded-3xl p-8 sm:p-10 border border-slate-200 shadow-sm hover:shadow-md transition-shadow relative overflow-hidden">
            <div className="w-14 h-14 rounded-2xl bg-blue-500/10 text-[#0D274C] flex items-center justify-center mb-6">
              <Eye size={30} />
            </div>
            <h2 className="text-2xl font-bold text-[#0D274C] mb-4">Nuestra Visión</h2>
            <p className="text-slate-600 leading-relaxed text-base">
              Posicionar al Módulo Mapa de Conocimiento como el observatorio analítico de referencia institucional, 
              facilitando la toma de decisiones basada en datos, los procesos de acreditación de alta calidad 
              y la vinculación efectiva con el Sistema Nacional de Ciencia, Tecnología e Innovación (Minciencias).
            </p>
          </div>
        </div>

        {/* =========================================================
            PRINCIPIOS ARQUITECTÓNICOS
        ========================================================= */}
        <div className="bg-[#0D274C] text-white rounded-3xl p-8 sm:p-12 shadow-xl space-y-10">
          <div className="max-w-2xl">
            <span className="text-xs font-bold uppercase tracking-widest text-amber-400">
              Pilares de Ingeniería de Software
            </span>
            <h2 className="text-3xl font-extrabold mt-2 tracking-tight">
              Principios Rectores del Sistema
            </h2>
            <p className="text-slate-300 text-sm sm:text-base mt-2">
              Cada línea de código se adhiere a la Constitución del Proyecto, garantizando consistencia y fiabilidad.
            </p>
          </div>

          <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
            {principios.map((principio, idx) => (
              <div 
                key={idx} 
                className="bg-white/5 border border-white/10 rounded-2xl p-6 hover:bg-white/10 transition-colors"
              >
                <div className="flex items-center gap-3 mb-3">
                  <div className="p-1.5 rounded-lg bg-amber-400 text-[#0D274C]">
                    <CheckCircle2 size={18} />
                  </div>
                  <h3 className="font-bold text-lg text-white">{principio.titulo}</h3>
                </div>
                <p className="text-sm text-slate-300 leading-relaxed">
                  {principio.descripcion}
                </p>
              </div>
            ))}
          </div>
        </div>

        {/* =========================================================
            METODOLOGÍA INCREMENTAL (SPEC KITS)
        ========================================================= */}
        <div className="space-y-8">
          <div className="text-center max-w-2xl mx-auto space-y-3">
            <h2 className="text-2xl sm:text-3xl font-extrabold text-[#0D274C]">
              Ruta Metodológica Incremental (Spec Kits)
            </h2>
            <p className="text-sm sm:text-base text-slate-600">
              El desarrollo se estructuró en fases estrictamente auditadas bajo contratos JSON y criterios de aceptación.
            </p>
          </div>

          <div className="grid grid-cols-1 md:grid-cols-3 lg:grid-cols-5 gap-4">
            {fases.map((item, index) => (
              <div 
                key={index}
                className="bg-white p-5 rounded-2xl border border-slate-200 shadow-sm flex flex-col justify-between hover:border-[#Eb8600] transition-colors"
              >
                <div>
                  <span className="text-xs font-black px-2.5 py-1 rounded bg-slate-100 text-[#0D274C] uppercase tracking-wider inline-block mb-3">
                    {item.fase}
                  </span>
                  <h3 className="font-bold text-sm text-[#0D274C] mb-2">
                    {item.nombre}
                  </h3>
                  <p className="text-xs text-slate-500 leading-relaxed">
                    {item.detalle}
                  </p>
                </div>
              </div>
            ))}
          </div>
        </div>

        {/* =========================================================
            STACK TECNOLÓGICO
        ========================================================= */}
        <div className="bg-slate-100 rounded-3xl p-8 sm:p-10 border border-slate-200 text-center space-y-6">
          <div className="inline-flex items-center gap-2 text-xs font-bold uppercase tracking-wider text-slate-500">
            <Code2 size={16} />
            <span>Stack de Última Generación</span>
          </div>
          <h2 className="text-2xl sm:text-3xl font-black text-[#0D274C]">
            Tecnologías Empleadas en el Proyecto
          </h2>
          <div className="flex flex-wrap items-center justify-center gap-3 sm:gap-4 max-w-4xl mx-auto pt-2">
            {[
              'React 19',
              'Vite 8',
              'Tailwind CSS 4',
              'ASP.NET Core 10 Web API',
              'C# 13',
              'Entity Framework Core 8',
              'PostgreSQL (Supabase)',
              'JWT Bearer',
              'BCrypt',
              'Axios',
              'React Router DOM 7'
            ].map((tech) => (
              <span 
                key={tech}
                className="px-4 py-2 rounded-xl bg-white border border-slate-200 text-xs sm:text-sm font-bold text-slate-700 shadow-xs hover:border-[#Eb8600] hover:text-[#Eb8600] transition-colors"
              >
                {tech}
              </span>
            ))}
          </div>
        </div>

      </div>
    </div>
  );
}

