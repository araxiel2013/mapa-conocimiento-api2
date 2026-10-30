import React from 'react';
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';

// Layouts
import LayoutPublico from './components/layout/LayoutPublico';

// Vistas Públicas Corporativas (Fase 4.3)
import Inicio from './pages/public/Inicio';
import Nosotros from './pages/public/Nosotros';
import Servicios from './pages/public/Servicios';
import Soporte from './pages/public/Soporte';
import Contacto from './pages/public/Contacto';

// Autenticación y Módulos Privados Transaccionales
import Login from './pages/Login';
import Dashboard from './pages/Dashboard';
import PanelUsuarios from './pages/PanelUsuarios';
import ProtectedRoute from './components/ProtectedRoute';
import Navbar from './components/Navbar';

function App() {
  return (
    <Router>
      <Routes>
        {/* =========================================================
            RUTAS PÚBLICAS CORPORATIVAS (LAYOUT PÚBLICO CON NAVBAR Y FOOTER)
        ========================================================= */}
        <Route element={<LayoutPublico />}>
          <Route path="/" element={<Inicio />} />
          <Route path="/nosotros" element={<Nosotros />} />
          <Route path="/servicios" element={<Servicios />} />
          <Route path="/soporte" element={<Soporte />} />
          <Route path="/contacto" element={<Contacto />} />
        </Route>

        {/* Ruta de Inicio de Sesión Institucional */}
        <Route path="/login" element={<Login />} />

        {/* =========================================================
            RUTAS PRIVADAS (TRANSACCIONALES PROTEGIDAS POR TOKEN JWT)
        ========================================================= */}
        {/* Tablero de Formularios y Módulos de Investigación */}
        <Route 
          path="/dashboard" 
          element={
            <ProtectedRoute>
              <div className="min-h-screen bg-gris-2 flex flex-col">
                <Navbar />
                <Dashboard />
              </div>
            </ProtectedRoute>
          } 
        />

        {/* Módulo Exclusivo de Administración de Usuarios (RBAC: Solo Administrador) */}
        <Route 
          path="/usuarios" 
          element={
            <ProtectedRoute role="Administrador">
              <div className="min-h-screen bg-gris-2 flex flex-col">
                <Navbar />
                <PanelUsuarios />
              </div>
            </ProtectedRoute>
          } 
        />

        {/* Redirección comodín al portal público */}
        <Route path="*" element={<Navigate to="/" replace />} />
      </Routes>
    </Router>
  );
}

export default App;
