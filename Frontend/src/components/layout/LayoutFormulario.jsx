import React from 'react';
import LogoInstitucional from '../ui/LogoInstitucional';

export default function LayoutFormulario({ children, menu, title = "Mapa de Conocimiento Multicampus", showGuardar = false, onGuardar }) {
  return (
    <div className="min-h-screen bg-gris-2 flex flex-col items-center py-10 font-sans">
      
      {/* Header Container */}
      <div className="w-full max-w-[1370px] flex flex-col items-center mb-6 relative">
        
        {/* Logos Oficiales USB */}
        <div className="flex items-center justify-center gap-10 mb-8 h-[90px]">
          <LogoInstitucional variante="blanco" className="h-[80px]" />
        </div>
        
        {/* Title & Guardar Button */}
        <div className="flex justify-between items-center w-full">
          <div className="flex-1"></div>
          <h1 className="text-white text-[50px] font-bold tracking-tight text-center font-['Helvetica_Neue',_Helvetica,_Arial,_sans-serif]">
            {title}
          </h1>
          <div className="flex-1 flex justify-end">
            {showGuardar && (
              <button 
                onClick={onGuardar}
                className="bg-naranja text-white font-semibold py-2 px-8 rounded-full text-[20px] shadow hover:bg-orange-600 transition-colors cursor-pointer">
                Guardar
              </button>
            )}
          </div>
        </div>

      </div>

      {/* Main Content Area (Gris 1 rounded-28px) */}
      <div 
        className="w-full max-w-[1370px] bg-gris-1 flex overflow-hidden shadow-lg"
        style={{ borderRadius: '28px', minHeight: '650px' }}
      >
        {/* Menu (Left panel) */}
        {menu && (
          <div className="w-[350px] flex-shrink-0 pt-10 pb-10 flex flex-col border-r-2 border-black/5">
            {menu}
          </div>
        )}
        
        {/* Form Content */}
        <div className="flex-grow p-10 relative">
          {children}
        </div>
      </div>
      
    </div>
  );
} 