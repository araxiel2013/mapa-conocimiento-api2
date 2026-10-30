import axios from 'axios';

// Limpiamos cualquier barra diagonal final duplicada en la URL base
const rawBaseURL = import.meta.env.VITE_API_URL || 'http://localhost:5000/api';
const baseURL = rawBaseURL.replace(/\/+$/, '');

// Instancia centralizada de Axios para la plataforma
const api = axios.create({
    baseURL,
    // Timeout de 60 segundos para tolerar el tiempo de Cold Start (~50s) de la capa gratuita de Render
    timeout: 60000
});

// Interceptor de Solicitud: Inyecta el token Bearer JWT automáticamente
api.interceptors.request.use(
    (config) => {
        const token = localStorage.getItem('token');
        if (token) {
            config.headers.Authorization = `Bearer ${token}`;
        }
        return config;
    },
    (error) => {
        return Promise.reject(error);
    }
);

// Interceptor de Respuesta: Maneja expiración de sesión (401/403) en caliente
api.interceptors.response.use(
    (response) => {
        return response;
    },
    (error) => {
        const status = error.response?.status;
        const isLoginRequest = error.config?.url?.toLowerCase().includes('/auth/login');

        // Si el token expiró (401) o no tiene permisos (403), limpiamos sesión y redirigimos
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
