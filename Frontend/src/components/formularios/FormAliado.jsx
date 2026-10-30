import React, { useState } from 'react';
import InputInstitucional from '../ui/InputInstitucional';
import api from '../../api/axiosConfig';


export default function FormAliado() {
  const [nit, setNit] = useState('');
  const [razonSocial, setRazonSocial] = useState('');
  const [nombreContacto, setNombreContacto] = useState('');
  const [correo, setCorreo] = useState('');
  const [telefono, setTelefono] = useState('');
  const [ciudad, setCiudad] = useState('');
  
  const [cargando, setCargando] = useState(false);
  const [mensaje, setMensaje] = useState({ texto: '', tipo: '' });

  const manejarEnvio = async (e) => {
    e.preventDefault();
    if (!nit || !razonSocial.trim() || !nombreContacto.trim() || !correo.trim() || !telefono.trim() || !ciudad.trim()) {
      setMensaje({ texto: 'Por favor, complete todos los campos.', tipo: 'error' });
      return;
    }
    setCargando(true);
    setMensaje({ texto: '', tipo: '' });

    try {
      const respuesta = await api.post('/Aliado', { 
        nit: parseInt(nit), 
        razon_social: razonSocial, 
        nombre_contacto: nombreContacto,
        correo: correo,
        telefono: telefono,
        ciudad: ciudad
      });

      const datos = respuesta.data;
      setMensaje({ texto: `¡Guardado con éxito! NIT/ID: ${datos.nit || nit}`, tipo: 'exito' });
      setNit('');
      setRazonSocial('');
      setNombreContacto('');
      setCorreo('');
      setTelefono('');
      setCiudad('');
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
        label="1. NIT del Aliado"
        name="nit"
        value={nit}
        onChange={(e) => setNit(e.target.value)}
        placeholder="Ej. 900123456"
       isValid={nit.length > 2} />
      <InputInstitucional 
        label="2. Razón Social"
        name="razonSocial"
        value={razonSocial}
        onChange={(e) => setRazonSocial(e.target.value)}
        placeholder="Ej. Empresa SA"
       isValid={razonSocial.length > 2} />
      <InputInstitucional 
        label="3. Nombre del Contacto"
        name="nombreContacto"
        value={nombreContacto}
        onChange={(e) => setNombreContacto(e.target.value)}
        placeholder="Ej. Juan Pérez"
       isValid={nombreContacto.length > 2} />
      <InputInstitucional 
        label="4. Correo Electrónico"
        name="correo"
        value={correo}
        onChange={(e) => setCorreo(e.target.value)}
        placeholder="Ej. contacto@empresa.com"
       isValid={correo.length > 2} />
      <InputInstitucional 
        label="5. Teléfono"
        name="telefono"
        value={telefono}
        onChange={(e) => setTelefono(e.target.value)}
        placeholder="Ej. 3001234567"
       isValid={telefono.length > 2} />
      <InputInstitucional 
        label="6. Ciudad"
        name="ciudad"
        value={ciudad}
        onChange={(e) => setCiudad(e.target.value)}
        placeholder="Ej. Bogotá"
       isValid={ciudad.length > 2} />

      {mensaje.texto && (
        <p className={`text-xl ${mensaje.tipo === 'exito' ? 'text-naranja' : 'text-red-500'} mt-4`}>
          {mensaje.texto}
        </p>
      )}
      <div className="mt-auto pt-10"><button type="submit" className="hidden" id="btn-submit" disabled={cargando}>{cargando ? 'Guardando...' : 'Guardar'}</button></div>
    </form>
  );
}

