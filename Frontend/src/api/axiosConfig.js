import axios from 'axios';

// Creamos una instancia configurada apuntando a la variable de entorno de Vite
const api = axios.create({
    baseURL: import.meta.env.VITE_API_URL || 'http://localhost:5000/api'
});

// Interceptor: Atrapa la petición ANTES de enviarla
api.interceptors.request.use(
    (config) => {
        // Buscamos el token en la memoria del navegador
        const token = localStorage.getItem('token');
        if (token) {
            // Si hay token, se lo pegamos en la cabecera
            config.headers.Authorization = `Bearer ${token}`;
        }
        return config;
    },
    (error) => {
        return Promise.reject(error);
    }
);

// Interceptor de respuesta: Captura global de HTTP 401/403 para cerrar sesión en caliente
api.interceptors.response.use(
    (response) => {
        return response;
    },
    (error) => {
        const status = error.response?.status;
        const isLoginRequest = error.config?.url?.toLowerCase().includes('/auth/login');

        // Si el token expiró (401) o no tiene permisos (403), cerramos sesión automáticamente
        // salvo que sea la petición inicial de login con credenciales erróneas
        if ((status === 401 || status === 403) && !isLoginRequest) {
            localStorage.removeItem('token');
            localStorage.removeItem('usuario');
            if (window.location.pathname !== '/login') {
                window.location.href = '/login';
            }
        }

        return Promise.reject(error);
    }
);

export default api;

