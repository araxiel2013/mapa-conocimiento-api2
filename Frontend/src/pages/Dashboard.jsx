import React, { useState } from 'react';
import LayoutFormulario from '../components/layout/LayoutFormulario';
import { ChevronDown, ChevronRight } from 'lucide-react';

import FormAreaConocimiento from '../components/formularios/FormAreaConocimiento';
import FormObjetivoDesarrolloSostenible from '../components/formularios/FormObjetivoDesarrolloSostenible';
import FormAreaAplicacion from '../components/formularios/FormAreaAplicacion';
import FormTerminoClave from '../components/formularios/FormTerminoClave';
import FormLineaInvestigacion from '../components/formularios/FormLineaInvestigacion';
import FormAliado from '../components/formularios/FormAliado';
import FormProyecto from '../components/formularios/FormProyecto';
import FormTipoProducto from '../components/formularios/FormTipoProducto';
import FormDocente from '../components/formularios/FormDocente';
import FormProducto from '../components/formularios/FormProducto';
import FormTablasPuente from '../components/formularios/FormTablasPuente';

const modulos = [
  { id: 'proyecto', label: 'Datos Básicos (Proyecto)' },
  { id: 'areaConocimiento', label: 'Área de Conocimiento' },
  { id: 'ods', label: 'Objetivos (ODS)' },
  { id: 'areaAplicacion', label: 'Área de Aplicación' },
  { id: 'terminoClave', label: 'Términos Clave' },
  { id: 'lineaInvestigacion', label: 'Líneas de Investigación' },
  { id: 'tipoProducto', label: 'Tipos de Producto' },
  { id: 'aliado', label: 'Aliados' },
  { id: 'docente', label: 'Docentes (Fase 2)' },
  { id: 'producto', label: 'Productos (Fase 2)' },
  { id: 'tablasPuente', label: 'Tablas Puente / Asociaciones' },
];

export default function Dashboard() {
  const [formularioActivo, setFormularioActivo] = useState('proyecto');

  const renderizarFormulario = () => {
    switch (formularioActivo) {
      case 'proyecto': return <FormProyecto />;
      case 'areaConocimiento': return <FormAreaConocimiento />;
      case 'ods': return <FormObjetivoDesarrolloSostenible />;
      case 'areaAplicacion': return <FormAreaAplicacion />;
      case 'terminoClave': return <FormTerminoClave />;
      case 'lineaInvestigacion': return <FormLineaInvestigacion />;
      case 'tipoProducto': return <FormTipoProducto />;
      case 'aliado': return <FormAliado />;
      case 'docente': return <FormDocente />;
      case 'producto': return <FormProducto />;
      case 'tablasPuente': return <FormTablasPuente />;
      default: return <div className="text-xl">Seleccione un módulo</div>;
    }
  };

  const menuNavegacion = (
    <div className="flex flex-col text-[20px] font-bold text-white w-full h-full border-r border-gris-2">
      {modulos.map((mod) => {
        const isActivo = formularioActivo === mod.id;
        return (
          <div key={mod.id} className="w-full">
            <button
              onClick={() => setFormularioActivo(mod.id)}
              className={`w-full flex items-center justify-between px-6 py-4 transition-all duration-200 cursor-pointer text-left ${
                isActivo 
                  ? 'bg-white/17 text-azul-1' 
                  : 'text-gris-2 hover:bg-white/5'
              }`}
            >
              <span className={isActivo ? 'text-azul-1' : 'text-gris-2'}>{mod.label}</span>
              {isActivo ? <ChevronDown size={20} className="text-azul-1" /> : <ChevronRight size={20} className="text-gris-2" />}
            </button>
            {isActivo && (
              <div className="bg-white/10 px-6 py-2 text-sm text-gris-2 font-normal flex flex-col gap-2">
                <span className="text-white opacity-80 pl-2">Información requerida</span>
              </div>
            )}
          </div>
        );
      })}
    </div>
  );

  return (
    <LayoutFormulario 
      menu={menuNavegacion} 
      showGuardar={true} 
      onGuardar={() => document.getElementById('btn-submit')?.click()}
    >
      <div className="bg-white border-2 border-gris-2 rounded-[8px] p-8 w-full max-w-[900px] h-full shadow-sm">
        {renderizarFormulario()}
      </div>
    </LayoutFormulario>
  );
}
