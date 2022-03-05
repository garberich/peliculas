import FormularioCines from "./FormularioCines";

export default function EditarCines(){
    return(
        <>
            <h3>Editar Cine</h3>
            <FormularioCines 
                modelo={{nombre: "Sambi", latitud: 6.258410, longitud: -75.550156}}
                onSubmit={valores => console.log(valores)}
            />
        </>
    )
}