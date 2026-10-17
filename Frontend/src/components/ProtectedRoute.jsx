import React, { useContext } from 'react';
import { Navigate } from 'react-router-dom';
import { AuthContext } from '../context/AuthContext';

const ProtectedRoute = ({ children, role, allowedRoles = [] }) => {
    const { isAuthenticated, user, loading } = useContext(AuthContext);

    // Mientras el AuthContext revisa el localStorage, mostramos un mensaje de espera
    if (loading) {
        return <div className="flex justify-center items-center h-screen">Cargando la sesión...</div>;
    }

    // Si no está logueado, lo devolvemos a la fuerza al login
    if (!isAuthenticated) {
        return <Navigate to="/login" replace />;
    }

    // Unificación de roles requeridos (soporta prop 'role' o 'allowedRoles')
    const rolesRequeridos = [
        ...(role ? [role] : []),
        ...(Array.isArray(allowedRoles) ? allowedRoles : [allowedRoles])
    ].filter(Boolean);

    // Si la ruta exige roles específicos, comprobamos si el usuario posee al menos uno
    if (rolesRequeridos.length > 0) {
        const tienePermiso = rolesRequeridos.some((r) => user?.roles?.includes(r));
        if (!tienePermiso) {
            return <Navigate to="/dashboard" replace />;
        }
    }

    // Si tiene sesión y roles válidos, renderizamos la pantalla
    return children;
};

export default ProtectedRoute;

