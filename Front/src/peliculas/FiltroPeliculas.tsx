import { Field, Form, Formik } from "formik";
import { generoDTO } from "../generos/generos.model";
import Button from "../utils/Button";

export default function FiltroPeliculas() {
  const valorInicial: filtroPeliculasForm = {
    titulo: "",
    generoId: 0,
    proximosEstrenos: false,
    enCines: false,
  };

  const generos: generoDTO[] = [
    { id: 1, nombre: "Acción" },
    { id: 2, nombre: "Drama" },
  ];

  // Para que formik funcione con un input type text necesita ciertas propiedades
  // por el ejemplo en el onchange se actualice el valor del formulario, etc
  // con ...formikProps.getFieldProps se hace esto en una sola linea
  return (
    <>
      <h3>Filtrar Películas</h3>
      <Formik
        initialValues={valorInicial}
        onSubmit={(valores) => console.log(valores)}
      >
        {(formikProps) => (
          <Form>
            <div className="form-inline">
              <div className="form-group mb-2">
                <label htmlFor="titulo" className="sr-only">
                  Titulo
                </label>
                <input
                  type="text"
                  className="form-control"
                  id="titulo"
                  placeholder="titulo de la película"
                  {...formikProps.getFieldProps("titulo")}
                />
              </div>
              <div className="form-group mx-sm-3 mb-2">
                <select
                  className="form-control"
                  {...formikProps.getFieldProps("generoId")}
                >
                  <option value="0">--Seleccione un género--</option>
                  {generos.map((genero) => (
                    <option key={genero.id} value={genero.id}>
                      {genero.nombre}
                    </option>
                  ))}
                </select>
              </div>
              <div className="form-group mx-sm-3 mb-2">
                <Field
                  className="form-check-input"
                  id="proximosEstrenos"
                  name="proximosEstrenos"
                  type="checkbox"
                />
                <label className="form-check-label" htmlFor="proximosEstrenos">
                  Próximos Estrenos
                </label>
              </div>
              <div className="form-group mx-sm-3 mb-2">
                <Field
                  className="form-check-input"
                  id="enCines"
                  name="enCines"
                  type="checkbox"
                />
                <label className="form-check-label" htmlFor="enCines">
                  En Cines
                </label>
              </div>
              <Button
                onClick={() => formikProps.submitForm()}
                className="btn btn-primary mb-2 mx-sm-3"
              >
                Filtrar
              </Button>
              <Button
                onClick={() => formikProps.setValues(valorInicial)}
                className="btn btn-danger mb-2"
              >
                Limpiar
              </Button>
            </div>
          </Form>
        )}
      </Formik>
    </>
  );
}

interface filtroPeliculasForm {
  titulo: string;
  generoId: number;
  proximosEstrenos: boolean;
  enCines: boolean;
}
