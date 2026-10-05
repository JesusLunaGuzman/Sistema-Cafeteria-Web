using CafeteriaWeb.Dtos;

namespace CafeteriaWeb.Services
{
    public interface IPedidoService
    {
        Task<IEnumerable<PedidoDto>> ListarPedidos();
        Task RegistrarVenta(PedidoDto pedidoDto);
        Task AnularPedido(int idPedido);
    }
}