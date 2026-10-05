using CafeteriaWeb.Models;

namespace CafeteriaWeb.Repositories
{
    public interface ICategoriaRepository
    {
        Task<IEnumerable<CategoriaProducto>> Listar();
        Task Registrar(CategoriaProducto categoria);
        Task<CategoriaProducto?> ObtenerPorId(int id);
        Task Editar(CategoriaProducto categoria);
    }
}