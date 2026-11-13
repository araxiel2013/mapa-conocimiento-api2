import React from 'react';

/**
 * Logotipo Institucional Oficial Universidad de San Buenaventura
 * Maneja versión clara/oscura con carga local para garantizar 100% de disponibilidad sin depender de servidores externos.
 */
export default function LogoInstitucional({ variante = 'blanco', className = 'h-10', incluirTexto = true }) {
  const rutaLogo = variante === 'blanco' ? '/logo-usb-blanco.svg' : '/logo-usb.svg';

  return (
    <div className={`flex items-center gap-3 select-none ${className}`}>
      <img 
        src={rutaLogo} 
        alt="Universidad de San Buenaventura" 
        className="h-full w-auto object-contain shrink-0"
        onError={(e) => {
          // Si por alguna razón la imagen no carga, mostramos el fallback SVG inline
          e.target.style.display = 'none';
          e.target.nextSibling.style.display = 'flex';
        }}
      />
      {/* Fallback de Respaldo SVG Inline para cero fallos */}
      <div className="hidden items-center gap-2.5">
        <div className="w-9 h-9 rounded-full bg-[#Eb8600] text-white flex items-center justify-center font-black text-sm shadow-sm">
          USB
        </div>
        {incluirTexto && (
          <div className="flex flex-col text-left">
            <span className={`text-[10px] font-bold tracking-wider ${variante === 'blanco' ? 'text-white/80' : 'text-slate-600'}`}>
              UNIVERSIDAD DE
            </span>
            <span className={`text-sm font-black tracking-tight leading-tight ${variante === 'blanco' ? 'text-white' : 'text-[#0D274C]'}`}>
              SAN BUENAVENTURA
            </span>
          </div>
        )}
      </div>
    </div>
  );
}
