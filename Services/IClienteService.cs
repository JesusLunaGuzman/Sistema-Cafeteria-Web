using CafeteriaWeb.Dtos;

namespace CafeteriaWeb.Services
{
    public interface IClienteService
    {
        Task<IEnumerable<ClienteDto>> Listar();
        Task Registrar(ClienteDto dto);
        Task<ClienteDto?> ObtenerPorId(int id);
        Task Editar(ClienteDto dto);
    }
}