import React, { useState } from 'react';
import InputInstitucional from '../ui/InputInstitucional';
import api from '../../api/axiosConfig';

const RELACIONES = [
  { id: 'desarrolla', label: '1. Docente <-> Proyecto (Desarrolla)', endpoint: '/Desarrolla' },
  { id: 'docenteProducto', label: '2. Docente <-> Producto', endpoint: '/DocenteProducto' },
  { id: 'odsProyecto', label: '3. Proyecto <-> ODS', endpoint: '/OdsProyecto' },
  { id: 'proyectoLinea', label: '4. Proyecto <-> Línea Investigación', endpoint: '/ProyectoLinea' },
  { id: 'aliadoProyecto', label: '5. Proyecto <-> Aliado', endpoint: '/AliadoProyecto' },
  { id: 'palabrasClave', label: '6. Proyecto <-> Término Clave (Palabras Clave)', endpoint: '/PalabrasClave' },
  { id: 'aa_proyecto', label: '7. Proyecto <-> Área de Aplicación', endpoint: '/AaProyecto' },
  { id: 'ac_proyecto', label: '8. Proyecto <-> Área de Conocimiento', endpoint: '/AcProyecto' }
];

export default function FormTablasPuente() {
  const [tipoRelacion, setTipoRelacion] = useState('desarrolla');

  // Campos compartidos
  const [docenteCedula, setDocenteCedula] = useState('');
  const [proyectoId, setProyectoId] = useState('');
  const [productoId, setProductoId] = useState('');
  const [odsId, setOdsId] = useState('');
  const [lineaId, setLineaId] = useState('');
  const [aliadoNit, setAliadoNit] = useState('');
  const [terminoClave, setTerminoClave] = useState('');
  const [areaAplicacionId, setAreaAplicacionId] = useState('');
  const [areaConocimientoId, setAreaConocimientoId] = useState('');
  const [rolDocente, setRolDocente] = useState('');
  const [descripcion, setDescripcion] = useState('');

  const [cargando, setCargando] = useState(false);
  const [mensaje, setMensaje] = useState({ texto: '', tipo: '' });

  const limpiarCampos = () => {
    setDocenteCedula('');
    setProyectoId('');
    setProductoId('');
    setOdsId('');
    setLineaId('');
    setAliadoNit('');
    setTerminoClave('');
    setAreaAplicacionId('');
    setAreaConocimientoId('');
    setRolDocente('');
    setDescripcion('');
  };

  const manejarEnvio = async (e) => {
    e.preventDefault();
    setCargando(true);
    setMensaje({ texto: 'Vinculando...', tipo: 'exito' });

    try {
      let endpoint = '';
      let payload = {};

      switch (tipoRelacion) {
        case 'desarrolla':
          if (!docenteCedula || !proyectoId) {
            setMensaje({ texto: 'La cédula del docente y el ID del proyecto son obligatorios.', tipo: 'error' });
            setCargando(false);
            return;
          }
          endpoint = '/Desarrolla';
          payload = {
            docente_cedula: parseInt(docenteCedula, 10),
            proyecto_id: parseInt(proyectoId, 10),
            rol: rolDocente.trim() || null,
            descripcion: descripcion.trim() || null
          };
          break;

        case 'docenteProducto':
          if (!docenteCedula || !productoId) {
            setMensaje({ texto: 'La cédula del docente y el ID del producto son obligatorios.', tipo: 'error' });
            setCargando(false);
            return;
          }
          endpoint = '/DocenteProducto';
          payload = {
            docente_cedula: parseInt(docenteCedula, 10),
            producto_id: parseInt(productoId, 10)
          };
          break;

        case 'odsProyecto':
          if (!odsId || !proyectoId) {
            setMensaje({ texto: 'El ID del ODS y el ID del proyecto son obligatorios.', tipo: 'error' });
            setCargando(false);
            return;
          }
          endpoint = '/OdsProyecto';
          payload = {
            ods_id: parseInt(odsId, 10),
            proyecto_id: parseInt(proyectoId, 10)
          };
          break;

        case 'proyectoLinea':
          if (!lineaId || !proyectoId) {
            setMensaje({ texto: 'El ID de la línea y el ID del proyecto son obligatorios.', tipo: 'error' });
            setCargando(false);
            return;
          }
          endpoint = '/ProyectoLinea';
          payload = {
            linea_investigacion_id: parseInt(lineaId, 10),
            proyecto_id: parseInt(proyectoId, 10)
          };
          break;

        case 'aliadoProyecto':
          if (!aliadoNit || !proyectoId) {
            setMensaje({ texto: 'El NIT del aliado y el ID del proyecto son obligatorios.', tipo: 'error' });
            setCargando(false);
            return;
          }
          endpoint = '/AliadoProyecto';
          payload = {
            aliado_nit: parseInt(aliadoNit, 10),
            proyecto_id: parseInt(proyectoId, 10)
          };
          break;

        case 'palabrasClave':
          if (!proyectoId || !terminoClave || !terminoClave.trim()) {
            setMensaje({ texto: 'El ID del proyecto y el término clave son obligatorios.', tipo: 'error' });
            setCargando(false);
            return;
          }
          endpoint = '/PalabrasClave';
          payload = {
            proyecto_id: parseInt(proyectoId, 10),
            termino_clave: terminoClave.trim()
          };
          break;

        case 'aa_proyecto':
          if (!proyectoId || !areaAplicacionId) {
            setMensaje({ texto: 'El ID del proyecto y el ID del área de aplicación son obligatorios.', tipo: 'error' });
            setCargando(false);
            return;
          }
          endpoint = '/AaProyecto';
          payload = {
            proyecto_id: parseInt(proyectoId, 10),
            area_aplicacion_id: parseInt(areaAplicacionId, 10)
          };
          break;

        case 'ac_proyecto':
          if (!proyectoId || !areaConocimientoId) {
            setMensaje({ texto: 'El ID del proyecto y el ID del área de conocimiento son obligatorios.', tipo: 'error' });
            setCargando(false);
            return;
          }
          endpoint = '/AcProyecto';
          payload = {
            proyecto_id: parseInt(proyectoId, 10),
            area_conocimiento_id: parseInt(areaConocimientoId, 10)
          };
          break;

        default:
          throw new Error('Tipo de relación no válida.');
      }

      await api.post(endpoint, payload);
      setMensaje({ texto: '¡Asociación guardada exitosamente!', tipo: 'exito' });
      limpiarCampos();
    } catch (error) {
      setMensaje({
        texto: error.response?.data?.mensaje || 'Hubo un error al guardar la asociación.',
        tipo: 'error'
      });
    } finally {
      setCargando(false);
    }
  };

  return (
    <form onSubmit={manejarEnvio} className="flex flex-col h-full w-full">
      <div className="mb-8 font-sans w-full max-w-[800px]">
        <label className="text-[25px] font-bold text-gris-2 mb-3 block tracking-wide">
          Seleccione la Relación a Vincular:
        </label>
        <select
          value={tipoRelacion}
          onChange={(e) => {
            setTipoRelacion(e.target.value);
            limpiarCampos();
            setMensaje({ texto: '', tipo: '' });
          }}
          className="w-full h-[60px] bg-white rounded-none border border-gris-2 px-4 text-[18px] text-azul-2 focus:outline-none focus:border-naranja font-semibold"
        >
          {RELACIONES.map((rel) => (
            <option key={rel.id} value={rel.id}>
              {rel.label}
            </option>
          ))}
        </select>
      </div>

      {/* Renderizado dinámico según el tipo de relación */}
      {(tipoRelacion === 'desarrolla' || tipoRelacion === 'docenteProducto') && (
        <InputInstitucional
          label="Cédula del Docente *"
          name="docenteCedula"
          type="number"
          value={docenteCedula}
          onChange={(e) => setDocenteCedula(e.target.value)}
          placeholder="Ej. 1017123456"
          isValid={docenteCedula.length > 5}
        />
      )}

      {(
        tipoRelacion === 'desarrolla' ||
        tipoRelacion === 'odsProyecto' ||
        tipoRelacion === 'proyectoLinea' ||
        tipoRelacion === 'aliadoProyecto' ||
        tipoRelacion === 'palabrasClave' ||
        tipoRelacion === 'aa_proyecto' ||
        tipoRelacion === 'ac_proyecto'
      ) && (
        <InputInstitucional
          label="ID del Proyecto *"
          name="proyectoId"
          type="number"
          value={proyectoId}
          onChange={(e) => setProyectoId(e.target.value)}
          placeholder="Ej. 1"
          isValid={proyectoId.length > 0}
        />
      )}

      {tipoRelacion === 'docenteProducto' && (
        <InputInstitucional
          label="ID del Producto *"
          name="productoId"
          type="number"
          value={productoId}
          onChange={(e) => setProductoId(e.target.value)}
          placeholder="Ej. 5"
          isValid={productoId.length > 0}
        />
      )}

      {tipoRelacion === 'odsProyecto' && (
        <InputInstitucional
          label="ID del ODS *"
          name="odsId"
          type="number"
          value={odsId}
          onChange={(e) => setOdsId(e.target.value)}
          placeholder="Ej. 3 (Salud y Bienestar)"
          isValid={odsId.length > 0}
        />
      )}

      {tipoRelacion === 'proyectoLinea' && (
        <InputInstitucional
          label="ID de la Línea de Investigación *"
          name="lineaId"
          type="number"
          value={lineaId}
          onChange={(e) => setLineaId(e.target.value)}
          placeholder="Ej. 2"
          isValid={lineaId.length > 0}
        />
      )}

      {tipoRelacion === 'aliadoProyecto' && (
        <InputInstitucional
          label="NIT del Aliado *"
          name="aliadoNit"
          type="number"
          value={aliadoNit}
          onChange={(e) => setAliadoNit(e.target.value)}
          placeholder="Ej. 900123456"
          isValid={aliadoNit.length > 5}
        />
      )}

      {tipoRelacion === 'palabrasClave' && (
        <InputInstitucional
          label="Término Clave *"
          name="terminoClave"
          type="text"
          value={terminoClave}
          onChange={(e) => setTerminoClave(e.target.value)}
          placeholder="Ej. Inteligencia Artificial"
          isValid={terminoClave.trim().length > 0}
        />
      )}

      {tipoRelacion === 'aa_proyecto' && (
        <InputInstitucional
          label="ID del Área de Aplicación *"
          name="areaAplicacionId"
          type="number"
          value={areaAplicacionId}
          onChange={(e) => setAreaAplicacionId(e.target.value)}
          placeholder="Ej. 4"
          isValid={areaAplicacionId.length > 0}
        />
      )}

      {tipoRelacion === 'ac_proyecto' && (
        <InputInstitucional
          label="ID del Área de Conocimiento *"
          name="areaConocimientoId"
          type="number"
          value={areaConocimientoId}
          onChange={(e) => setAreaConocimientoId(e.target.value)}
          placeholder="Ej. 2"
          isValid={areaConocimientoId.length > 0}
        />
      )}

      {tipoRelacion === 'desarrolla' && (
        <>
          <InputInstitucional
            label="Rol del Docente en el Proyecto (Opcional)"
            name="rolDocente"
            value={rolDocente}
            onChange={(e) => setRolDocente(e.target.value)}
            placeholder="Ej. Investigador Principal / Co-investigador"
            isValid={true}
          />
          <InputInstitucional
            label="Descripción de la Participación (Opcional)"
            name="descripcion"
            value={descripcion}
            onChange={(e) => setDescripcion(e.target.value)}
            placeholder="Breve detalle de las funciones..."
            isValid={true}
          />
        </>
      )}

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
