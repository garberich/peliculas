using System.Collections.Generic;
using System.Threading.Tasks;
using peliculasApi.Entidades;

namespace peliculasApi.Repositorio
{
    public interface IRepositorio_Old
    {
        Task<Genero_Old> ObtenerPorId(int id);
        List<Genero_Old> ObtenerTodosLosGeneros();
    }
}