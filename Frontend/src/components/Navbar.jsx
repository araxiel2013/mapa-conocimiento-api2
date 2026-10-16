import React, { useContext } from 'react';
import { Link, useNavigate, useLocation } from 'react-router-dom';
import { AuthContext } from '../context/AuthContext';
import { LogOut, Users, FileText } from 'lucide-react';

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
        <header className="w-full bg-azul-2 text-white shadow-md border-b border-black/10 sticky top-0 z-50">
            <div className="max-w-[1370px] mx-auto px-6 py-2.5 flex justify-between items-center">
                {/* Logo institucional y Título */}
                <div className="flex items-center gap-4">
                    <Link to="/dashboard" className="flex items-center gap-4 group">
                        <img 
                            src="https://usb.edu.co/wp-content/uploads/2021/06/Logo-USB-RECTORIA.png" 
                            alt="Universidad de San Buenaventura" 
                            className="h-[38px] object-contain filter brightness-0 invert" 
                        />
                        <div className="h-5 w-[1px] bg-white/20 hidden sm:block"></div>
                        <span className="text-lg font-bold tracking-tight text-white group-hover:text-azul-1 transition font-['Helvetica_Neue',_Helvetica,_Arial,_sans-serif]">
                            Mapa de Conocimiento Multicampus
                        </span>
                    </Link>
                </div>

                {/* Navegación y Sesión */}
                <div className="flex items-center gap-4">
                    <Link 
                        to="/dashboard" 
                        className={`flex items-center gap-2 text-sm font-semibold transition px-4 py-1.5 rounded-full ${
                            location.pathname === '/dashboard' 
                                ? 'bg-white/20 text-azul-1 border border-azul-1/40' 
                                : 'text-gray-200 hover:text-white hover:bg-white/10'
                        }`}
                    >
                        <FileText size={16} />
                        <span>Formularios</span>
                    </Link>

                    {esAdmin && (
                        <Link 
                            to="/usuarios" 
                            className={`flex items-center gap-2 text-sm font-semibold transition px-4 py-1.5 rounded-full shadow-sm cursor-pointer ${
                                location.pathname === '/usuarios'
                                    ? 'bg-naranja text-white'
                                    : 'bg-naranja/90 hover:bg-naranja text-white'
                            }`}
                        >
                            <Users size={16} />
                            <span>Gestión de Usuarios</span>
                        </Link>
                    )}

                    <div className="border-l border-white/20 pl-4 flex items-center gap-3">
                        <div className="text-right hidden sm:block">
                            <span className="text-xs text-gray-400 block leading-tight">Usuario</span>
                            <span className="text-sm font-bold text-white">{user?.username || 'Conectado'}</span>
                        </div>
                        <button 
                            onClick={handleLogout} 
                            title="Cerrar sesión"
                            className="bg-red-600 hover:bg-red-700 text-white font-medium px-3.5 py-1.5 rounded-full text-xs sm:text-sm transition cursor-pointer flex items-center gap-1.5 shadow"
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
