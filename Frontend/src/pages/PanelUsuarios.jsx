import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import api from '../api/axiosConfig';
import { Users, ArrowLeft, ShieldCheck, UserCheck, UserX, UserPlus, Trash2, X, AlertCircle, CheckCircle2 } from 'lucide-react';

const ROLES_DISPONIBLES = [
    { id: 1, nombre: 'Administrador' },
    { id: 2, nombre: 'Editor' },
    { id: 3, nombre: 'Consulta' }
];

const PanelUsuarios = () => {
    const [usuarios, setUsuarios] = useState([]);
    const [cargando, setCargando] = useState(true);
    const [error, setError] = useState('');
    const [mensajeExito, setMensajeExito] = useState('');

    // Estado del modal de creación
    const [mostrarModal, setMostrarModal] = useState(false);
    const [guardando, setGuardando] = useState(false);
    const [errorModal, setErrorModal] = useState('');
    const [eliminandoId, setEliminandoId] = useState(null);

    const [nuevoUsuario, setNuevoUsuario] = useState({
        username: '',
        email: '',
        password: '',
        nombre_completo: '',
        roles_ids: [1]
    });

    const cargarUsuarios = async () => {
        setCargando(true);
        try {
            const response = await api.get('/Usuario');
            setUsuarios(response.data.datos || []);
        } catch (err) {
            setError(err.response?.data?.mensaje || 'No tienes permisos para ver esta sección o hubo un error al cargar los usuarios.');
        } finally {
            setCargando(false);
        }
    };

    useEffect(() => {
        cargarUsuarios();
    }, []);

    const handleCheckboxRol = (rolId) => {
        setNuevoUsuario((prev) => {
            const existe = prev.roles_ids.includes(rolId);
            if (existe) {
                // Al menos un rol debe estar seleccionado
                if (prev.roles_ids.length === 1) return prev;
                return { ...prev, roles_ids: prev.roles_ids.filter((id) => id !== rolId) };
            } else {
                return { ...prev, roles_ids: [...prev.roles_ids, rolId] };
            }
        });
    };

    const handleCrearUsuario = async (e) => {
        e.preventDefault();
        setErrorModal('');

        if (!nuevoUsuario.username.trim() || !nuevoUsuario.email.trim() || !nuevoUsuario.password.trim()) {
            setErrorModal('Por favor, completa los campos requeridos (usuario, correo y contraseña).');
            return;
        }

        if (nuevoUsuario.password.length < 6) {
            setErrorModal('La contraseña debe tener al menos 6 caracteres.');
            return;
        }

        if (nuevoUsuario.roles_ids.length === 0) {
            setErrorModal('Debes asignar al menos un rol al usuario.');
            return;
        }

        setGuardando(true);
        try {
            await api.post('/Usuario', {
                username: nuevoUsuario.username.trim(),
                email: nuevoUsuario.email.trim(),
                password: nuevoUsuario.password,
                nombre_completo: nuevoUsuario.nombre_completo.trim() || null,
                roles_ids: nuevoUsuario.roles_ids
            });

            setMensajeExito(`Usuario "${nuevoUsuario.username}" creado con éxito.`);
            setMostrarModal(false);
            setNuevoUsuario({
                username: '',
                email: '',
                password: '',
                nombre_completo: '',
                roles_ids: [1]
            });
            await cargarUsuarios();
        } catch (err) {
            setErrorModal(err.response?.data?.mensaje || 'Error al crear el usuario.');
        } finally {
            setGuardando(false);
        }
    };

    const handleEliminarUsuario = async (u) => {
        const confirmar = window.confirm(`¿Estás seguro de que deseas desactivar al usuario "${u.username}"?`);
        if (!confirmar) return;

        setEliminandoId(u.id);
        setError('');
        setMensajeExito('');

        try {
            await api.delete(`/Usuario/${u.id}`);
            setMensajeExito(`Usuario "${u.username}" desactivado correctamente.`);
            await cargarUsuarios();
        } catch (err) {
            setError(err.response?.data?.mensaje || 'Error al desactivar el usuario.');
        } finally {
            setEliminandoId(null);
        }
    };

    return (
        <div className="min-h-screen bg-gris-2 flex flex-col items-center py-10 px-4 font-sans">
            {/* Header con Logos Institucionales */}
            <div className="w-full max-w-[1370px] flex flex-col items-center mb-6 relative">
                <div className="flex items-center justify-center gap-10 mb-6 h-[80px]">
                    <img 
                        src="https://usb.edu.co/wp-content/uploads/2021/06/Logo-USB-RECTORIA.png" 
                        alt="Universidad de San Buenaventura" 
                        className="h-full object-contain filter brightness-0 invert" 
                    />
                    <img 
                        src="https://usb.edu.co/wp-content/uploads/2021/07/Logo-blanco-ACREDITACION.png" 
                        alt="Acreditación Institucional de Alta Calidad Multicampus" 
                        className="h-[80%] object-contain" 
                    />
                </div>

                <div className="flex justify-between items-center w-full">
                    <Link 
                        to="/dashboard"
                        className="flex items-center gap-2 text-white hover:text-azul-1 transition font-semibold text-lg cursor-pointer"
                    >
                        <ArrowLeft size={22} />
                        <span>Volver a Formularios</span>
                    </Link>

                    <h1 className="text-white text-[38px] sm:text-[46px] font-bold tracking-tight text-center font-['Helvetica_Neue',_Helvetica,_Arial,_sans-serif]">
                        Gestión de Usuarios
                    </h1>

                    <div className="w-[180px]"></div>
                </div>
            </div>

            {/* Contenedor Principal */}
            <div 
                className="w-full max-w-[1370px] bg-gris-1 flex flex-col p-8 sm:p-10 shadow-lg overflow-hidden border-2 border-white/20"
                style={{ borderRadius: '28px', minHeight: '600px' }}
            >
                <div className="flex flex-wrap items-center justify-between gap-4 mb-6 pb-4 border-b border-black/10">
                    <div className="flex items-center gap-3">
                        <Users className="text-azul-2" size={28} />
                        <h2 className="text-[26px] font-bold text-azul-2 font-['Helvetica_Neue',_Helvetica,_Arial,_sans-serif]">
                            Cuentas del Sistema
                        </h2>
                    </div>

                    <div className="flex items-center gap-4">
                        <span className="text-sm font-semibold text-gris-2 bg-white px-4 py-1.5 rounded-full border border-gris-2 shadow-sm">
                            Total: {usuarios.length} usuarios registrados
                        </span>
                        <button
                            onClick={() => { setErrorModal(''); setMostrarModal(true); }}
                            className="flex items-center gap-2 bg-naranja hover:bg-orange-600 text-white font-bold py-2 px-5 rounded-full shadow-md transition cursor-pointer"
                        >
                            <UserPlus size={18} />
                            <span>Nuevo Usuario</span>
                        </button>
                    </div>
                </div>

                {mensajeExito && (
                    <div className="bg-emerald-50 border border-emerald-300 text-emerald-800 p-4 rounded-[12px] mb-6 font-medium flex items-center justify-between shadow-sm">
                        <div className="flex items-center gap-2">
                            <CheckCircle2 className="text-emerald-600" size={20} />
                            <span>{mensajeExito}</span>
                        </div>
                        <button onClick={() => setMensajeExito('')} className="text-emerald-600 hover:text-emerald-900 cursor-pointer">
                            <X size={18} />
                        </button>
                    </div>
                )}

                {error && (
                    <div className="bg-red-50 border border-red-300 text-red-700 p-4 rounded-[12px] mb-6 font-medium flex items-center justify-between shadow-sm">
                        <div className="flex items-center gap-2">
                            <AlertCircle className="text-red-500" size={20} />
                            <span>{error}</span>
                        </div>
                        <button onClick={() => setError('')} className="text-red-500 hover:text-red-800 cursor-pointer">
                            <X size={18} />
                        </button>
                    </div>
                )}

                {cargando ? (
                    <div className="flex justify-center items-center h-64 text-gris-2 text-lg font-medium">
                        Cargando usuarios...
                    </div>
                ) : (
                    <div className="bg-white border border-gris-2 rounded-[12px] overflow-hidden shadow-sm">
                        <table className="min-w-full divide-y divide-gray-200">
                            <thead className="bg-azul-2 text-white">
                                <tr>
                                    <th className="text-left py-4 px-6 font-bold text-[16px] tracking-wide">Usuario</th>
                                    <th className="text-left py-4 px-6 font-bold text-[16px] tracking-wide">Nombre Completo</th>
                                    <th className="text-left py-4 px-6 font-bold text-[16px] tracking-wide">Correo Electrónico</th>
                                    <th className="text-left py-4 px-6 font-bold text-[16px] tracking-wide">Roles Asignados</th>
                                    <th className="text-left py-4 px-6 font-bold text-[16px] tracking-wide">Estado</th>
                                    <th className="text-center py-4 px-6 font-bold text-[16px] tracking-wide">Acciones</th>
                                </tr>
                            </thead>
                            <tbody className="divide-y divide-gray-100 text-[16px]">
                                {usuarios.map((u) => (
                                    <tr key={u.id} className="hover:bg-azul-1/5 transition-colors">
                                        <td className="py-4 px-6 font-bold text-azul-2">
                                            {u.username}
                                        </td>
                                        <td className="py-4 px-6 text-gray-700">
                                            {u.nombre_completo || '—'}
                                        </td>
                                        <td className="py-4 px-6 text-gray-700">
                                            {u.email}
                                        </td>
                                        <td className="py-4 px-6">
                                            <div className="flex flex-wrap gap-1.5">
                                                {u.roles?.map(rol => (
                                                    <span 
                                                        key={rol} 
                                                        className="inline-flex items-center gap-1 bg-naranja/15 text-naranja border border-naranja/40 text-xs px-3 py-1 rounded-full font-bold"
                                                    >
                                                        <ShieldCheck size={13} />
                                                        {rol}
                                                    </span>
                                                ))}
                                            </div>
                                        </td>
                                        <td className="py-4 px-6">
                                            {u.activo ? (
                                                <span className="inline-flex items-center gap-1.5 bg-emerald-100 text-emerald-800 border border-emerald-300 px-3 py-1 rounded-full text-xs font-bold">
                                                    <UserCheck size={14} />
                                                    Activo
                                                </span>
                                            ) : (
                                                <span className="inline-flex items-center gap-1.5 bg-red-100 text-red-800 border border-red-300 px-3 py-1 rounded-full text-xs font-bold">
                                                    <UserX size={14} />
                                                    Inactivo
                                                </span>
                                            )}
                                        </td>
                                        <td className="py-4 px-6 text-center">
                                            {u.activo ? (
                                                <button
                                                    onClick={() => handleEliminarUsuario(u)}
                                                    disabled={eliminandoId === u.id}
                                                    title="Desactivar usuario"
                                                    className="inline-flex items-center gap-1.5 text-red-600 hover:text-red-800 hover:bg-red-50 px-3 py-1.5 rounded-lg border border-red-200 transition font-medium text-sm cursor-pointer disabled:opacity-50"
                                                >
                                                    <Trash2 size={16} />
                                                    <span>{eliminandoId === u.id ? 'Eliminando...' : 'Desactivar'}</span>
                                                </button>
                                            ) : (
                                                <span className="text-xs text-gray-400 italic">Desactivado</span>
                                            )}
                                        </td>
                                    </tr>
                                ))}
                            </tbody>
                        </table>
                    </div>
                )}
            </div>

            {/* Modal para Crear Usuario (POST /api/Usuario) */}
            {mostrarModal && (
                <div className="fixed inset-0 bg-black/60 backdrop-blur-sm z-50 flex items-center justify-center p-4">
                    <div className="bg-white rounded-[24px] shadow-2xl max-w-lg w-full p-8 border border-gray-200 animate-fadeIn">
                        <div className="flex justify-between items-center pb-4 mb-6 border-b border-gray-200">
                            <div className="flex items-center gap-2">
                                <UserPlus className="text-naranja" size={24} />
                                <h3 className="text-2xl font-bold text-azul-2 font-['Helvetica_Neue',_Helvetica,_Arial,_sans-serif]">
                                    Registrar Usuario
                                </h3>
                            </div>
                            <button
                                onClick={() => setMostrarModal(false)}
                                className="text-gray-400 hover:text-gray-600 transition cursor-pointer"
                            >
                                <X size={24} />
                            </button>
                        </div>

                        {errorModal && (
                            <div className="bg-red-50 border border-red-300 text-red-700 p-3 rounded-[8px] mb-4 text-sm font-medium">
                                {errorModal}
                            </div>
                        )}

                        <form onSubmit={handleCrearUsuario} className="flex flex-col gap-4">
                            <div>
                                <label className="block text-sm font-bold text-gray-700 mb-1">
                                    Nombre de Usuario *
                                </label>
                                <input
                                    type="text"
                                    value={nuevoUsuario.username}
                                    onChange={(e) => setNuevoUsuario({ ...nuevoUsuario, username: e.target.value })}
                                    placeholder="Ej. jperez"
                                    required
                                    className="w-full border border-gray-300 rounded-[8px] p-3 text-azul-2 focus:outline-none focus:border-naranja transition"
                                />
                            </div>

                            <div>
                                <label className="block text-sm font-bold text-gray-700 mb-1">
                                    Nombre Completo
                                </label>
                                <input
                                    type="text"
                                    value={nuevoUsuario.nombre_completo}
                                    onChange={(e) => setNuevoUsuario({ ...nuevoUsuario, nombre_completo: e.target.value })}
                                    placeholder="Ej. Juan Pérez Gómez"
                                    className="w-full border border-gray-300 rounded-[8px] p-3 text-azul-2 focus:outline-none focus:border-naranja transition"
                                />
                            </div>

                            <div>
                                <label className="block text-sm font-bold text-gray-700 mb-1">
                                    Correo Electrónico *
                                </label>
                                <input
                                    type="email"
                                    value={nuevoUsuario.email}
                                    onChange={(e) => setNuevoUsuario({ ...nuevoUsuario, email: e.target.value })}
                                    placeholder="Ej. juan.perez@usb.edu.co"
                                    required
                                    className="w-full border border-gray-300 rounded-[8px] p-3 text-azul-2 focus:outline-none focus:border-naranja transition"
                                />
                            </div>

                            <div>
                                <label className="block text-sm font-bold text-gray-700 mb-1">
                                    Contraseña * (Mínimo 6 caracteres)
                                </label>
                                <input
                                    type="password"
                                    value={nuevoUsuario.password}
                                    onChange={(e) => setNuevoUsuario({ ...nuevoUsuario, password: e.target.value })}
                                    placeholder="••••••••"
                                    required
                                    minLength={6}
                                    className="w-full border border-gray-300 rounded-[8px] p-3 text-azul-2 focus:outline-none focus:border-naranja transition"
                                />
                            </div>

                            <div>
                                <label className="block text-sm font-bold text-gray-700 mb-2">
                                    Roles Asignados * (Al menos uno)
                                </label>
                                <div className="flex flex-wrap gap-3">
                                    {ROLES_DISPONIBLES.map((r) => {
                                        const seleccionado = nuevoUsuario.roles_ids.includes(r.id);
                                        return (
                                            <button
                                                type="button"
                                                key={r.id}
                                                onClick={() => handleCheckboxRol(r.id)}
                                                className={`px-4 py-2 rounded-full text-sm font-bold border transition cursor-pointer flex items-center gap-1.5 ${
                                                    seleccionado
                                                        ? 'bg-naranja text-white border-naranja shadow-sm'
                                                        : 'bg-gray-100 text-gray-600 border-gray-300 hover:bg-gray-200'
                                                }`}
                                            >
                                                <ShieldCheck size={16} />
                                                <span>{r.nombre}</span>
                                            </button>
                                        );
                                    })}
                                </div>
                            </div>

                            <div className="flex justify-end gap-3 mt-6 pt-4 border-t border-gray-200">
                                <button
                                    type="button"
                                    onClick={() => setMostrarModal(false)}
                                    className="px-5 py-2.5 rounded-full border border-gray-300 text-gray-700 font-semibold hover:bg-gray-100 transition cursor-pointer"
                                >
                                    Cancelar
                                </button>
                                <button
                                    type="submit"
                                    disabled={guardando}
                                    className="px-6 py-2.5 rounded-full bg-azul-2 hover:bg-azul-1 text-white font-bold transition cursor-pointer shadow-md disabled:opacity-50"
                                >
                                    {guardando ? 'Guardando...' : 'Crear Usuario'}
                                </button>
                            </div>
                        </form>
                    </div>
                </div>
            )}
        </div>
    );
};

export default PanelUsuarios;
