using CafeteriaWeb.Models;

namespace CafeteriaWeb.Repositories
{
    public interface IClienteRepository
    {
        Task<IEnumerable<Cliente>> Listar();
        Task Registrar(Cliente cliente);
        Task<Cliente?> ObtenerPorId(int id);
        Task Editar(Cliente cliente);
    }
}