import React, { useContext } from 'react';
import { Navigate } from 'react-router-dom';
import { AuthContext } from '../context/AuthContext';

const ProtectedRoute = ({ children }) => {
    const { isAuthenticated, loading } = useContext(AuthContext);

    // Mientras el AuthContext revisa el localStorage, mostramos un mensaje de espera
    if (loading) {
        return <div className="flex justify-center items-center h-screen">Cargando la sesión...</div>;
    }

    // Si no está logueado, lo devolvemos a la fuerza al login
    if (!isAuthenticated) {
        return <Navigate to="/login" replace />;
    }

    // Si tiene token, renderizamos la pantalla que pidió
    return children;
};

export default ProtectedRoute;

