import { useFormikContext } from "formik";
import MostrarErrorCampo from "./MostrarErrorCampo";

export default function FormGroupFecha(props: formGroupFechasProps) {
  // Si queremos obtener por ejemplo la fecha de nacimiento del actor que estamos editando debemos usar un contexto que nos
  // provee formik para extraer información. Con un contexto tenemos un proveedor que nos provee valores
  // Desde cualquier componente interno de formik podemos acceder a los valores que nos da el contexto

  const { values, validateForm, touched, errors } = useFormikContext<any>();
  return (
    <div className="form-group">
      <label htmlFor={props.campo}>{props.label}</label>
      <input
        type="date"
        className="form-control"
        id={props.campo}
        name={props.campo}
        defaultValue={values[props.campo]?.toLocaleDateString('en-CA')}
        onChange={e => {
            const fecha = new Date(e.currentTarget.value + 'T00:00:00');
            // con esto actualizamos el formulario
            values[props.campo] = fecha;
            validateForm();
        }}
      />
      {touched[props.campo] && errors[props.campo] ?
      <MostrarErrorCampo mensaje={errors[props.campo]?.toString()!} />: null}
    </div>
  );
}

interface formGroupFechasProps {
  campo: string;
  label: string;
}
