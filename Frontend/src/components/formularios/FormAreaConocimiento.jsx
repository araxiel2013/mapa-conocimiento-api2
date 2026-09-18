import React, { useState } from 'react';
import InputInstitucional from '../ui/InputInstitucional';

export default function FormAreaConocimiento() {
  const [granArea, setGranArea] = useState('');
  const [area, setArea] = useState('');
  const [disciplina, setDisciplina] = useState('');
  const [cargando, setCargando] = useState(false);
  const [mensaje, setMensaje] = useState({ texto: '', tipo: '' }); 

  const manejarEnvio = async (e) => {
    e.preventDefault();
    if (!granArea.trim() || !area.trim() || !disciplina.trim()) {
      setMensaje({ texto: 'Por favor, complete todos los campos requeridos.', tipo: 'error' });
      return;
    }
    setCargando(true);
    setMensaje({ texto: '', tipo: '' });
    try {
      const respuesta = await fetch('http://localhost:5000/api/AreaConocimiento', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ gran_area: granArea, area: area, disciplina: disciplina }) 
      });
      if (respuesta.ok) {
        setMensaje({ texto: `¡Guardado con éxito!`, tipo: 'exito' });
        setGranArea(''); setArea(''); setDisciplina('');
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
        label="1. Ingrese la Gran Área"
        name="granArea"
        value={granArea}
        onChange={(e) => setGranArea(e.target.value)}
        placeholder="Ej. Ingeniería y Tecnología"
        isValid={granArea.length > 2}
      />

      <InputInstitucional 
        label="2. Ingrese el Área"
        name="area"
        value={area}
        onChange={(e) => setArea(e.target.value)}
        placeholder="Ej. Ingeniería de Sistemas"
        isValid={area.length > 2}
      />

      <InputInstitucional 
        label="3. Ingrese la Disciplina"
        name="disciplina"
        value={disciplina}
        onChange={(e) => setDisciplina(e.target.value)}
        placeholder="Ej. Ingeniería de Software"
        isValid={disciplina.length > 2}
      />

      {mensaje.texto && (
        <p className={`text-xl ${mensaje.tipo === 'exito' ? 'text-naranja' : 'text-red-500'} mt-4`}>
          {mensaje.texto}
        </p>
      )}

      {/* Botón oculto real que envía el form; usaremos el botón Guardar de LayoutFormulario mediante ref o context luego */}
      <button type="submit" className="hidden" id="btn-submit">Guardar</button>
    </form>
  );
}
