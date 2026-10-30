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

  const estilosColor = {
    naranja: {
      bgIcono: 'bg-amber-500/10 text-[#Eb8600]',
      borde: 'hover:border-amber-400'
    },
    azul: {
      bgIcono: 'bg-blue-500/10 text-blue-600',
      borde: 'hover:border-blue-400'
    },
    verde: {
      bgIcono: 'bg-emerald-500/10 text-emerald-600',
      borde: 'hover:border-emerald-400'
    },
    morado: {
      bgIcono: 'bg-purple-500/10 text-purple-600',
      borde: 'hover:border-purple-400'
    }
  };

  const estiloSeleccionado = estilosColor[color] || estilosColor.azul;

  return (
    <div className={`bg-white rounded-2xl p-6 border border-slate-200 shadow-sm ${estiloSeleccionado.borde} transition-all duration-200 flex flex-col justify-between group`}>
      <div className="flex items-center justify-between mb-4">
        <span className="text-xs font-bold uppercase tracking-wider text-slate-500">
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
          <p className="text-xs text-slate-500 mt-1.5 leading-relaxed">
            {descripcion}
          </p>
        )}
      </div>
    </div>
  );
}

