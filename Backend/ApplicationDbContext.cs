using Microsoft.EntityFrameworkCore;
using peliculasApi.Entidades;

namespace peliculasApi
{
    /*
        El DBContext es la pieza central de entityframework core
        A través de este se accede a las diferentes tablas de la BD
    */
    public class ApplicationDbContext : DbContext
    {
        /* 
            El DbContextOptions se envía a través de la clase startup
        */
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Genero> Generos {get; set;}
    }
}