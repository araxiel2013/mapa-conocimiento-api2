import React from 'react';
import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import Login from './pages/Login';
import Dashboard from './pages/Dashboard';
import PanelUsuarios from './pages/PanelUsuarios';
import ProtectedRoute from './components/ProtectedRoute';
import Navbar from './components/Navbar';

function App() {
  return (
    <Router>
      <Routes>
        {/* Ruta pública de Inicio de Sesión */}
        <Route path="/login" element={<Login />} />

        {/* Ruta Protegida: Dashboard con todos los Formularios Multicampus */}
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

        {/* Ruta Protegida: Panel de Administración de Usuarios (solo Admin) */}
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

        {/* Redirección por defecto */}
        <Route path="*" element={<Navigate to="/dashboard" replace />} />
      </Routes>
    </Router>
  );
}

export default App;