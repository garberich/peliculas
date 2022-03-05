using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using peliculasApi.Entidades;
using peliculasApi.Filtros;
using peliculasApi.Repositorio;

namespace peliculasApi.Controllers
{
    [Route("api/generos_old")]
    [ApiController] //Atributo que automaticamente me realiza las validaciones del ModelState
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] //Es un filtro de autorización. Se implementa por medio de un middleware. Para este filtro se usa un paquete de nuget de JWT
    public class GenerosController_Old: ControllerBase
    {
        private readonly IRepositorio_Old repositorio;
        public ILogger<GenerosController_Old> _logger { get; }

        public GenerosController_Old(IRepositorio_Old repositorio, ILogger<GenerosController_Old> logger)
        {
            this.repositorio = repositorio;

            /*
                El servicio ILogger se usa para guardar los logs y podemos guardar diferentes tipos de logs en el siguiente orden:
                    Critical
                    Error
                    Warning
                    Information
                    Debug
                    Trace
                En el archivo appsettings se define qué tipos de logs quiero ver en el output de la consola cuando la aplicación esté corriendo
            */
            _logger = logger;
        }


        [HttpGet]//api/generos
        [HttpGet("listado")]//api/generos/listado
        [HttpGet("/listadogeneros_old")]// /listadogeneros
        /*
            ServiceFilter inicializa el servicio y sus dependencias. Para usar MiFiltroDeAccion con el sistema de inyeccion de dependencias
            debemos configurarlo en startup
        */
        //[ServiceFilter(typeof(MiFiltroDeAccion))]
        public ActionResult<List<Genero_Old>> Get()
        {
            _logger.LogInformation("Vamos a mostrar los géneros");
            return repositorio.ObtenerTodosLosGeneros();
        }

        [HttpGet("{id:int}")]//api/generos/2
        // [HttpGet("{id:int}/{nombre=Roberto}")]//api/generos/2/Roberto (El "Roberto" no es necesario mandarlo pues ya está por defecto)
        public async Task<ActionResult<Genero_Old>> Get(int id)
        {
            _logger.LogInformation($"Obteniendo el género {id}");
            /*
                Si se agrega el parámetro de "nombre" de la siguiente manera: [BindRequired] string nombre
                pero el parámetro "nombre" no es enviado se va a generar un error en el ModelState
                El Modelstate valida que no haya errores en el Biding

            if(!ModelState.IsValid) //Se puede borrar esta validacion ya que el ApiController se encarga de hacerla automaticamente
            {
                Se retorna un error de tipo Biding
                return BadRequest(ModelState);
            }
            */

            var genero = await repositorio.ObtenerPorId(id);

            if (genero == null)
            {
                _logger.LogWarning($"No se encontró el genero {id}");
                // Gracias a que heredamos del ControllerBase podemos usar métodos auxiliares como NotFound
                return NotFound();
            }

            /*
                Un metodo puede retornar 3 valores diferentes
                    - Tipo Especifico
                    - ActionResult<t>
                    - IActionResult
                ActionResult es un tipo base que representa cualquier tipo de retorno de una acción
                Al retornar ActionResult<Genero> decimos que podemos retornar un tipo Genero o un tipo ActionResult
                El problema con IActionresult es que permitiría devolver cualquier cosa
                como por ejemplo Ok(genero) o Ok("Hola")
            */
            return genero;
        }

        [HttpPost]
        public ActionResult Post([FromBody]Genero_Old genero)
        {
            /*
                validaciones por atributor
                    - Required: es requerido
                    - StringLength: Tamaño minimo y maximo de un string
                    - Range: Rango de un numero. Minimo y maximo
                    - CreditCard: Valida la estructura numerica de una tarjeta de credito
                    - Compare: Valida si dos valores son iguales. Por ejemplo para comparar el password
                    - Phone: Valida un formato de telefono
                    - RegularExpression: valida contra una expresion regular
                    - Url: valida un formato de URL
                    - BindRequired: Indica si se requiere un Binding hacia la propiedad
                Las validaciones se hacen contra el ModelState
                Con el atributo ApiController se realizan las validaciones del ModelState automaticamente
            */
            return NoContent();
        }
    }
}