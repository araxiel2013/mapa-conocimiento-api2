import React from 'react';
import { 
  ResponsiveContainer, 
  PieChart, 
  Pie, 
  Cell, 
  Tooltip, 
  Legend 
} from 'recharts';
import { PieChart as PieIcon } from 'lucide-react';

// Paleta Institucional Universidad de San Buenaventura (USB)
const COLORES_POR_DEFECTO = [
  '#Eb8600', // Naranja Franciscano USB
  '#0D274C', // Azul Marino Institucional USB
  '#1E3A8A', // Azul USB Corporativo
  '#D97706', // Ámbar / Dorado Acreditación
  '#334155', // Gris Pizarra Institucional
  '#C2410C', // Naranja Quemado
  '#0284C7', // Azul Acero
  '#475569'  // Pizarra Medio
];

export default function GraficoTortaGenerico({ 
  titulo, 
  descripcion, 
  datos = [], 
  colores = COLORES_POR_DEFECTO,
  esMoneda = false,
  esDonut = true,
  altura = 340
}) {
  // Regla de Oro Anti-Crash (Rúbrica Fase 4):
  const datosValidos = Array.isArray(datos) && datos.length > 0;
  const tieneDatosPositivos = datosValidos && datos.some(item => Number(item?.cantidad || 0) > 0);

  if (!datosValidos || !tieneDatosPositivos) {
    return (
      <div className="bg-white rounded-2xl p-6 border border-slate-200 shadow-sm flex flex-col justify-between h-full min-h-[300px]">
        <div>
          <h3 className="text-base sm:text-lg font-black text-[#0D274C] mb-1">{titulo}</h3>
          {descripcion && <p className="text-xs text-slate-700 font-medium mb-4">{descripcion}</p>}
        </div>
        <div className="flex-1 flex flex-col items-center justify-center p-8 text-center bg-slate-50 rounded-xl border border-dashed border-slate-200">
          <div className="w-12 h-12 rounded-full bg-slate-100 text-slate-400 flex items-center justify-center mb-3">
            <PieIcon size={24} />
          </div>
          <p className="text-sm font-bold text-slate-700">
            No hay datos suficientes para mostrar esta métrica
          </p>
          <p className="text-xs text-slate-500 mt-1">
            Se actualizará automáticamente cuando existan fuentes de financiamiento o categorías vinculadas.
          </p>
        </div>
      </div>
    );
  }

  const customTooltip = ({ active, payload }) => {
    if (active && payload && payload.length) {
      const dataItem = payload[0];
      const valor = dataItem.value;
      const valorFormateado = esMoneda 
        ? new Intl.NumberFormat('es-CO', { style: 'currency', currency: 'COP', maximumFractionDigits: 0 }).format(valor)
        : new Intl.NumberFormat('es-CO').format(valor);

      return (
        <div className="bg-[#0D274C] text-white p-3 rounded-xl shadow-lg border border-slate-700 text-xs">
          <p className="font-bold text-amber-400 mb-1">{dataItem.name}</p>
          <p className="font-semibold">Aporte / Total: <span className="text-white font-mono">{valorFormateado}</span></p>
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
          <PieChart>
            <Tooltip content={customTooltip} />
            <Legend 
              verticalAlign="bottom" 
              height={44} 
              wrapperStyle={{ 
                fontSize: '13px', 
                fontWeight: 600, 
                paddingTop: '14px' 
              }} 
              formatter={(value) => (
                <span className="text-slate-800 font-bold text-xs sm:text-sm mr-2">{value}</span>
              )}
            />
            <Pie
              data={datos}
              dataKey="cantidad"
              nameKey="nombre"
              cx="50%"
              cy="46%"
              innerRadius={esDonut ? 55 : 0}
              outerRadius={85}
              paddingAngle={esDonut ? 3 : 0}
            >
              {datos.map((entry, index) => (
                <Cell 
                  key={`cell-${index}`} 
                  fill={colores[index % colores.length]} 
                  stroke="#ffffff"
                  strokeWidth={2}
                />
              ))}
            </Pie>
          </PieChart>
        </ResponsiveContainer>
      </div>
    </div>
  );
}
