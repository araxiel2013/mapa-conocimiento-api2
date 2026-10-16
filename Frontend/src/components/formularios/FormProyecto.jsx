import React, { useState } from 'react';
import InputInstitucional from '../ui/InputInstitucional';
import api from '../../api/axiosConfig';

export default function FormProyecto() {
  const [datos, setDatos] = useState({
    titulo: '',
    resumen: '',
    presupuesto: '',
    tipo_financiacion: '',
    tipo_fondos: '',
    fecha_inicio: '',
    fecha_fin: ''
  });

  const [mensaje, setMensaje] = useState({ texto: '', tipo: '' });

  const handleChange = (e) => {
    setDatos({ ...datos, [e.target.name]: e.target.value });
  };

  const manejarEnvio = async (e) => {
    e.preventDefault();
    setMensaje({ texto: 'Guardando...', tipo: 'exito' });
    try {
      const payload = {
        ...datos,
        presupuesto: datos.presupuesto ? parseFloat(datos.presupuesto) : null,
        fecha_inicio: datos.fecha_inicio ? new Date(datos.fecha_inicio).toISOString() : null,
        fecha_fin: datos.fecha_fin ? new Date(datos.fecha_fin).toISOString() : null,
      };

      await api.post('/proyecto', payload);
      setMensaje({ texto: '¡Guardado con éxito!', tipo: 'exito' });
      setDatos({ titulo: '', resumen: '', presupuesto: '', tipo_financiacion: '', tipo_fondos: '', fecha_inicio: '', fecha_fin: '' });
    } catch (error) {
      setMensaje({ texto: 'Hubo un error.', tipo: 'error' });
    }
  };

  return (
    <form onSubmit={manejarEnvio} className="flex flex-col h-full w-full">
      <InputInstitucional 
        label="1. Ingrese el Título del proyecto"
        name="titulo"
        value={datos.titulo}
        onChange={handleChange}
        placeholder="Título"
        isValid={datos.titulo.length > 3}
      />
      <InputInstitucional 
        label="2. Ingrese el Resumen del proyecto"
        name="resumen"
        value={datos.resumen}
        onChange={handleChange}
        placeholder="Resumen"
        isValid={datos.resumen.length > 10}
      />
      <InputInstitucional 
        label="3. Presupuesto"
        name="presupuesto"
        value={datos.presupuesto}
        onChange={handleChange}
        placeholder="Ej. 1500000"
        isValid={datos.presupuesto.length > 0}
      />
      <InputInstitucional 
        label="4. Tipo de Financiación"
        name="tipo_financiacion"
        value={datos.tipo_financiacion}
        onChange={handleChange}
        placeholder="Ej. Interna / Externa"
        isValid={datos.tipo_financiacion.length > 0}
      />

      {mensaje.texto && (
        <p className={`text-xl ${mensaje.tipo === 'exito' ? 'text-naranja' : 'text-red-500'} mt-4`}>
          {mensaje.texto}
        </p>
      )}

      <div className="mt-auto pt-10">
        <button type="submit" className="hidden" id="btn-submit">Guardar</button>
      </div>
    </form>
  );
}
