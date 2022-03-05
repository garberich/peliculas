using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace peliculasApi.Filtros
{
    public class FiltroDeExcepcion : ExceptionFilterAttribute
    {
        public ILogger<FiltroDeExcepcion> logger { get; set; }
        public FiltroDeExcepcion(ILogger<FiltroDeExcepcion> logger)
        {
            this.logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            //Con esto se proceso a través del ILogger cualquier error que no haya sido atrapado por un try catch
            logger.LogError(context.Exception, context.Exception.Message);
            base.OnException(context);
        }
    }
}