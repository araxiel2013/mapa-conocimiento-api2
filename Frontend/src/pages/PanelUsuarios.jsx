import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import api from '../api/axiosConfig';
import { Users, ArrowLeft, ShieldCheck, UserCheck, UserX } from 'lucide-react';

const PanelUsuarios = () => {
    const [usuarios, setUsuarios] = useState([]);
    const [cargando, setCargando] = useState(true);
    const [error, setError] = useState('');

    useEffect(() => {
        const cargarUsuarios = async () => {
            setCargando(true);
            try {
                const response = await api.get('/Usuario');
                setUsuarios(response.data.datos || []);
            } catch (err) {
                setError('No tienes permisos para ver esta sección o hubo un error al cargar los usuarios.');
            } finally {
                setCargando(false);
            }
        };

        cargarUsuarios();
    }, []);

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

            {/* Contenedor Principal (Gris 1 rounded-[28px]) */}
            <div 
                className="w-full max-w-[1370px] bg-gris-1 flex flex-col p-8 sm:p-10 shadow-lg overflow-hidden border-2 border-white/20"
                style={{ borderRadius: '28px', minHeight: '600px' }}
            >
                <div className="flex items-center justify-between mb-6 pb-4 border-b border-black/10">
                    <div className="flex items-center gap-3">
                        <Users className="text-azul-2" size={28} />
                        <h2 className="text-[26px] font-bold text-azul-2 font-['Helvetica_Neue',_Helvetica,_Arial,_sans-serif]">
                            Cuentas del Sistema
                        </h2>
                    </div>
                    <span className="text-sm font-semibold text-gris-2 bg-white px-4 py-1.5 rounded-full border border-gris-2 shadow-sm">
                        Total: {usuarios.length} usuarios registrados
                    </span>
                </div>

                {error && (
                    <div className="bg-red-100 border border-red-300 text-red-700 p-4 rounded-[8px] mb-6 font-medium">
                        {error}
                    </div>
                )}

                {cargando ? (
                    <div className="flex justify-center items-center h-64 text-gris-2 text-lg font-medium">
                        Cargando usuarios...
                    </div>
                ) : (
                    <div className="bg-white border border-gris-2 rounded-[8px] overflow-hidden shadow-sm">
                        <table className="min-w-full divide-y divide-gray-200">
                            <thead className="bg-azul-2 text-white">
                                <tr>
                                    <th className="text-left py-4 px-6 font-bold text-[16px] tracking-wide">Usuario</th>
                                    <th className="text-left py-4 px-6 font-bold text-[16px] tracking-wide">Correo Electrónico</th>
                                    <th className="text-left py-4 px-6 font-bold text-[16px] tracking-wide">Roles Asignados</th>
                                    <th className="text-left py-4 px-6 font-bold text-[16px] tracking-wide">Estado</th>
                                </tr>
                            </thead>
                            <tbody className="divide-y divide-gray-100 text-[16px]">
                                {usuarios.map((u) => (
                                    <tr key={u.id} className="hover:bg-azul-1/5 transition-colors">
                                        <td className="py-4 px-6 font-bold text-azul-2">
                                            {u.username}
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
                                    </tr>
                                ))}
                            </tbody>
                        </table>
                    </div>
                )}
            </div>
        </div>
    );
};

export default PanelUsuarios;
