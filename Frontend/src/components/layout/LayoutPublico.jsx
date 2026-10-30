import React, { useState, useContext } from 'react';
import { Outlet, NavLink, Link, useNavigate } from 'react-router-dom';
import { AuthContext } from '../../context/AuthContext';
import { 
  Menu, 
  X, 
  GraduationCap, 
  LogIn, 
  LayoutDashboard, 
  Globe, 
  Mail, 
  Phone, 
  MapPin, 
  ExternalLink,
  ChevronRight
} from 'lucide-react';

export default function LayoutPublico() {
  const [menuAbierto, setMenuAbierto] = useState(false);
  const { isAuthenticated, user } = useContext(AuthContext);
  const navigate = useNavigate();

  const enlacesNavegacion = [
    { ruta: '/', etiqueta: 'Inicio' },
    { ruta: '/nosotros', etiqueta: 'Nosotros' },
    { ruta: '/servicios', etiqueta: 'Servicios' },
    { ruta: '/soporte', etiqueta: 'Soporte' },
    { ruta: '/contacto', etiqueta: 'Contacto' },
  ];

  const alternarMenu = () => setMenuAbierto(!menuAbierto);
  const cerrarMenu = () => setMenuAbierto(false);

  return (
    <div className="min-h-screen flex flex-col bg-slate-50 text-slate-800 font-sans antialiased">
      {/* =========================================================
          BARRA DE NAVEGACIÓN SUPERIOR (NAVBAR)
      ========================================================= */}
      <header className="sticky top-0 z-50 bg-[#0D274C] text-white shadow-md border-b border-slate-700/50 backdrop-blur-md">
        <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
          <div className="flex items-center justify-between h-20">
            {/* Logotipo e Identidad */}
            <Link 
              to="/" 
              className="flex items-center gap-3 group transition-transform duration-200 hover:scale-[1.01]"
              onClick={cerrarMenu}
            >
              <div className="w-12 h-12 rounded-xl bg-gradient-to-tr from-[#Eb8600] to-amber-500 flex items-center justify-center shadow-lg shadow-amber-900/20 group-hover:shadow-amber-500/30 transition-all">
                <GraduationCap size={28} className="text-white" />
              </div>
              <div className="flex flex-col">
                <span className="font-extrabold text-xl tracking-tight leading-tight text-white">
                  Mapa de Conocimiento
                </span>
                <span className="text-xs text-amber-400 font-semibold uppercase tracking-wider">
                  Universidad de San Buenaventura
                </span>
              </div>
            </Link>

            {/* Enlaces de Escritorio */}
            <nav className="hidden md:flex items-center gap-1 lg:gap-2">
              {enlacesNavegacion.map((enlace) => (
                <NavLink
                  key={enlace.ruta}
                  to={enlace.ruta}
                  end={enlace.ruta === '/'}
                  className={({ isActive }) =>
                    `px-4 py-2 rounded-lg text-sm font-semibold transition-all duration-200 ${
                      isActive
                        ? 'bg-white/15 text-white shadow-inner font-bold border-b-2 border-[#Eb8600]'
                        : 'text-slate-200 hover:bg-white/10 hover:text-white'
                    }`
                  }
                >
                  {enlace.etiqueta}
                </NavLink>
              ))}
            </nav>

            {/* Botón de Acción Principal (Escritorio) */}
            <div className="hidden md:flex items-center gap-3">
              {isAuthenticated ? (
                <button
                  onClick={() => navigate('/dashboard')}
                  className="flex items-center gap-2 bg-[#Eb8600] hover:bg-[#d47800] text-white px-5 py-2.5 rounded-lg text-sm font-bold shadow-md hover:shadow-lg transition-all duration-200 transform hover:-translate-y-0.5 cursor-pointer"
                >
                  <LayoutDashboard size={18} />
                  <span>Ir al Dashboard</span>
                </button>
              ) : (
                <button
                  onClick={() => navigate('/login')}
                  className="flex items-center gap-2 bg-[#Eb8600] hover:bg-[#d47800] text-white px-5 py-2.5 rounded-lg text-sm font-bold shadow-md hover:shadow-lg transition-all duration-200 transform hover:-translate-y-0.5 cursor-pointer"
                >
                  <LogIn size={18} />
                  <span>Iniciar Sesión</span>
                </button>
              )}
            </div>

            {/* Botón Móvil Hamburguesa */}
            <div className="md:hidden flex items-center">
              <button
                type="button"
                onClick={alternarMenu}
                aria-label="Abrir menú de navegación"
                className="p-2 rounded-lg text-slate-200 hover:text-white hover:bg-white/10 focus:outline-none focus:ring-2 focus:ring-amber-400 transition-colors"
              >
                {menuAbierto ? <X size={28} /> : <Menu size={28} />}
              </button>
            </div>
          </div>
        </div>

        {/* Menú Desplegable Móvil */}
        {menuAbierto && (
          <div className="md:hidden bg-[#0a1e3b] border-t border-slate-700/80 px-4 pt-4 pb-6 space-y-2 animate-fadeIn">
            {enlacesNavegacion.map((enlace) => (
              <NavLink
                key={enlace.ruta}
                to={enlace.ruta}
                end={enlace.ruta === '/'}
                onClick={cerrarMenu}
                className={({ isActive }) =>
                  `block px-4 py-3 rounded-lg text-base font-semibold transition-all ${
                    isActive
                      ? 'bg-white/15 text-white font-bold border-l-4 border-[#Eb8600]'
                      : 'text-slate-300 hover:bg-white/5 hover:text-white'
                  }`
                }
              >
                {enlace.etiqueta}
              </NavLink>
            ))}

            <div className="pt-4 border-t border-slate-700/60 mt-4">
              {isAuthenticated ? (
                <button
                  onClick={() => {
                    cerrarMenu();
                    navigate('/dashboard');
                  }}
                  className="w-full flex items-center justify-center gap-2 bg-[#Eb8600] hover:bg-[#d47800] text-white py-3 rounded-lg font-bold shadow-md"
                >
                  <LayoutDashboard size={20} />
                  <span>Ir al Dashboard ({user?.username || 'Usuario'})</span>
                </button>
              ) : (
                <button
                  onClick={() => {
                    cerrarMenu();
                    navigate('/login');
                  }}
                  className="w-full flex items-center justify-center gap-2 bg-[#Eb8600] hover:bg-[#d47800] text-white py-3 rounded-lg font-bold shadow-md"
                >
                  <LogIn size={20} />
                  <span>Iniciar Sesión Institucional</span>
                </button>
              )}
            </div>
          </div>
        )}
      </header>

      {/* =========================================================
          CONTENIDO DINÁMICO DE LAS VISTAS PÚBLICAS
      ========================================================= */}
      <main className="flex-1 flex flex-col">
        <Outlet />
      </main>

      {/* =========================================================
          PIE DE PÁGINA CORPORATIVO (FOOTER)
      ========================================================= */}
      <footer className="bg-[#08182f] text-slate-300 pt-16 pb-8 border-t border-slate-800">
        <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
          <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-10 mb-12">
            {/* Columna 1: Identidad Institucional */}
            <div className="space-y-4">
              <div className="flex items-center gap-3">
                <div className="w-10 h-10 rounded-lg bg-[#Eb8600] flex items-center justify-center shadow-md">
                  <GraduationCap size={24} className="text-white" />
                </div>
                <div className="flex flex-col">
                  <span className="font-extrabold text-lg text-white">Mapa de Conocimiento</span>
                  <span className="text-[11px] text-amber-400 font-semibold uppercase">USB Colombia</span>
                </div>
              </div>
              <p className="text-sm text-slate-400 leading-relaxed">
                Plataforma de inteligencia y trazabilidad analítica de la producción investigativa, 
                articulando proyectos, docentes, áreas del saber y los Objetivos de Desarrollo Sostenible (ODS).
              </p>
              <div className="pt-2 text-xs text-slate-400 flex items-center gap-2">
                <span className="inline-block w-2 h-2 rounded-full bg-emerald-500 animate-pulse"></span>
                <span>Vigilada Mineducación · Acreditación Institucional</span>
              </div>
            </div>

            {/* Columna 2: Enlaces Rápidos */}
            <div className="space-y-4">
              <h3 className="text-white font-bold text-base tracking-wide border-b border-slate-700/60 pb-2">
                Navegación Institucional
              </h3>
              <ul className="space-y-2 text-sm">
                {enlacesNavegacion.map((enlace) => (
                  <li key={enlace.ruta}>
                    <Link
                      to={enlace.ruta}
                      className="hover:text-amber-400 transition-colors flex items-center gap-2 group"
                    >
                      <ChevronRight size={14} className="text-amber-500 group-hover:translate-x-1 transition-transform" />
                      <span>{enlace.etiqueta}</span>
                    </Link>
                  </li>
                ))}
                <li>
                  <Link
                    to="/login"
                    className="hover:text-amber-400 transition-colors flex items-center gap-2 group"
                  >
                    <ChevronRight size={14} className="text-amber-500 group-hover:translate-x-1 transition-transform" />
                    <span>Portal de Investigadores (Login)</span>
                  </Link>
                </li>
              </ul>
            </div>

            {/* Columna 3: Marco y Estándares de Investigación */}
            <div className="space-y-4">
              <h3 className="text-white font-bold text-base tracking-wide border-b border-slate-700/60 pb-2">
                Ecosistema y Estándares
              </h3>
              <ul className="space-y-2 text-sm text-slate-400">
                <li className="flex items-center gap-2">
                  <span className="w-1.5 h-1.5 rounded-full bg-amber-400"></span>
                  <span>Modelo de Medición Minciencias</span>
                </li>
                <li className="flex items-center gap-2">
                  <span className="w-1.5 h-1.5 rounded-full bg-amber-400"></span>
                  <span>17 Objetivos de Desarrollo Sostenible (ONU)</span>
                </li>
                <li className="flex items-center gap-2">
                  <span className="w-1.5 h-1.5 rounded-full bg-amber-400"></span>
                  <span>Clasificación de Áreas OCDE / Frascati</span>
                </li>
                <li className="flex items-center gap-2">
                  <span className="w-1.5 h-1.5 rounded-full bg-amber-400"></span>
                  <span>Redes Científicas CvLAC y GrupLAC</span>
                </li>
                <li className="flex items-center gap-2">
                  <span className="w-1.5 h-1.5 rounded-full bg-amber-400"></span>
                  <span>Alianzas Universidad - Empresa - Estado</span>
                </li>
              </ul>
            </div>

            {/* Columna 4: Canales de Atención */}
            <div className="space-y-4">
              <h3 className="text-white font-bold text-base tracking-wide border-b border-slate-700/60 pb-2">
                Contacto y Sedes
              </h3>
              <div className="space-y-3 text-sm text-slate-400">
                <p className="flex items-start gap-2.5">
                  <MapPin size={18} className="text-amber-400 shrink-0 mt-0.5" />
                  <span>Vicerrectoría de Investigaciones, Campus Principal. Bogotá D.C. - Colombia</span>
                </p>
                <p className="flex items-center gap-2.5">
                  <Mail size={18} className="text-amber-400 shrink-0" />
                  <span>investigaciones@usbbog.edu.co</span>
                </p>
                <p className="flex items-center gap-2.5">
                  <Phone size={18} className="text-amber-400 shrink-0" />
                  <span>+57 (601) 667 3535 Ext. 2100</span>
                </p>
                <p className="flex items-center gap-2.5">
                  <Globe size={18} className="text-amber-400 shrink-0" />
                  <span>www.usbbog.edu.co</span>
                </p>
              </div>
            </div>
          </div>

          {/* Línea inferior de derechos reservados */}
          <div className="pt-8 mt-8 border-t border-slate-800/80 flex flex-col sm:flex-row items-center justify-between gap-4 text-xs text-slate-400">
            <p>
              &copy; {new Date().getFullYear()} Universidad de San Buenaventura. Módulo Mapa de Conocimiento. Todos los derechos reservados.
            </p>
            <div className="flex items-center gap-6">
              <Link to="/soporte" className="hover:text-amber-400 transition-colors">Políticas de Privacidad</Link>
              <Link to="/soporte" className="hover:text-amber-400 transition-colors">Términos de Uso</Link>
              <Link to="/contacto" className="hover:text-amber-400 transition-colors">Mesa de Ayuda</Link>
            </div>
          </div>
        </div>
      </footer>
    </div>
  );
}

