import React, { useState } from 'react';
import InputInstitucional from '../ui/InputInstitucional';

export default function FormTipoProducto() {
  const [nombre, setNombre] = useState('');
  const [descripcion, setDescripcion] = useState('');
  
  const [cargando, setCargando] = useState(false);
  const [mensaje, setMensaje] = useState({ texto: '', tipo: '' });

  const manejarEnvio = async (e) => {
    e.preventDefault();
    if (!nombre.trim()) {
      setMensaje({ texto: 'Por favor, ingrese el nombre.', tipo: 'error' });
      return;
    }
    setCargando(true);
    setMensaje({ texto: '', tipo: '' });

    try {
      const respuesta = await fetch('http://localhost:5000/api/TipoProducto', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ nombre, descripcion }) 
      });

      if (respuesta.ok) {
        setMensaje({ texto: '¡Guardado con éxito!', tipo: 'exito' });
        setNombre('');
        setDescripcion('');
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
        label="1. Nombre del Tipo de Producto"
        name="nombre"
        value={nombre}
        onChange={(e) => setNombre(e.target.value)}
        placeholder="Ej. Artículo, Libro..."
        isValid={nombre.length > 2}
      />
      <InputInstitucional 
        label="2. Descripción"
        name="descripcion"
        value={descripcion}
        onChange={(e) => setDescripcion(e.target.value)}
        placeholder="Breve descripción"
        isValid={descripcion.length > 5}
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

