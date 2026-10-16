import React, { useState, useContext } from 'react';
import { useNavigate } from 'react-router-dom';
import api from '../api/axiosConfig';
import { AuthContext } from '../context/AuthContext';
import { Lock, User } from 'lucide-react';

const Login = () => {
    const [credenciales, setCredenciales] = useState({ username: '', password: '' });
    const [error, setError] = useState('');
    const [cargando, setCargando] = useState(false);
    const { login } = useContext(AuthContext);
    const navigate = useNavigate();

    const handleChange = (e) => {
        setCredenciales({ ...credenciales, [e.target.name]: e.target.value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        setError('');
        setCargando(true);

        try {
            const response = await api.post('/Auth/login', credenciales);
            const { token, usuario } = response.data;
            
            login(token, usuario);
            navigate('/dashboard');
        } catch (err) {
            setError(err.response?.data?.mensaje || err.response?.data?.detalle || 'Credenciales inválidas. Verifica tu usuario y contraseña.');
        } finally {
            setCargando(false);
        }
    };

    return (
        <div className="min-h-screen bg-gris-2 flex flex-col items-center justify-center py-10 px-4 font-sans">
            {/* Contenedor Superior con Logos Oficiales USB */}
            <div className="w-full max-w-[500px] flex flex-col items-center mb-8">
                <div className="flex items-center justify-center gap-8 mb-6 h-[90px]">
                    <img 
                        src="https://usb.edu.co/wp-content/uploads/2021/06/Logo-USB-RECTORIA.png" 
                        alt="Universidad de San Buenaventura" 
                        className="h-full object-contain filter brightness-0 invert" 
                    />
                    <img 
                        src="https://usb.edu.co/wp-content/uploads/2021/07/Logo-blanco-ACREDITACION.png" 
                        alt="Acreditación Institucional de Alta Calidad Multicampus" 
                        className="h-[75%] object-contain" 
                    />
                </div>
                
                <h1 className="text-white text-[34px] sm:text-[40px] font-bold tracking-tight text-center font-['Helvetica_Neue',_Helvetica,_Arial,_sans-serif] leading-tight">
                    Mapa de Conocimiento
                </h1>
                <p className="text-azul-1 text-sm sm:text-base font-semibold tracking-wider uppercase mt-1">
                    Acceso Institucional
                </p>
            </div>

            {/* Tarjeta de Login Institucional (Gris 1 rounded-[28px]) */}
            <div 
                className="w-full max-w-[460px] bg-gris-1 shadow-2xl p-8 sm:p-10 flex flex-col border-2 border-white/20"
                style={{ borderRadius: '28px' }}
            >
                <h2 className="text-[24px] font-bold text-azul-2 mb-6 text-center font-['Helvetica_Neue',_Helvetica,_Arial,_sans-serif]">
                    Iniciar Sesión
                </h2>

                {error && (
                    <div className="mb-5 p-3 bg-red-100 border border-red-300 text-red-700 rounded-[8px] text-sm text-center font-medium">
                        {error}
                    </div>
                )}

                <form onSubmit={handleSubmit} className="flex flex-col gap-5">
                    <div className="flex flex-col">
                        <label className="text-[17px] font-bold text-gris-2 mb-2 flex items-center gap-1.5">
                            <User size={18} className="text-gris-2" />
                            <span>Usuario</span>
                        </label>
                        <input 
                            type="text" 
                            name="username" 
                            value={credenciales.username}
                            onChange={handleChange} 
                            placeholder="Nombre de usuario"
                            required 
                            className="w-full h-[52px] bg-white border border-gris-2 px-4 text-[17px] text-azul-2 rounded-none focus:outline-none focus:border-naranja transition-colors"
                        />
                    </div>

                    <div className="flex flex-col">
                        <label className="text-[17px] font-bold text-gris-2 mb-2 flex items-center gap-1.5">
                            <Lock size={18} className="text-gris-2" />
                            <span>Contraseña</span>
                        </label>
                        <input 
                            type="password" 
                            name="password" 
                            value={credenciales.password}
                            onChange={handleChange} 
                            placeholder="••••••••"
                            required 
                            className="w-full h-[52px] bg-white border border-gris-2 px-4 text-[17px] text-azul-2 rounded-none focus:outline-none focus:border-naranja transition-colors"
                        />
                    </div>

                    <div className="mt-4">
                        <button 
                            type="submit" 
                            disabled={cargando}
                            className="w-full bg-naranja hover:bg-orange-600 text-white font-semibold py-3 px-8 rounded-full text-[19px] shadow-md transition-colors cursor-pointer disabled:opacity-50"
                        >
                            {cargando ? 'Ingresando...' : 'Ingresar'}
                        </button>
                    </div>
                </form>
            </div>
            
            <p className="text-white/60 text-xs text-center mt-8">
                Universidad de San Buenaventura &copy; {new Date().getFullYear()} — Sistema Multicampus
            </p>
        </div>
    );
};

export default Login;
