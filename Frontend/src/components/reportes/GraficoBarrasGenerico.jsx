import React from 'react';
import { 
  ResponsiveContainer, 
  BarChart, 
  Bar, 
  XAxis, 
  YAxis, 
  CartesianGrid, 
  Tooltip, 
  Legend 
} from 'recharts';
import { BarChart3 } from 'lucide-react';

export default function GraficoBarrasGenerico({ 
  titulo, 
  descripcion, 
  datos = [], 
  horizontal = false, 
  colorBarra = '#Eb8600',
  altura = 340,
  esMoneda = false
}) {
  // Regla de Oro Anti-Crash (Rúbrica Fase 4):
  const datosValidos = Array.isArray(datos) && datos.length > 0;
  const tieneDatosPositivos = datosValidos && datos.some(item => Number(item?.valor || 0) > 0);

  if (!datosValidos || !tieneDatosPositivos) {
    return (
      <div className="bg-white rounded-2xl p-6 border border-slate-200 shadow-sm flex flex-col justify-between h-full min-h-[300px]">
        <div>
          <h3 className="text-base sm:text-lg font-black text-[#0D274C] mb-1">{titulo}</h3>
          {descripcion && <p className="text-xs text-slate-700 font-medium mb-4">{descripcion}</p>}
        </div>
        <div className="flex-1 flex flex-col items-center justify-center p-8 text-center bg-slate-50 rounded-xl border border-dashed border-slate-200">
          <div className="w-12 h-12 rounded-full bg-slate-100 text-slate-400 flex items-center justify-center mb-3">
            <BarChart3 size={24} />
          </div>
          <p className="text-sm font-bold text-slate-700">
            No hay datos suficientes para mostrar esta métrica
          </p>
          <p className="text-xs text-slate-500 mt-1">
            Los registros asociados se actualizarán automáticamente al ingresar nuevos datos.
          </p>
        </div>
      </div>
    );
  }

  const formatearEjeY = (valor) => {
    if (esMoneda) {
      if (valor >= 1000000) return `$${(valor / 1000000).toFixed(0)}M`;
      if (valor >= 1000) return `$${(valor / 1000).toFixed(0)}k`;
      return `$${valor}`;
    }
    return valor;
  };

  const customTooltip = ({ active, payload, label }) => {
    if (active && payload && payload.length) {
      const valor = payload[0].value;
      const valorFormateado = esMoneda 
        ? new Intl.NumberFormat('es-CO', { style: 'currency', currency: 'COP', maximumFractionDigits: 0 }).format(valor)
        : new Intl.NumberFormat('es-CO').format(valor);

      return (
        <div className="bg-[#0D274C] text-white p-3 rounded-xl shadow-lg border border-slate-700 text-xs">
          <p className="font-bold text-amber-400 mb-1">{label}</p>
          <p className="font-semibold">Cantidad: <span className="text-white font-mono">{valorFormateado}</span></p>
        </div>
      );
    }
    return null;
  };

  return (
    <div className="bg-white rounded-2xl p-6 border border-slate-200 shadow-sm flex flex-col justify-between h-full">
      <div className="mb-4">
        <h3 className="text-base sm:text-lg font-black text-[#0D274C] mb-1">{titulo}</h3>
        {descripcion && <p className="text-xs text-slate-700 font-medium">{descripcion}</p>}
      </div>

      <div className="w-full" style={{ height: altura }}>
        <ResponsiveContainer width="100%" height="100%">
          {horizontal ? (
            <BarChart
              data={datos}
              layout="vertical"
              margin={{ top: 10, right: 30, left: 40, bottom: 10 }}
            >
              <CartesianGrid strokeDasharray="3 3" horizontal={false} stroke="#e2e8f0" />
              <XAxis type="number" tickFormatter={formatearEjeY} stroke="#334155" fontSize={12} fontWeight={600} />
              <YAxis 
                type="category" 
                dataKey="etiqueta" 
                stroke="#0D274C" 
                fontSize={12} 
                fontWeight={600}
                width={130}
                tickLine={false}
              />
              <Tooltip content={customTooltip} />
              <Legend 
                wrapperStyle={{ fontSize: '13px', paddingTop: '10px' }} 
                formatter={(value) => <span className="text-slate-800 font-bold text-xs">{value}</span>}
              />
              <Bar 
                dataKey="valor" 
                name="Total Registrado" 
                fill={colorBarra} 
                radius={[0, 6, 6, 0]} 
              />
            </BarChart>
          ) : (
            <BarChart
              data={datos}
              margin={{ top: 10, right: 20, left: 0, bottom: 25 }}
            >
              <CartesianGrid strokeDasharray="3 3" vertical={false} stroke="#e2e8f0" />
              <XAxis 
                dataKey="etiqueta" 
                stroke="#0D274C" 
                fontSize={12} 
                fontWeight={600}
                tickLine={false}
                interval={0}
                angle={-20}
                textAnchor="end"
              />
              <YAxis 
                tickFormatter={formatearEjeY} 
                stroke="#334155" 
                fontSize={12} 
                fontWeight={600}
                tickLine={false}
              />
              <Tooltip content={customTooltip} />
              <Legend 
                wrapperStyle={{ fontSize: '13px', paddingTop: '10px' }} 
                formatter={(value) => <span className="text-slate-800 font-bold text-xs">{value}</span>}
              />
              <Bar 
                dataKey="valor" 
                name="Total Registrado" 
                fill={colorBarra} 
                radius={[6, 6, 0, 0]} 
              />
            </BarChart>
          )}
        </ResponsiveContainer>
      </div>
    </div>
  );
}
