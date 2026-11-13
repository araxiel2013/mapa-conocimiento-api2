import React from 'react';

export default function TarjetaKpi({ 
  titulo, 
  valor, 
  icono: Icono, 
  color = 'azul', 
  esMoneda = false,
  descripcion
}) {
  const formatearValor = (val) => {
    if (val === null || val === undefined || isNaN(val)) {
      return esMoneda ? '$ 0' : '0';
    }
    if (esMoneda) {
      return new Intl.NumberFormat('es-CO', {
        style: 'currency',
        currency: 'COP',
        maximumFractionDigits: 0
      }).format(val);
    }
    return new Intl.NumberFormat('es-CO').format(val);
  };

  // Paleta Institucional Universidad de San Buenaventura (USB)
  const estilosColor = {
    naranja: {
      bgIcono: 'bg-[#Eb8600]/15 text-[#Eb8600]',
      borde: 'hover:border-[#Eb8600]',
      acento: 'border-t-4 border-[#Eb8600]'
    },
    azul: {
      bgIcono: 'bg-[#0D274C]/15 text-[#0D274C]',
      borde: 'hover:border-[#0D274C]',
      acento: 'border-t-4 border-[#0D274C]'
    },
    dorado: {
      bgIcono: 'bg-[#D97706]/15 text-[#D97706]',
      borde: 'hover:border-[#D97706]',
      acento: 'border-t-4 border-[#D97706]'
    },
    corporativo: {
      bgIcono: 'bg-[#1E3A8A]/15 text-[#1E3A8A]',
      borde: 'hover:border-[#1E3A8A]',
      acento: 'border-t-4 border-[#1E3A8A]'
    },
    // Compatibilidad con invocaciones previas
    verde: {
      bgIcono: 'bg-[#D97706]/15 text-[#D97706]',
      borde: 'hover:border-[#D97706]',
      acento: 'border-t-4 border-[#D97706]'
    },
    morado: {
      bgIcono: 'bg-[#1E3A8A]/15 text-[#1E3A8A]',
      borde: 'hover:border-[#1E3A8A]',
      acento: 'border-t-4 border-[#1E3A8A]'
    }
  };

  const estiloSeleccionado = estilosColor[color] || estilosColor.azul;

  return (
    <div className={`bg-white rounded-2xl p-6 border border-slate-200 shadow-sm ${estiloSeleccionado.acento} ${estiloSeleccionado.borde} transition-all duration-200 flex flex-col justify-between group`}>
      <div className="flex items-center justify-between mb-3">
        <span className="text-xs font-black uppercase tracking-wider text-slate-800">
          {titulo}
        </span>
        {Icono && (
          <div className={`w-12 h-12 rounded-xl flex items-center justify-center shrink-0 ${estiloSeleccionado.bgIcono} transition-transform group-hover:scale-105`}>
            <Icono size={24} />
          </div>
        )}
      </div>

      <div>
        <div className="text-2xl sm:text-3xl font-black text-[#0D274C] tracking-tight">
          {formatearValor(valor)}
        </div>
        {descripcion && (
          <p className="text-xs text-slate-700 font-medium mt-1.5 leading-relaxed">
            {descripcion}
          </p>
        )}
      </div>
    </div>
  );
}
