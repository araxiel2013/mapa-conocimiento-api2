const fs = require('fs');
const path = require('path');
const formsDir = path.join(process.cwd(), 'src/components/formularios');
const files = fs.readdirSync(formsDir).filter(f => f.startsWith('Form') && f.endsWith('.jsx') && f !== 'FormProyecto.jsx' && f !== 'FormAreaConocimiento.jsx');

files.forEach(file => {
  let content = fs.readFileSync(path.join(formsDir, file), 'utf8');
  content = content.replace(/import '\.\/Formularios\.css';\n?/g, '');
  content = content.replace(/className="formulario-seccion"/g, 'className="flex flex-col h-full w-full"');
  content = content.replace(/<h2 className="titulo-seccion">.*?<\/h2>\n?/g, '');
  
  // Replace the submit button
  content = content.replace(/<button type="submit" className="boton-funcional"[\s\S]*?<\/button>/g, '<div className="mt-auto pt-10"><button type="submit" className="hidden" id="btn-submit">Guardar</button></div>');
  
  // Update the p tag for mensaje
  content = content.replace(/<p style={{[\s\S]*?}}>/g, '<p className={`text-xl ${mensaje.tipo === \'exito\' ? \'text-naranja\' : \'text-red-500\'} mt-4`}>');

  // Add isValid simulation to InputInstitucional tags by matching name
  content = content.replace(/<InputInstitucional([\s\S]*?)name="([^"]+)"([\s\S]*?)\/>/g, (match, p1, name, p3) => {
     if (match.includes('isValid=')) return match;
     return `<InputInstitucional${p1}name="${name}"${p3} isValid={${name}.length > 2} />`;
  });

  fs.writeFileSync(path.join(formsDir, file), content);
});
console.log('Forms updated successfully.');
