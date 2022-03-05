import { cineDTO } from "../cines/cines.model";
import { generoDTO } from "../generos/generos.model";
import FormularioPeliculas from "./FormularioPeliculas";

export default function CrearPeliculas() {
  const generos: generoDTO[] = [
    { id: 1, nombre: "Acción" },
    { id: 2, nombre: "Drama" },
    { id: 3, nombre: "Comedia" },
  ];

  const cines: cineDTO[] =[
    { id: 1, nombre: "Cine Colombia" },
    { id: 2, nombre: "Royal Films" }
  ];

  return (
    <>
      <h3>Crear Película</h3>
      <FormularioPeliculas
      actoresSeleccionados={[]}
        modelo={{ titulo: "", enCines: false, trailer: "" }}
        onSubmit={(valores) => console.log(valores)}
        generosSeleccionados={[]}
        generosNoSeleccionados={generos}
        cinesNoSeleccionados={cines}
        cinesSeleccionados={[]}
      />
    </>
  );
}
