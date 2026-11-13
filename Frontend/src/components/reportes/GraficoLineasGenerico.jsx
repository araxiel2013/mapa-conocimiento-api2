import React from 'react';
import { 
  ResponsiveContainer, 
  LineChart, 
  Line, 
  XAxis, 
  YAxis, 
  CartesianGrid, 
  Tooltip, 
  Legend 
} from 'recharts';
import { TrendingUp } from 'lucide-react';

export default function GraficoLineasGenerico({ 
  titulo, 
  descripcion, 
  datos = [], 
  colorLinea = '#0D274C', // Azul Marino Institucional USB
  colorPunto = '#Eb8600', // Naranja Franciscano USB
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
            <TrendingUp size={24} />
          </div>
          <p className="text-sm font-bold text-slate-700">
            No hay datos suficientes para mostrar esta métrica
          </p>
          <p className="text-xs text-slate-500 mt-1">
            Se generará la serie temática a medida que se registren proyectos en el tiempo.
          </p>
        </div>
      </div>
    );
  }

  const customTooltip = ({ active, payload, label }) => {
    if (active && payload && payload.length) {
      const valor = payload[0].value;
      const valorFormateado = esMoneda 
        ? new Intl.NumberFormat('es-CO', { style: 'currency', currency: 'COP', maximumFractionDigits: 0 }).format(valor)
        : new Intl.NumberFormat('es-CO').format(valor);

      return (
        <div className="bg-[#0D274C] text-white p-3 rounded-xl shadow-lg border border-slate-700 text-xs">
          <p className="font-bold text-amber-400 mb-1">{label}</p>
          <p className="font-semibold">Métrica: <span className="text-white font-mono">{valorFormateado}</span></p>
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
          <LineChart
            data={datos}
            margin={{ top: 10, right: 20, left: 0, bottom: 20 }}
          >
            <CartesianGrid strokeDasharray="3 3" vertical={false} stroke="#e2e8f0" />
            <XAxis 
              dataKey="etiqueta" 
              stroke="#0D274C" 
              fontSize={12} 
              fontWeight={600}
              tickLine={false}
            />
            <YAxis 
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
            <Line 
              type="monotone" 
              dataKey="valor" 
              name="Tendencia Temática" 
              stroke={colorLinea} 
              strokeWidth={3}
              dot={{ r: 5, fill: colorPunto, strokeWidth: 2, stroke: '#ffffff' }}
              activeDot={{ r: 7, fill: colorPunto }}
            />
          </LineChart>
        </ResponsiveContainer>
      </div>
    </div>
  );
}
