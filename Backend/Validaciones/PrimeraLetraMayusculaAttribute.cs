using System.ComponentModel.DataAnnotations;

namespace peliculasApi.Validaciones
{
    public class PrimeraLetraMayusculaAttribute: ValidationAttribute //Se hereda de esta para poder usar la clase como validacion personalizada
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //No valido si es diferente de null porque esa validacion ya la hace el atributo Required del modelo
            if(value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return ValidationResult.Success;
            }

            var primeraLetra = value.ToString()[0].ToString();

            if(primeraLetra != primeraLetra.ToUpper())
            {
                return new ValidationResult("La primera letra debe ser mayúscula");
            }

            return ValidationResult.Success;
        }
    }
}