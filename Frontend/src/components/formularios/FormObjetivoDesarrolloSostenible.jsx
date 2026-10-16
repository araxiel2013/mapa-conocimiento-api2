import React, { useState } from 'react';
import InputInstitucional from '../ui/InputInstitucional';
import api from '../../api/axiosConfig';


export default function FormObjetivoDesarrolloSostenible() {
  const [nombre, setNombre] = useState('');
  const [categoria, setCategoria] = useState('');
  const [cargando, setCargando] = useState(false);
  const [mensaje, setMensaje] = useState({ texto: '', tipo: '' });

  const manejarEnvio = async (e) => {
    e.preventDefault();
    
    if (!nombre.trim() || !categoria.trim()) {
      setMensaje({ texto: 'Por favor, complete todos los campos requeridos.', tipo: 'error' });
      return;
    }

    setCargando(true);
    setMensaje({ texto: '', tipo: '' });

    try {
      const respuesta = await api.post('/ObjetivoDesarrolloSostenible', { nombre, categoria });

      const datos = respuesta.data;
      setMensaje({ texto: `¡Guardado con éxito! ID asignado: ${datos.id}`, tipo: 'exito' });
      setNombre('');
      setCategoria('');
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
        label="1. Ingrese el nombre del ODS"
        name="nombre"
        value={nombre}
        onChange={(e) => setNombre(e.target.value)}
        placeholder="Ej. Fin de la pobreza"
       isValid={nombre.length > 2} />
      
      <InputInstitucional 
        label="2. Ingrese la categoría"
        name="categoria"
        value={categoria}
        onChange={(e) => setCategoria(e.target.value)}
        placeholder="Ej. Social"
       isValid={categoria.length > 2} />

      {mensaje.texto && (
        <p className={`text-xl ${mensaje.tipo === 'exito' ? 'text-naranja' : 'text-red-500'} mt-4`}>
          {mensaje.texto}
        </p>
      )}

      <div className="mt-auto pt-10"><button type="submit" className="hidden" id="btn-submit">Guardar</button></div>
    </form>
  );
}

