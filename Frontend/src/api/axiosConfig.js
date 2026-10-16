import axios from 'axios';

// Creamos una instancia configurada apuntando a tu backend
const api = axios.create({
    baseURL: 'http://localhost:5000/api'
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

export default api;

