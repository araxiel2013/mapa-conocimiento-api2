import React, { useState } from 'react';
import { 
  Mail, 
  Phone, 
  MapPin, 
  Clock, 
  Send, 
  CheckCircle2, 
  AlertCircle, 
  MessageSquare,
  Building
} from 'lucide-react';

export default function Contacto() {
  const [formulario, setFormulario] = useState({
    nombre: '',
    correo: '',
    dependencia: '',
    asunto: '',
    mensaje: ''
  });

  const [enviando, setEnviando] = useState(false);
  const [enviadoExito, setEnviadoExito] = useState(false);
  const [errorValidacion, setErrorValidacion] = useState('');

  const handleChange = (e) => {
    setFormulario({ ...formulario, [e.target.name]: e.target.value });
    if (errorValidacion) setErrorValidacion('');
  };

  const manejarEnvio = (e) => {
    e.preventDefault();

    if (!formulario.nombre.trim()) {
      setErrorValidacion('Por favor ingresa tu nombre completo.');
      return;
    }

    if (!formulario.correo.trim() || !formulario.correo.includes('@')) {
      setErrorValidacion('Por favor ingresa un correo electrónico institucional válido.');
      return;
    }

    if (!formulario.asunto.trim()) {
      setErrorValidacion('Por favor especifica el asunto de tu consulta.');
      return;
    }

    if (!formulario.mensaje.trim() || formulario.mensaje.trim().length < 10) {
      setErrorValidacion('El mensaje debe tener al menos 10 caracteres.');
      return;
    }

    setEnviando(true);
    setErrorValidacion('');

    // Simulación de envío exitoso con feedback en cliente
    setTimeout(() => {
      setEnviando(false);
      setEnviadoExito(true);
      setFormulario({
        nombre: '',
        correo: '',
        dependencia: '',
        asunto: '',
        mensaje: ''
      });
    }, 1000);
  };

  return (
    <div className="flex flex-col py-12 lg:py-20 bg-slate-50">
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 space-y-16">
        
        {/* =========================================================
            ENCABEZADO
        ========================================================= */}
        <div className="text-center max-w-3xl mx-auto space-y-4">
          <div className="inline-flex items-center gap-2 px-3.5 py-1.5 rounded-full bg-amber-100 text-[#Eb8600] text-xs font-bold uppercase tracking-wider">
            <MessageSquare size={14} />
            <span>Canales de Atención</span>
          </div>
          <h1 className="text-3xl sm:text-5xl font-black text-[#0D274C] tracking-tight">
            Contacto y Mesa de Ayuda
          </h1>
          <p className="text-base sm:text-lg text-slate-600 leading-relaxed">
            Comunícate con la Vicerrectoría de Investigaciones para solicitar acceso, resolver dudas sobre homologación Minciencias o soporte técnico.
          </p>
        </div>

        {/* =========================================================
            GRID: FORMULARIO + TARJETAS DE INFORMACIÓN
        ========================================================= */}
        <div className="grid grid-cols-1 lg:grid-cols-12 gap-12 items-start max-w-6xl mx-auto">
          
          {/* Formulario de Contacto (Columna 7/12) */}
          <div className="lg:col-span-7 bg-white rounded-3xl p-8 sm:p-10 border border-slate-200 shadow-sm space-y-6">
            <div>
              <h2 className="text-2xl font-bold text-[#0D274C] mb-2">
                Envíanos tu Mensaje
              </h2>
              <p className="text-sm text-slate-500">
                Diligencia el formulario y te responderemos en un plazo máximo de 24 horas hábiles.
              </p>
            </div>

            {enviadoExito && (
              <div className="p-5 rounded-2xl bg-emerald-50 border border-emerald-200 text-emerald-800 flex items-start gap-3">
                <CheckCircle2 size={24} className="text-emerald-600 shrink-0 mt-0.5" />
                <div className="text-sm">
                  <p className="font-bold">¡Mensaje enviado con éxito!</p>
                  <p className="mt-1 text-emerald-700">
                    Tu solicitud ha sido radicada correctamente. Nuestro equipo se pondrá en contacto a tu correo institucional.
                  </p>
                </div>
              </div>
            )}

            {errorValidacion && (
              <div className="p-4 rounded-xl bg-rose-50 border border-rose-200 text-rose-700 flex items-center gap-3 text-sm font-semibold">
                <AlertCircle size={20} className="shrink-0" />
                <span>{errorValidacion}</span>
              </div>
            )}

            <form onSubmit={manejarEnvio} className="space-y-5">
              <div>
                <label className="block text-xs font-bold uppercase tracking-wider text-slate-600 mb-2">
                  Nombre Completo *
                </label>
                <input
                  type="text"
                  name="nombre"
                  value={formulario.nombre}
                  onChange={handleChange}
                  placeholder="Ej. Dr. Carlos Mario Gómez"
                  className="w-full px-4 py-3 rounded-xl border border-slate-300 focus:outline-none focus:border-[#Eb8600] text-sm text-slate-800"
                />
              </div>

              <div>
                <label className="block text-xs font-bold uppercase tracking-wider text-slate-600 mb-2">
                  Correo Electrónico Institucional *
                </label>
                <input
                  type="email"
                  name="correo"
                  value={formulario.correo}
                  onChange={handleChange}
                  placeholder="ejemplo@usb.edu.co"
                  className="w-full px-4 py-3 rounded-xl border border-slate-300 focus:outline-none focus:border-[#Eb8600] text-sm text-slate-800"
                />
              </div>

              <div>
                <label className="block text-xs font-bold uppercase tracking-wider text-slate-600 mb-2">
                  Facultad o Dependencia Académica (Opcional)
                </label>
                <input
                  type="text"
                  name="dependencia"
                  value={formulario.dependencia}
                  onChange={handleChange}
                  placeholder="Ej. Facultad de Ingeniería / Grupo GIDIT"
                  className="w-full px-4 py-3 rounded-xl border border-slate-300 focus:outline-none focus:border-[#Eb8600] text-sm text-slate-800"
                />
              </div>

              <div>
                <label className="block text-xs font-bold uppercase tracking-wider text-slate-600 mb-2">
                  Asunto *
                </label>
                <input
                  type="text"
                  name="asunto"
                  value={formulario.asunto}
                  onChange={handleChange}
                  placeholder="Ej. Solicitud de Creación de Usuario / Duda en ODS"
                  className="w-full px-4 py-3 rounded-xl border border-slate-300 focus:outline-none focus:border-[#Eb8600] text-sm text-slate-800"
                />
              </div>

              <div>
                <label className="block text-xs font-bold uppercase tracking-wider text-slate-600 mb-2">
                  Mensaje o Descripción de la Solicitud *
                </label>
                <textarea
                  name="mensaje"
                  rows={4}
                  value={formulario.mensaje}
                  onChange={handleChange}
                  placeholder="Describe con detalle tu requerimiento, indicando ID de proyecto o documento de identidad si aplica..."
                  className="w-full px-4 py-3 rounded-xl border border-slate-300 focus:outline-none focus:border-[#Eb8600] text-sm text-slate-800 resize-none"
                ></textarea>
              </div>

              <button
                type="submit"
                disabled={enviando}
                className="w-full flex items-center justify-center gap-2 bg-[#Eb8600] hover:bg-[#d47800] text-white py-3.5 rounded-xl font-bold text-sm shadow-md transition-all cursor-pointer disabled:opacity-50"
              >
                <Send size={18} />
                <span>{enviando ? 'Enviando Solicitud...' : 'Radicar Mensaje'}</span>
              </button>
            </form>
          </div>

          {/* Tarjetas de Información de Contacto (Columna 5/12) */}
          <div className="lg:col-span-5 space-y-6">
            
            {/* Tarjeta Sede Principal */}
            <div className="bg-white rounded-3xl p-8 border border-slate-200 shadow-sm space-y-4">
              <div className="flex items-center gap-3">
                <div className="w-10 h-10 rounded-xl bg-amber-100 text-[#Eb8600] flex items-center justify-center">
                  <Building size={20} />
                </div>
                <div>
                  <h3 className="font-bold text-base text-[#0D274C]">Vicerrectoría de Investigaciones</h3>
                  <p className="text-xs text-slate-500">Sede Principal Bogotá</p>
                </div>
              </div>

              <div className="space-y-3 text-xs sm:text-sm text-slate-600 pt-2 border-t border-slate-100">
                <p className="flex items-start gap-3">
                  <MapPin size={18} className="text-[#Eb8600] shrink-0 mt-0.5" />
                  <span>Carrera 8H No. 172-20, Campus Universitario San Buenaventura. Edificio San Francisco.</span>
                </p>
                <p className="flex items-center gap-3">
                  <Phone size={18} className="text-[#Eb8600] shrink-0" />
                  <span>PBX: +57 (601) 667 3535 Ext. 2100 / 2105</span>
                </p>
                <p className="flex items-center gap-3">
                  <Mail size={18} className="text-[#Eb8600] shrink-0" />
                  <span>investigaciones@usbbog.edu.co</span>
                </p>
              </div>
            </div>

            {/* Tarjeta Horarios */}
            <div className="bg-white rounded-3xl p-8 border border-slate-200 shadow-sm space-y-3">
              <div className="flex items-center gap-3 mb-2">
                <div className="w-10 h-10 rounded-xl bg-blue-100 text-[#0D274C] flex items-center justify-center">
                  <Clock size={20} />
                </div>
                <div>
                  <h3 className="font-bold text-base text-[#0D274C]">Horarios de Atención</h3>
                  <p className="text-xs text-slate-500">Presencial y Virtual</p>
                </div>
              </div>

              <div className="text-xs sm:text-sm text-slate-600 space-y-1.5 pt-2 border-t border-slate-100">
                <p><strong>Lunes a Viernes:</strong> 8:00 a.m. a 1:00 p.m. y 2:00 p.m. a 5:30 p.m.</p>
                <p><strong>Sábados:</strong> Con cita previa en comités de investigación.</p>
              </div>
            </div>

            {/* Tarjeta Multicampus */}
            <div className="bg-[#0D274C] text-white rounded-3xl p-8 shadow-md space-y-3">
              <h3 className="font-bold text-base text-amber-400">Presencia Nacional Multicampus</h3>
              <p className="text-xs text-slate-300 leading-relaxed">
                El Módulo Mapa de Conocimiento atiende los programas de investigación de las sedes y seccionales en 
                Bogotá, Medellín, Cali y Cartagena.
              </p>
            </div>

          </div>

        </div>

      </div>
    </div>
  );
}

