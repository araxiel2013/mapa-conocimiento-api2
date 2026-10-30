import React, { useState } from 'react';
import { Link } from 'react-router-dom';
import { 
  HelpCircle, 
  ChevronDown, 
  ChevronUp, 
  ShieldCheck, 
  UserCheck, 
  KeyRound, 
  LifeBuoy, 
  Mail, 
  FileQuestion,
  Lock
} from 'lucide-react';

export default function Soporte() {
  const [acordeonAbierto, setAcordeonAbierto] = useState(null);

  const toggleAcordeon = (id) => {
    setAcordeonAbierto(acordeonAbierto === id ? null : id);
  };

  const faqs = [
    {
      id: 1,
      pregunta: '¿Qué es el Módulo Mapa de Conocimiento y quiénes deben usarlo?',
      respuesta: 'Es el sistema informático institucional desarrollado para la Universidad de San Buenaventura que centraliza, clasifica y relaciona la investigación, los docentes, proyectos, productos y su alineación con los ODS. Su uso está orientado a docentes investigadores, directores de grupo de investigación, decanaturas y personal de la Vicerrectoría de Investigaciones.'
    },
    {
      id: 2,
      pregunta: '¿Cómo funciona el borrado lógico y qué pasa si elimino un registro por error?',
      respuesta: 'Por mandato constitucional del proyecto (Artículo 5), queda terminantemente prohibido el borrado destructivo (sentencia DELETE de SQL). Cuando eliminas un registro, el sistema simplemente actualiza su estado a inactivo (activo = false). Los datos no se pierden y pueden ser reactivados o auditados por el Administrador del sistema en caso de equivocación.'
    },
    {
      id: 3,
      pregunta: '¿Por qué la API rechaza el registro de un Producto o una Tabla Puente con error 400?',
      respuesta: 'El sistema implementa integridad referencial de negocio. Si intentas registrar un producto con un `tipo_producto_id` inexistente, o asignar a un docente en un proyecto que no existe o está inactivo, el servicio corta la petición con un error HTTP 400 ("Violación de Integridad Referencial"). Siempre debes asegurarte de registrar primero las tablas maestras antes de vincularlas.'
    },
    {
      id: 4,
      pregunta: '¿Cómo se maneja la seguridad de contraseñas y sesiones?',
      respuesta: 'Las contraseñas de los usuarios jamás se guardan en texto plano; son procesadas mediante el algoritmo unidireccional con salting BCrypt. Tras iniciar sesión exitosamente, el servidor emite un JSON Web Token (JWT) firmado criptográficamente que expira automáticamente y debe enviarse en la cabecera Authorization: Bearer de cada petición.'
    },
    {
      id: 5,
      pregunta: '¿Qué roles existen en la plataforma y cuáles son sus permisos?',
      respuesta: 'Existen tres roles principales: 1) Administrador: Control total del sistema, creación de cuentas, asignación de roles y eliminación lógica de usuarios. 2) Editor / Docente: Acceso a la captura y edición de proyectos, productos y vinculaciones relacionales. 3) Consulta: Visualización de información y reportes analíticos sin permisos de modificación.'
    },
    {
      id: 6,
      pregunta: '¿Qué debo hacer si olvido mi contraseña o no tengo acceso al Dashboard?',
      respuesta: 'Debes contactar a la Mesa de Ayuda de la Vicerrectoría de Investigaciones o a tu Administrador de Facultad a través del formulario de contacto o al correo investigaciones@usbbog.edu.co para que reestablezcan tus credenciales de acceso.'
    }
  ];

  const roles = [
    {
      nombre: 'Administrador',
      color: 'border-[#Eb8600] bg-amber-500/10 text-[#Eb8600]',
      descripcion: 'Acceso total a la plataforma. Puede crear usuarios, reasignar roles, gestionar el borrado lógico y configurar parámetros globales.',
      permisos: ['Gestión completa de usuarios (/usuarios)', 'CRUD en todos los módulos de Fase 1 y 2', 'Reactivación de datos inactivos', 'Auditoría general']
    },
    {
      nombre: 'Editor / Investigador',
      color: 'border-blue-500 bg-blue-500/10 text-blue-700',
      descripcion: 'Destinado a docentes y líderes de grupo para el registro y actualización continua de sus proyectos, productos y tablas puente.',
      permisos: ['Creación de proyectos y productos', 'Vinculación de docentes y ODS', 'Edición de información propia', 'Consulta de catálogos']
    },
    {
      nombre: 'Consulta',
      color: 'border-emerald-500 bg-emerald-500/10 text-emerald-700',
      descripcion: 'Perfil de visualización para directivos y evaluadores que requieren consultar indicadores de producción científica.',
      permisos: ['Acceso de solo lectura a catálogos', 'Generación de reportes analíticos', 'Consulta de redes de aliados', 'Sin permisos de edición ni borrado']
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
            <LifeBuoy size={14} />
            <span>Centro de Ayuda y Soporte</span>
          </div>
          <h1 className="text-3xl sm:text-5xl font-black text-[#0D274C] tracking-tight">
            Preguntas Frecuentes y Guía de Roles
          </h1>
          <p className="text-base sm:text-lg text-slate-600 leading-relaxed">
            Resuelve dudas sobre la operación del sistema, políticas de seguridad, roles de usuario y preservación de datos.
          </p>
        </div>

        {/* =========================================================
            SECCIÓN DE ACORDEONES (FAQS)
        ========================================================= */}
        <div className="max-w-4xl mx-auto space-y-4">
          <div className="flex items-center gap-3 mb-6">
            <FileQuestion size={24} className="text-[#Eb8600]" />
            <h2 className="text-2xl font-bold text-[#0D274C]">
              Preguntas Frecuentes de la Plataforma
            </h2>
          </div>

          {faqs.map((faq) => {
            const estaAbierto = acordeonAbierto === faq.id;
            return (
              <div
                key={faq.id}
                className="bg-white border border-slate-200 rounded-2xl overflow-hidden shadow-xs hover:border-slate-300 transition-colors"
              >
                <button
                  type="button"
                  onClick={() => toggleAcordeon(faq.id)}
                  className="w-full px-6 py-5 flex items-center justify-between text-left gap-4 font-bold text-base text-[#0D274C] hover:text-[#Eb8600] transition-colors cursor-pointer"
                >
                  <span>{faq.pregunta}</span>
                  <div className="shrink-0 p-1 rounded-full bg-slate-100 text-slate-600">
                    {estaAbierto ? <ChevronUp size={20} /> : <ChevronDown size={20} />}
                  </div>
                </button>

                {estaAbierto && (
                  <div className="px-6 pb-6 text-sm text-slate-600 leading-relaxed border-t border-slate-100 pt-4 bg-slate-50/50">
                    {faq.respuesta}
                  </div>
                )}
              </div>
            );
          })}
        </div>

        {/* =========================================================
            MATRIZ DE ROLES Y CONTROL DE ACCESO (RBAC)
        ========================================================= */}
        <div className="space-y-8 max-w-5xl mx-auto">
          <div className="text-center space-y-2">
            <div className="inline-flex items-center gap-2 text-xs font-bold uppercase tracking-wider text-slate-500">
              <KeyRound size={16} />
              <span>Control de Acceso Basado en Roles (RBAC)</span>
            </div>
            <h2 className="text-2xl sm:text-3xl font-black text-[#0D274C]">
              Esquema de Permisos por Rol
            </h2>
            <p className="text-sm text-slate-600">
              La plataforma restringe rutas y endpoints mediante validación de claims embebidos en el token JWT.
            </p>
          </div>

          <div className="grid grid-cols-1 md:grid-cols-3 gap-6">
            {roles.map((rol, i) => (
              <div 
                key={i}
                className="bg-white rounded-3xl p-7 border border-slate-200 shadow-sm flex flex-col justify-between"
              >
                <div>
                  <div className={`inline-block text-xs font-extrabold uppercase px-3 py-1 rounded-full border mb-4 ${rol.color}`}>
                    {rol.nombre}
                  </div>
                  <p className="text-xs text-slate-600 leading-relaxed mb-6">
                    {rol.descripcion}
                  </p>

                  <h4 className="text-xs font-bold uppercase tracking-wider text-slate-500 mb-3">
                    Permisos Clave:
                  </h4>
                  <ul className="space-y-2 text-xs text-slate-700">
                    {rol.permisos.map((p, idx) => (
                      <li key={idx} className="flex items-start gap-2">
                        <span className="w-1.5 h-1.5 rounded-full bg-emerald-500 shrink-0 mt-1.5"></span>
                        <span>{p}</span>
                      </li>
                    ))}
                  </ul>
                </div>
              </div>
            ))}
          </div>
        </div>

        {/* =========================================================
            TARJETA DE CONTACTO RÁPIDO
        ========================================================= */}
        <div className="bg-[#0D274C] rounded-3xl p-8 sm:p-12 text-white text-center max-w-3xl mx-auto space-y-6 shadow-xl">
          <h3 className="text-2xl sm:text-3xl font-extrabold">
            ¿No encuentras lo que buscas?
          </h3>
          <p className="text-slate-300 text-sm sm:text-base leading-relaxed">
            Nuestro equipo de soporte técnico y la Vicerrectoría de Investigaciones están a tu disposición para orientarte en el registro y homologación de tus datos.
          </p>
          <div className="pt-2">
            <Link
              to="/contacto"
              className="inline-flex items-center gap-2 bg-[#Eb8600] hover:bg-[#d47800] text-white px-8 py-3.5 rounded-xl font-bold text-sm shadow-md transition-all"
            >
              <Mail size={18} />
              <span>Enviar Solicitud a Mesa de Ayuda</span>
            </Link>
          </div>
        </div>

      </div>
    </div>
  );
}

