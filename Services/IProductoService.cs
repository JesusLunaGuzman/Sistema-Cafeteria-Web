using CafeteriaWeb.Dtos;

namespace CafeteriaWeb.Services
{
    public interface IProductoService
    {
        Task<IEnumerable<ProductoDto>> Listar();
        Task Registrar(ProductoDto dto);
        Task<ProductoDto?> ObtenerPorId(int id);
        Task Editar(ProductoDto dto);
    }
}