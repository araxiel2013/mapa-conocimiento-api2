import React, { useState } from 'react';
import InputInstitucional from '../ui/InputInstitucional';


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
      const respuesta = await fetch('http://localhost:5000/api/TerminoClave', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ termino }) 
      });

      if (respuesta.ok) {
        setMensaje({ texto: `¡Guardado con éxito!`, tipo: 'exito' });
        setTermino('');
      } else {
        setMensaje({ texto: 'Hubo un error al guardar los datos.', tipo: 'error' });
      }
    } catch (error) {
      setMensaje({ texto: 'Error de conexión con el servidor.', tipo: 'error' });
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
      <div className="mt-auto pt-10"><button type="submit" className="hidden" id="btn-submit">Guardar</button></div>
    </form>
  );
}

