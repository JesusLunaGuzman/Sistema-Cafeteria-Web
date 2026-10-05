using CafeteriaWeb.Models;

namespace CafeteriaWeb.Repositories
{
    public interface IProductoRepository
    {
        Task<IEnumerable<Producto>> Listar();
        Task Registrar(Producto producto);
        Task<Producto?> ObtenerPorId(int id);
        Task Editar(Producto producto);
    }
}