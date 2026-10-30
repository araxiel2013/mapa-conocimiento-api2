import React, { createContext, useState, useEffect, useCallback } from 'react';
import { jwtDecode } from 'jwt-decode';

export const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
    const [isAuthenticated, setIsAuthenticated] = useState(false);
    const [user, setUser] = useState(null);
    const [loading, setLoading] = useState(true);

    // Función para destruir la sesión (declarada antes del useEffect para evitar TDZ)
    const logout = useCallback(() => {
        localStorage.removeItem('token');
        localStorage.removeItem('usuario');
        setIsAuthenticated(false);
        setUser(null);
    }, []);

    // Función que llamaremos cuando el Login sea exitoso
    const login = useCallback((token, usuario) => {
        localStorage.setItem('token', token);
        localStorage.setItem('usuario', JSON.stringify(usuario)); // Guardamos roles y datos
        setIsAuthenticated(true);
        setUser(usuario);
    }, []);

    // Se ejecuta al recargar la página para ver si ya había una sesión guardada
    useEffect(() => {
        const token = localStorage.getItem('token');
        const usuarioGuardado = localStorage.getItem('usuario');

        if (token && usuarioGuardado) {
            try {
                const decoded = jwtDecode(token);
                const currentTime = Date.now() / 1000;
                
                // Verificamos si la firma matemática del token ya expiró
                if (decoded.exp < currentTime) {
                    logout();
                } else {
                    setIsAuthenticated(true);
                    setUser(JSON.parse(usuarioGuardado));
                }
            } catch (error) {
                logout(); // Si el token está corrupto, cerramos sesión
            }
        }
        setLoading(false);
    }, [logout]);

    return (
        <AuthContext.Provider value={{ isAuthenticated, user, login, logout, loading }}>
            {children}
        </AuthContext.Provider>
    );
};
