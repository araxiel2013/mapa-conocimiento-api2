import React from 'react';
import ReactDOM from 'react-dom/client';
import App from './App.jsx';
import './index.css';
import { AuthProvider } from './context/AuthContext'; // <-- Importamos el proveedor

ReactDOM.createRoot(document.getElementById('root')).render(
  <React.StrictMode>
    <AuthProvider>  {/* <-- Envolvemos toda la app aquí */}
      <App />
    </AuthProvider>
  </React.StrictMode>,
);
