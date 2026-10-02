import React from 'react';
import { CheckCircle2, XCircle } from 'lucide-react';

export default function InputInstitucional({ label, name, value, onChange, placeholder, isValid, hasError }) {
  return (
    <div className="flex flex-col mb-8 font-sans w-full max-w-[800px]">
      <label 
        htmlFor={name} 
        className="text-[25px] font-bold text-gris-2 mb-3 tracking-wide"
      >
        {label}
      </label>
      
      <div className="relative flex items-center">
        <input
          type="text"
          id={name}
          name={name}
          value={value}
          onChange={onChange}
          placeholder={placeholder}
          className="w-full h-[60px] bg-white rounded-none border border-gris-2 px-4 text-[20px] text-azul-2 focus:outline-none focus:border-naranja transition-colors"
        />
        
        {/* Validation Icons (Simulated as in PDF) */}
        <div className="absolute right-[-70px] flex gap-2">
          {isValid ? (
             <CheckCircle2 size={30} className="text-naranja fill-naranja/20" />
          ) : (
             <CheckCircle2 size={30} className="text-naranja opacity-30" />
          )}
          {hasError ? (
             <XCircle size={30} className="text-naranja fill-naranja/20" />
          ) : (
             <XCircle size={30} className="text-naranja opacity-30" />
          )}
        </div>
      </div>
    </div>
  );
}