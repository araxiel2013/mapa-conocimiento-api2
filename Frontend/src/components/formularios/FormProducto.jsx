import React, { useState } from 'react';
import InputInstitucional from '../ui/InputInstitucional';
import api from '../../api/axiosConfig';

export default function FormProducto() {
  const [datos, setDatos] = useState({
    nombre: '',
    categoria: '',
    fecha_entrega: '',
    tipo_producto_id: '',
    proyecto_id: ''
  });

  const [cargando, setCargando] = useState(false);
  const [mensaje, setMensaje] = useState({ texto: '', tipo: '' });

  const handleChange = (e) => {
    setDatos({ ...datos, [e.target.name]: e.target.value });
  };

  const manejarEnvio = async (e) => {
    e.preventDefault();
    if (!datos.nombre.trim() || !datos.categoria.trim() || !datos.fecha_entrega || !datos.tipo_producto_id) {
      setMensaje({ texto: 'Por favor, complete todos los campos obligatorios.', tipo: 'error' });
      return;
    }

    setCargando(true);
    setMensaje({ texto: 'Guardando...', tipo: 'exito' });

    try {
      const payload = {
        nombre: datos.nombre.trim(),
        categoria: datos.categoria.trim(),
        fecha_entrega: new Date(datos.fecha_entrega).toISOString(),
        tipo_producto_id: parseInt(datos.tipo_producto_id),
        proyecto_id: datos.proyecto_id ? parseInt(datos.proyecto_id) : null
      };

      const respuesta = await api.post('/Producto', payload);
      setMensaje({
        texto: `¡Producto registrado con éxito! ID asignado: ${respuesta.data?.datos?.id || 'OK'}`,
        tipo: 'exito'
      });

      setDatos({
        nombre: '',
        categoria: '',
        fecha_entrega: '',
        tipo_producto_id: '',
        proyecto_id: ''
      });
    } catch (error) {
      setMensaje({
        texto: error.response?.data?.mensaje || 'Hubo un error al registrar el producto.',
        tipo: 'error'
      });
    } finally {
      setCargando(false);
    }
  };

  return (
    <form onSubmit={manejarEnvio} className="flex flex-col h-full w-full">
      <InputInstitucional
        label="1. Nombre del Producto *"
        name="nombre"
        value={datos.nombre}
        onChange={handleChange}
        placeholder="Ej. Algoritmo de Visión por Computador para Diagnóstico"
        isValid={datos.nombre.trim().length > 3}
      />

      <InputInstitucional
        label="2. Categoría *"
        name="categoria"
        value={datos.categoria}
        onChange={handleChange}
        placeholder="Ej. Desarrollo Tecnológico e Innovación"
        isValid={datos.categoria.trim().length > 2}
      />

      <InputInstitucional
        label="3. Fecha de Entrega *"
        name="fecha_entrega"
        type="date"
        value={datos.fecha_entrega}
        onChange={handleChange}
        placeholder="YYYY-MM-DD"
        isValid={Boolean(datos.fecha_entrega)}
      />

      <InputInstitucional
        label="4. ID del Tipo de Producto *"
        name="tipo_producto_id"
        type="number"
        value={datos.tipo_producto_id}
        onChange={handleChange}
        placeholder="Ej. 1"
        isValid={datos.tipo_producto_id.length > 0}
      />

      <InputInstitucional
        label="5. ID del Proyecto Asociado (Opcional)"
        name="proyecto_id"
        type="number"
        value={datos.proyecto_id}
        onChange={handleChange}
        placeholder="Ej. 10 (Dejar vacío si es independiente)"
        isValid={true}
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

