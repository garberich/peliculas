using System.ComponentModel.DataAnnotations;
using peliculasApi.Validaciones;

namespace peliculasApi.Entidades
{
    public class Genero
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")] //Valida que el nombre sea requerido
        [StringLength(maximumLength: 50)]
        [PrimeraLetraMayuscula] //ejecutar la validacion por atributo
        public string Nombre { get; set; }
    }
}