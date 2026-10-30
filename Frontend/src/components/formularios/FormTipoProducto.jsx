import React, { useState } from 'react';
import InputInstitucional from '../ui/InputInstitucional';
import api from '../../api/axiosConfig';

export default function FormTipoProducto() {
  const [categoria, setCategoria] = useState('');
  const [clase, setClase] = useState('');
  const [nombre, setNombre] = useState('');
  const [tipologia, setTipologia] = useState('');
  
  const [cargando, setCargando] = useState(false);
  const [mensaje, setMensaje] = useState({ texto: '', tipo: '' });

  const manejarEnvio = async (e) => {
    e.preventDefault();
    if (!categoria.trim() || !clase.trim() || !nombre.trim() || !tipologia.trim()) {
      setMensaje({ texto: 'Por favor, complete todos los campos requeridos.', tipo: 'error' });
      return;
    }
    setCargando(true);
    setMensaje({ texto: '', tipo: '' });

    try {
      await api.post('/TipoProducto', { categoria, clase, nombre, tipologia });

      setMensaje({ texto: '¡Guardado con éxito!', tipo: 'exito' });
      setCategoria('');
      setClase('');
      setNombre('');
      setTipologia('');
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
        label="1. Categoría"
        name="categoria"
        value={categoria}
        onChange={(e) => setCategoria(e.target.value)}
        placeholder="Ej. Generación de Nuevo Conocimiento"
        isValid={categoria.trim().length > 2}
      />
      <InputInstitucional 
        label="2. Clase"
        name="clase"
        value={clase}
        onChange={(e) => setClase(e.target.value)}
        placeholder="Ej. Artículos de investigación"
        isValid={clase.trim().length > 2}
      />
      <InputInstitucional 
        label="3. Nombre del Tipo de Producto"
        name="nombre"
        value={nombre}
        onChange={(e) => setNombre(e.target.value)}
        placeholder="Ej. Artículo en revista indexada"
        isValid={nombre.trim().length > 2}
      />
      <InputInstitucional 
        label="4. Tipología"
        name="tipologia"
        value={tipologia}
        onChange={(e) => setTipologia(e.target.value)}
        placeholder="Ej. A1, A, B o Tipología I"
        isValid={tipologia.trim().length > 0}
      />

      {mensaje.texto && (
        <p className={`text-xl ${mensaje.tipo === 'exito' ? 'text-naranja' : 'text-red-500'} mt-4`}>
          {mensaje.texto}
        </p>
      )}
      
      <div className="mt-auto pt-10">
        <button type="submit" className="hidden" id="btn-submit" disabled={cargando}>{cargando ? 'Guardando...' : 'Guardar'}</button>
      </div>
    </form>
  );
}

