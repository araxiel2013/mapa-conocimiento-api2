import React, { useState, useContext } from 'react';
import { useNavigate } from 'react-router-dom';
import api from '../api/axiosConfig';
import { AuthContext } from '../context/AuthContext';
import { Lock, User, KeyRound, CheckCircle2, AlertCircle, X, ShieldCheck } from 'lucide-react';
import LogoInstitucional from '../components/ui/LogoInstitucional';

const Login = () => {
    // Estado del formulario de Login
    const [credenciales, setCredenciales] = useState({ username: '', password: '' });
    const [error, setError] = useState('');
    const [cargando, setCargando] = useState(false);
    const { login } = useContext(AuthContext);
    const navigate = useNavigate();

    // Estado del Modal de Recuperación de Contraseña
    const [mostrarModalRecuperar, setMostrarModalRecuperar] = useState(false);
    const [datosRecuperacion, setDatosRecuperacion] = useState({
        identificador: '',
        nueva_contrasena: '',
        confirmar_contrasena: ''
    });
    const [cargandoRecuperacion, setCargandoRecuperacion] = useState(false);
    const [errorRecuperacion, setErrorRecuperacion] = useState('');
    const [mensajeRecuperacion, setMensajeRecuperacion] = useState('');

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

    // Manejo de Recuperación de Contraseña
    const handleRecuperacionChange = (e) => {
        setDatosRecuperacion({ ...datosRecuperacion, [e.target.name]: e.target.value });
    };

    const handleRecuperarSubmit = async (e) => {
        e.preventDefault();
        setErrorRecuperacion('');
        setMensajeRecuperacion('');

        // Validaciones en cliente
        if (!datosRecuperacion.identificador.trim()) {
            setErrorRecuperacion('Por favor ingresa tu usuario o correo institucional.');
            return;
        }

        if (datosRecuperacion.nueva_contrasena.length < 6) {
            setErrorRecuperacion('La nueva contraseña debe tener al menos 6 caracteres.');
            return;
        }

        if (datosRecuperacion.nueva_contrasena !== datosRecuperacion.confirmar_contrasena) {
            setErrorRecuperacion('Las contraseñas no coinciden. Por favor verifícalas.');
            return;
        }

        setCargandoRecuperacion(true);

        try {
            const respuesta = await api.post('/Auth/recuperar-contrasena', {
                identificador: datosRecuperacion.identificador.trim(),
                nueva_contrasena: datosRecuperacion.nueva_contrasena
            });

            setMensajeRecuperacion(respuesta.data?.mensaje || 'Contraseña actualizada exitosamente.');
            // Prellenamos el usuario en el formulario de login para agilizar el acceso
            setCredenciales(prev => ({ ...prev, username: datosRecuperacion.identificador.trim(), password: '' }));
            setDatosRecuperacion({ identificador: '', nueva_contrasena: '', confirmar_contrasena: '' });
        } catch (err) {
            setErrorRecuperacion(err.response?.data?.mensaje || 'No fue posible restablecer la contraseña. Verifica que el usuario o correo institucional existan.');
        } finally {
            setCargandoRecuperacion(false);
        }
    };

    const cerrarModal = () => {
        setMostrarModalRecuperar(false);
        setErrorRecuperacion('');
        setMensajeRecuperacion('');
    };

    return (
        <div className="min-h-screen bg-[#6F6F6F] flex flex-col items-center justify-center py-10 px-4 font-sans relative">
            
            {/* Contenedor Superior con Logos Oficiales USB */}
            <div className="w-full max-w-[550px] flex flex-col items-center mb-8">
                <div className="flex items-center justify-center mb-5 w-full max-w-[480px]">
                    <LogoInstitucional variante="blanco" className="h-[75px] sm:h-[85px] w-auto" />
                </div>
                
                <h1 className="text-white text-[32px] sm:text-[38px] font-black tracking-tight text-center font-['Helvetica_Neue',_Helvetica,_Arial,_sans-serif] leading-tight">
                    Mapa de Conocimiento
                </h1>
                <p className="text-[#Eb8600] text-xs sm:text-sm font-extrabold tracking-widest uppercase mt-1">
                    Acceso Institucional
                </p>
            </div>

            {/* Tarjeta de Login Institucional */}
            <div 
                className="w-full max-w-[460px] bg-[#F0F0F0] shadow-2xl p-8 sm:p-10 flex flex-col border-2 border-white/20"
                style={{ borderRadius: '28px' }}
            >
                <h2 className="text-[24px] font-bold text-[#0D274C] mb-6 text-center font-['Helvetica_Neue',_Helvetica,_Arial,_sans-serif]">
                    Iniciar Sesión
                </h2>

                {error && (
                    <div className="mb-5 p-3.5 bg-rose-50 border border-rose-300 text-rose-800 rounded-xl text-xs sm:text-sm text-center font-semibold flex items-center justify-center gap-2">
                        <AlertCircle size={16} className="shrink-0 text-rose-600" />
                        <span>{error}</span>
                    </div>
                )}

                <form onSubmit={handleSubmit} className="flex flex-col gap-5">
                    <div className="flex flex-col">
                        <label className="text-[16px] font-bold text-slate-700 mb-2 flex items-center gap-2">
                            <User size={18} className="text-[#0D274C]" />
                            <span>Usuario o Correo Institucional</span>
                        </label>
                        <input 
                            type="text" 
                            name="username" 
                            value={credenciales.username}
                            onChange={handleChange} 
                            placeholder="Ej. docente@usb.edu.co o usuario"
                            required 
                            className="w-full h-[50px] bg-white border border-slate-300 px-4 text-[16px] text-[#0D274C] rounded-xl focus:outline-none focus:border-[#Eb8600] focus:ring-2 focus:ring-[#Eb8600]/20 transition-all font-medium"
                        />
                    </div>

                    <div className="flex flex-col">
                        <label className="text-[16px] font-bold text-slate-700 mb-2 flex items-center gap-2">
                            <Lock size={18} className="text-[#0D274C]" />
                            <span>Contraseña</span>
                        </label>
                        <input 
                            type="password" 
                            name="password" 
                            value={credenciales.password}
                            onChange={handleChange} 
                            placeholder="••••••••"
                            required 
                            className="w-full h-[50px] bg-white border border-slate-300 px-4 text-[16px] text-[#0D274C] rounded-xl focus:outline-none focus:border-[#Eb8600] focus:ring-2 focus:ring-[#Eb8600]/20 transition-all font-medium"
                        />
                    </div>

                    <div className="mt-2">
                        <button 
                            type="submit" 
                            disabled={cargando}
                            className="w-full bg-[#Eb8600] hover:bg-[#d47800] text-white font-bold py-3.5 px-8 rounded-full text-[18px] shadow-md transition-all cursor-pointer disabled:opacity-50"
                        >
                            {cargando ? 'Ingresando...' : 'Ingresar'}
                        </button>
                    </div>

                    {/* Botón de Recuperar Contraseña solicitado */}
                    <div className="mt-2 text-center">
                        <button 
                            type="button" 
                            onClick={() => {
                                setMostrarModalRecuperar(true);
                                setErrorRecuperacion('');
                                setMensajeRecuperacion('');
                            }}
                            className="text-slate-700 hover:text-[#0D274C] text-xs sm:text-sm font-bold underline transition-colors cursor-pointer py-1"
                        >
                            ¿Olvidaste tu contraseña? / Recuperar acceso
                        </button>
                    </div>
                </form>
            </div>
            
            <p className="text-white/80 text-xs text-center mt-8 font-medium">
                Universidad de San Buenaventura &copy; {new Date().getFullYear()} — Sistema Multicampus
            </p>

            {/* =========================================================
                MODAL FUNCIONAL DE RECUPERACIÓN DE CONTRASEÑA
            ========================================================= */}
            {mostrarModalRecuperar && (
                <div className="fixed inset-0 bg-black/60 backdrop-blur-xs flex items-center justify-center z-50 p-4 animate-in fade-in duration-200">
                    <div 
                        className="bg-white w-full max-w-[480px] p-6 sm:p-8 rounded-3xl shadow-2xl border border-slate-200 relative"
                        onClick={(e) => e.stopPropagation()}
                    >
                        {/* Botón Cerrar */}
                        <button 
                            onClick={cerrarModal}
                            className="absolute top-5 right-5 text-slate-400 hover:text-slate-700 p-1.5 rounded-full hover:bg-slate-100 transition-all cursor-pointer"
                        >
                            <X size={20} />
                        </button>

                        {/* Encabezado del Modal */}
                        <div className="flex items-center gap-3 mb-4">
                            <div className="w-12 h-12 rounded-2xl bg-[#Eb8600]/15 text-[#Eb8600] flex items-center justify-center shrink-0">
                                <KeyRound size={24} />
                            </div>
                            <div>
                                <h3 className="text-xl font-black text-[#0D274C] leading-tight">
                                    Recuperar Contraseña
                                </h3>
                                <p className="text-xs text-slate-600 font-medium">
                                    Universidad de San Buenaventura
                                </p>
                            </div>
                        </div>

                        {mensajeRecuperacion ? (
                            /* Pantalla de Éxito */
                            <div className="space-y-5 py-4">
                                <div className="p-4 bg-emerald-50 border border-emerald-200 rounded-2xl text-emerald-900 text-xs sm:text-sm flex items-start gap-3">
                                    <CheckCircle2 size={20} className="shrink-0 text-emerald-600 mt-0.5" />
                                    <div>
                                        <p className="font-bold">¡Restablecimiento exitoso!</p>
                                        <p className="mt-1 leading-relaxed text-emerald-800">{mensajeRecuperacion}</p>
                                    </div>
                                </div>
                                <button
                                    onClick={cerrarModal}
                                    className="w-full bg-[#0D274C] hover:bg-slate-900 text-white font-bold py-3 px-6 rounded-xl transition-all cursor-pointer shadow-sm text-sm"
                                >
                                    Ir a Iniciar Sesión con mi nueva clave
                                </button>
                            </div>
                        ) : (
                            /* Formulario de Restablecimiento */
                            <form onSubmit={handleRecuperarSubmit} className="space-y-4 mt-2">
                                <p className="text-xs text-slate-600 leading-relaxed">
                                    Ingresa tu nombre de usuario o correo institucional para verificar tu identidad y asignar una nueva contraseña segura.
                                </p>

                                {errorRecuperacion && (
                                    <div className="p-3 bg-rose-50 border border-rose-200 text-rose-800 rounded-xl text-xs font-semibold flex items-center gap-2">
                                        <AlertCircle size={16} className="shrink-0 text-rose-600" />
                                        <span>{errorRecuperacion}</span>
                                    </div>
                                )}

                                <div>
                                    <label className="block text-xs font-bold text-slate-700 uppercase tracking-wider mb-1.5">
                                        Usuario o Correo Institucional *
                                    </label>
                                    <input 
                                        type="text" 
                                        name="identificador" 
                                        value={datosRecuperacion.identificador}
                                        onChange={handleRecuperacionChange}
                                        placeholder="Ej. docente@usb.edu.co o jdoe"
                                        required
                                        className="w-full h-11 bg-slate-50 border border-slate-300 rounded-xl px-3.5 text-sm text-[#0D274C] focus:bg-white focus:outline-none focus:border-[#Eb8600] font-medium"
                                    />
                                </div>

                                <div>
                                    <label className="block text-xs font-bold text-slate-700 uppercase tracking-wider mb-1.5">
                                        Nueva Contraseña *
                                    </label>
                                    <input 
                                        type="password" 
                                        name="nueva_contrasena" 
                                        value={datosRecuperacion.nueva_contrasena}
                                        onChange={handleRecuperacionChange}
                                        placeholder="Mínimo 6 caracteres"
                                        required
                                        className="w-full h-11 bg-slate-50 border border-slate-300 rounded-xl px-3.5 text-sm text-[#0D274C] focus:bg-white focus:outline-none focus:border-[#Eb8600] font-medium"
                                    />
                                </div>

                                <div>
                                    <label className="block text-xs font-bold text-slate-700 uppercase tracking-wider mb-1.5">
                                        Confirmar Nueva Contraseña *
                                    </label>
                                    <input 
                                        type="password" 
                                        name="confirmar_contrasena" 
                                        value={datosRecuperacion.confirmar_contrasena}
                                        onChange={handleRecuperacionChange}
                                        placeholder="Repite la nueva contraseña"
                                        required
                                        className="w-full h-11 bg-slate-50 border border-slate-300 rounded-xl px-3.5 text-sm text-[#0D274C] focus:bg-white focus:outline-none focus:border-[#Eb8600] font-medium"
                                    />
                                </div>

                                <div className="pt-2 flex items-center justify-end gap-3">
                                    <button
                                        type="button"
                                        onClick={cerrarModal}
                                        className="px-4 py-2.5 rounded-xl border border-slate-300 text-slate-700 hover:bg-slate-100 font-bold text-xs transition cursor-pointer"
                                    >
                                        Cancelar
                                    </button>
                                    <button
                                        type="submit"
                                        disabled={cargandoRecuperacion}
                                        className="px-5 py-2.5 rounded-xl bg-[#Eb8600] hover:bg-[#d47800] text-white font-bold text-xs transition cursor-pointer shadow-sm disabled:opacity-50 flex items-center gap-2"
                                    >
                                        <ShieldCheck size={16} />
                                        <span>{cargandoRecuperacion ? 'Restableciendo...' : 'Restablecer Contraseña'}</span>
                                    </button>
                                </div>
                            </form>
                        )}
                    </div>
                </div>
            )}
        </div>
    );
};

export default Login;
