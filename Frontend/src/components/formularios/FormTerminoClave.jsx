import React, { useState } from 'react';
import InputInstitucional from '../ui/InputInstitucional';
import api from '../../api/axiosConfig';


export default function FormTerminoClave() {
  const [termino, setTermino] = useState('');
  const [cargando, setCargando] = useState(false);
  const [mensaje, setMensaje] = useState({ texto: '', tipo: '' });

  const manejarEnvio = async (e) => {
    e.preventDefault();
    if (!termino.trim()) {
      setMensaje({ texto: 'Por favor, ingrese el término clave.', tipo: 'error' });
      return;
    }
    setCargando(true);
    setMensaje({ texto: '', tipo: '' });

    try {
      await api.post('/TerminoClave', { termino });

      setMensaje({ texto: `¡Guardado con éxito!`, tipo: 'exito' });
      setTermino('');
    } catch (error) {
      setMensaje({ 
        texto: error.response?.data?.mensaje || 'Hubo un error al guardar los datos.', 
        tipo: 'error' 
      });
    } finally {
      setCargando(false);
    }
  };

  return (
    <form onSubmit={manejarEnvio} className="flex flex-col h-full w-full">
      
      <InputInstitucional 
        label="1. Ingrese el término clave"
        name="termino"
        value={termino}
        onChange={(e) => setTermino(e.target.value)}
        placeholder="Ej. Inteligencia Artificial"
       isValid={termino.length > 2} />
      {mensaje.texto && (
        <p className={`text-xl ${mensaje.tipo === 'exito' ? 'text-naranja' : 'text-red-500'} mt-4`}>
          {mensaje.texto}
        </p>
      )}
      <div className="mt-auto pt-10"><button type="submit" className="hidden" id="btn-submit" disabled={cargando}>{cargando ? 'Guardando...' : 'Guardar'}</button></div>
    </form>
  );
}

