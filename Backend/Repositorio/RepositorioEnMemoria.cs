using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using peliculasApi.Entidades;

namespace peliculasApi.Repositorio
{
    public class RepositorioEnMemoria : IRepositorio_Old
    {
        private List<Genero_Old> _generos;
        public RepositorioEnMemoria()
        {
            _generos = new List<Genero_Old>(){
                new Genero_Old(){Id = 1, Nombre = "Comedia"},
                new Genero_Old(){Id = 2, Nombre = "Acción"}
            };
        }

        public List<Genero_Old> ObtenerTodosLosGeneros()
        {
            return _generos;
        }

        public async Task<Genero_Old> ObtenerPorId(int id)
        {
            await Task.Delay(TimeSpan.FromSeconds(3));
            return _generos.FirstOrDefault(x => x.Id == id);
        }
    }
}