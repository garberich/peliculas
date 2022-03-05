using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using peliculasApi.ApiBehavior;
using peliculasApi.Filtros;
using peliculasApi.Repositorio;

namespace peliculasApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Para conectarnos a la BD. defaultConnection se encuentra en appsettings.Development.json
            services.AddDbContext<ApplicationDbContext>(options => 
            options.UseSqlServer(Configuration.GetConnectionString("defaultConnection")));
            /*
                Existen 3 ciclos de vida que puede tener un servicio:
                    AddTransiet - es el tiempo mas corto de vida que se le puede dar a un servicio. Retorna una nueva instancia siempre
                    AddScoped - el tiempo de vida de la clase instanciada va a ser durante toda la peticion http
                    AddSingleton - el tiempo de vida de la instancia del servicio va a ser durante el tiempo de ejeecucion de la aplicacion
            */
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
            // services.AddTransient<IRepositorio_Old, RepositorioEnMemoria>(); //Inyeccion de dependencias
            // services.AddTransient<MiFiltroDeAccion>(); //Inyeccion de dependencias para usar el filtro personalizado en la clase Generos
            services.AddControllers(options => {
                options.Filters.Add(typeof(FiltroDeExcepcion));//Con esto registramos el filtro a nivel global
                options.Filters.Add(typeof(ParsearBadRequest)); // Con esto se captura cuando se retorna un BadRequest de una accion
            })
            /*
                Como en los controladores se usa ApiController se controlan automaticamente los errores.
                A esto se le llama "el comportamiento de la API"
                Por lo tanto se debe configurar el comportamiento del API para que haga lo mismo que tenemos en BadRequest
                porque la validacion automatica en las reglas de validacion a nivel de modelo de la entidad Genero ocurren fuera de los filtros
            */
            .ConfigureApiBehaviorOptions(BehaviorBadRequests.Parsear);

            /*
                Con esto configuramos los CORS obteniendo la URL desde la que dejaremos consumir el servicio
                Esta URL la tenemos mapeada en el archivo appsettings.Development.json
            */
            services.AddCors(options => {
                var frontendURL = Configuration.GetValue<string>("frontend_url");
                options.AddDefaultPolicy(builder => {
                    builder.WithOrigins(frontendURL).AllowAnyMethod().AllowAnyHeader();
                });
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "peliculasApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)//, ILogger<Startup> logger)
        {
            /*
            
                    Existen 2 tipos: los que se ejecutan e interrumpen el pipeline y los que envían respuesta hacia otros middleware.
                    Por convención, los que inician con Use son los que no detienen el proceso, ejemplo:
                        UseSwagger, UseAuthorization, UseRouting, etc.
                

                // Creamos un middleware personalizado para guardar en un log la respuesta http hacia el usuario
                // Usamos el Use porque es un middleware que no detiene el proceso
                app.Use(async (context, next) => {
                    using(var swapStream = new MemoryStream())
                    {
                        var respuestaOriginal = context.Response.Body;
                        context.Response.Body = swapStream;

                        // Con esto se dice que se quiere que se continúe la ejecución del pipeline
                        await next.Invoke();

                        //Lo que viene después del next.Invoke es la respuesta que me devuelven los demás middleware

                        //Como me devuelven la respuesta pues ya puedo trabajar con el string
                        swapStream.Seek(0, SeekOrigin.Begin); //Para llevar la respuesta al principio del MemoryStream
                        string respuesta = new StreamReader(swapStream).ReadToEnd();
                        swapStream.Seek(0, SeekOrigin.Begin);

                        await swapStream.CopyToAsync(respuestaOriginal);//Despues de obtener la respuesta http la copiamos a la respuesta original
                        context.Response.Body = respuestaOriginal;

                        logger.LogInformation(respuesta);//Se guarda en un log la respuesta http
                    }
                });
            */

            /*
                // middleware que solo se ejecuta si se intenta ingresar a la ruta mapa1/
                app.Map("mapa1/", (app) =>
                {
                    // Middleware que intercepta e interrumpe el pipeline
                    app.Run(async context =>
                    {
                        await context.Response.WriteAsync("Estoy interceptando el pipeline");
                    });
                });
            */

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "peliculasApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
