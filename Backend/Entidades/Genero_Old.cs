using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using peliculasApi.Validaciones;

namespace peliculasApi.Entidades
{
    public class Genero_Old: IValidatableObject //Esto realiza las validaciones personalizadas por modelo y no por atributos
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")] //Valida que el nombre sea requerido
        [StringLength(maximumLength: 10)]
        //[PrimeraLetraMayuscula] //ejecutar la validacion por atributo
        public string Nombre { get; set; }

        //Esta validacion por modelo solo se ejcuta si todas las reglas de validacion de atributos pasan, por ejemplo required y StringLength
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(Nombre))
            {
                var primeraLetra = Nombre[0].ToString();

                if(primeraLetra != primeraLetra.ToUpper())
                {
                    yield return new ValidationResult("Primera letra debe ser mayúscula", new string[] {nameof(Nombre)});
                }
            }
        }
    }
}