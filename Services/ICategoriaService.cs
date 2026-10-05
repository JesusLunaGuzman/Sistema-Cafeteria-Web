using CafeteriaWeb.Dtos;

namespace CafeteriaWeb.Services
{
    public interface ICategoriaService
    {
        Task<IEnumerable<CategoriaDto>> Listar();
        Task Registrar(CategoriaDto dto);
        Task<CategoriaDto?> ObtenerPorId(int id);
        Task Editar(CategoriaDto dto);
    }
}