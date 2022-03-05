import FormularioActores from "./FormularioActores";

export default function CrearActores() {
  return (
    <>
      Crear Actor
      <FormularioActores
        modelo={{ nombre: "", fechaNacimiento: undefined }}
        onSubmit={(valores) => console.log(valores)}
      />
    </>
  );
}
