import React, { useContext } from 'react';
import { Link, useNavigate, useLocation } from 'react-router-dom';
import { AuthContext } from '../context/AuthContext';
import { LogOut, Users, FileText } from 'lucide-react';
import LogoInstitucional from './ui/LogoInstitucional';

const Navbar = () => {
    const { user, logout } = useContext(AuthContext);
    const navigate = useNavigate();
    const location = useLocation();

    const handleLogout = () => {
        logout();
        navigate('/login');
    };

    const esAdmin = user?.roles?.includes('Administrador');

    return (
        <header className="w-full bg-[#0D274C] text-white shadow-md border-b border-slate-700/60 sticky top-0 z-50">
            <div className="max-w-[1370px] mx-auto px-4 sm:px-6 py-2.5 flex justify-between items-center">
                {/* Logo institucional y Título */}
                <div className="flex items-center gap-4">
                    <Link to="/dashboard" className="flex items-center gap-3 group">
                        <LogoInstitucional variante="blanco" className="h-9 sm:h-10" />
                        <div className="h-6 w-[1px] bg-white/25 hidden md:block"></div>
                        <span className="text-base sm:text-lg font-black tracking-tight text-white group-hover:text-amber-400 transition hidden lg:inline-block">
                            Mapa de Conocimiento Multicampus
                        </span>
                    </Link>
                </div>

                {/* Navegación y Sesión */}
                <div className="flex items-center gap-3 sm:gap-4">
                    <Link 
                        to="/dashboard" 
                        className={`flex items-center gap-2 text-xs sm:text-sm font-bold transition px-4 py-2 rounded-xl shadow-xs ${
                            location.pathname === '/dashboard' 
                                ? 'bg-[#Eb8600] text-white' 
                                : 'text-slate-200 hover:text-white hover:bg-white/10'
                        }`}
                    >
                        <FileText size={16} />
                        <span>Formularios</span>
                    </Link>

                    {esAdmin && (
                        <Link 
                            to="/usuarios" 
                            className={`flex items-center gap-2 text-xs sm:text-sm font-bold transition px-4 py-2 rounded-xl shadow-xs cursor-pointer ${
                                location.pathname === '/usuarios'
                                    ? 'bg-[#Eb8600] text-white'
                                    : 'bg-white/10 hover:bg-white/20 text-white border border-white/20'
                            }`}
                        >
                            <Users size={16} />
                            <span>Gestión de Usuarios</span>
                        </Link>
                    )}

                    <div className="border-l border-white/20 pl-3 sm:pl-4 flex items-center gap-3">
                        <div className="text-right hidden sm:block">
                            <span className="text-[11px] text-slate-300 font-medium block leading-tight">Usuario</span>
                            <span className="text-xs sm:text-sm font-extrabold text-white">{user?.username || 'Conectado'}</span>
                        </div>
                        <button 
                            onClick={handleLogout} 
                            title="Cerrar sesión"
                            className="bg-rose-600 hover:bg-rose-700 text-white font-bold px-3.5 py-1.5 rounded-xl text-xs sm:text-sm transition cursor-pointer flex items-center gap-1.5 shadow-sm"
                        >
                            <LogOut size={14} />
                            <span>Salir</span>
                        </button>
                    </div>
                </div>
            </div>
        </header>
    );
};

export default Navbar;
