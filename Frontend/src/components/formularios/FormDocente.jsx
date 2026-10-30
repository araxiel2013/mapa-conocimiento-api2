import React, { useState } from 'react';
import InputInstitucional from '../ui/InputInstitucional';
import api from '../../api/axiosConfig';

export default function FormDocente() {
  const [datos, setDatos] = useState({
    cedula: '',
    nombres: '',
    apellidos: '',
    genero: '',
    cargo: '',
    fecha_nacimiento: '',
    correo: '',
    telefono: '',
    url_cvlac: '',
    escalafon: '',
    perfil: '',
    conv_minciencia: '',
    nacionalidad: ''
  });

  const [cargando, setCargando] = useState(false);
  const [mensaje, setMensaje] = useState({ texto: '', tipo: '' });

  const handleChange = (e) => {
    setDatos({ ...datos, [e.target.name]: e.target.value });
  };

  const manejarEnvio = async (e) => {
    e.preventDefault();
    if (!datos.nacionalidad || !datos.nacionalidad.trim()) {
      setMensaje({ texto: 'La nacionalidad del docente es obligatoria.', tipo: 'error' });
      return;
    }
    if (
      !datos.cedula ||
      !datos.nombres.trim() ||
      !datos.apellidos.trim() ||
      !datos.genero.trim() ||
      !datos.cargo.trim() ||
      !datos.fecha_nacimiento ||
      !datos.correo.trim() ||
      !datos.telefono.trim() ||
      !datos.url_cvlac.trim() ||
      !datos.escalafon.trim() ||
      !datos.perfil.trim() ||
      !datos.conv_minciencia.trim() ||
      !datos.nacionalidad.trim()
    ) {
      setMensaje({ texto: 'Por favor, complete todos los campos obligatorios.', tipo: 'error' });
      return;
    }

    setCargando(true);
    setMensaje({ texto: 'Guardando...', tipo: 'exito' });

    try {
      const payload = {
        cedula: parseInt(datos.cedula, 10),
        nombres: datos.nombres.trim(),
        apellidos: datos.apellidos.trim(),
        genero: datos.genero.trim(),
        cargo: datos.cargo.trim(),
        fecha_nacimiento: new Date(datos.fecha_nacimiento).toISOString(),
        correo: datos.correo.trim(),
        telefono: datos.telefono.trim(),
        url_cvlac: datos.url_cvlac.trim(),
        escalafon: datos.escalafon.trim(),
        perfil: datos.perfil.trim(),
        conv_minciencia: datos.conv_minciencia.trim(),
        nacionalidad: datos.nacionalidad.trim()
      };

      const respuesta = await api.post('/Docente', payload);
      setMensaje({ 
        texto: `¡Docente registrado con éxito! Cédula: ${respuesta.data?.datos?.cedula || datos.cedula}`, 
        tipo: 'exito' 
      });

      setDatos({
        cedula: '',
        nombres: '',
        apellidos: '',
        genero: '',
        cargo: '',
        fecha_nacimiento: '',
        correo: '',
        telefono: '',
        url_cvlac: '',
        escalafon: '',
        perfil: '',
        conv_minciencia: '',
        nacionalidad: ''
      });
    } catch (error) {
      setMensaje({
        texto: error.response?.data?.mensaje || 'Hubo un error al registrar el docente.',
        tipo: 'error'
      });
    } finally {
      setCargando(false);
    }
  };

  return (
    <form onSubmit={manejarEnvio} className="flex flex-col h-full w-full">
      <InputInstitucional
        label="1. Cédula del Docente *"
        name="cedula"
        type="number"
        value={datos.cedula}
        onChange={handleChange}
        placeholder="Ej. 1017123456"
        isValid={datos.cedula.length > 5}
      />

      <InputInstitucional
        label="2. Nombres *"
        name="nombres"
        value={datos.nombres}
        onChange={handleChange}
        placeholder="Ej. Carlos Andrés"
        isValid={datos.nombres.trim().length > 2}
      />

      <InputInstitucional
        label="3. Apellidos *"
        name="apellidos"
        value={datos.apellidos}
        onChange={handleChange}
        placeholder="Ej. Restrepo Valencia"
        isValid={datos.apellidos.trim().length > 2}
      />

      <InputInstitucional
        label="4. Género *"
        name="genero"
        value={datos.genero}
        onChange={handleChange}
        placeholder="Ej. Masculino / Femenino / Otro"
        isValid={datos.genero.trim().length > 0}
      />

      <InputInstitucional
        label="5. Cargo *"
        name="cargo"
        value={datos.cargo}
        onChange={handleChange}
        placeholder="Ej. Docente Investigador Tiempo Completo"
        isValid={datos.cargo.trim().length > 2}
      />

      <InputInstitucional
        label="6. Fecha de Nacimiento *"
        name="fecha_nacimiento"
        type="date"
        value={datos.fecha_nacimiento}
        onChange={handleChange}
        placeholder="YYYY-MM-DD"
        isValid={Boolean(datos.fecha_nacimiento)}
      />

      <InputInstitucional
        label="7. Correo Electrónico Institucional *"
        name="correo"
        type="email"
        value={datos.correo}
        onChange={handleChange}
        placeholder="Ej. carlos.restrepo@usb.edu.co"
        isValid={datos.correo.includes('@')}
      />

      <InputInstitucional
        label="8. Teléfono *"
        name="telefono"
        value={datos.telefono}
        onChange={handleChange}
        placeholder="Ej. 3001234567"
        isValid={datos.telefono.trim().length > 6}
      />

      <InputInstitucional
        label="9. Enlace CvLAC *"
        name="url_cvlac"
        value={datos.url_cvlac}
        onChange={handleChange}
        placeholder="https://scienti.minciencias.gov.co/cvlac/..."
        isValid={datos.url_cvlac.trim().length > 5}
      />

      <InputInstitucional
        label="10. Escalafón *"
        name="escalafon"
        value={datos.escalafon}
        onChange={handleChange}
        placeholder="Ej. Asociado / Titular / Auxiliar"
        isValid={datos.escalafon.trim().length > 0}
      />

      <InputInstitucional
        label="11. Perfil Profesional / Académico *"
        name="perfil"
        value={datos.perfil}
        onChange={handleChange}
        placeholder="Breve reseña del perfil..."
        isValid={datos.perfil.trim().length > 5}
      />

      <InputInstitucional
        label="12. Convocatoria Minciencias *"
        name="conv_minciencia"
        value={datos.conv_minciencia}
        onChange={handleChange}
        placeholder="Ej. Convocatoria 894 de 2021"
        isValid={datos.conv_minciencia.trim().length > 0}
      />

      <InputInstitucional
        label="13. Nacionalidad *"
        name="nacionalidad"
        value={datos.nacionalidad}
        onChange={handleChange}
        placeholder="Ej. Colombiana"
        isValid={datos.nacionalidad.trim().length > 2}
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
