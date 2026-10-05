using CafeteriaWeb.Models;

namespace CafeteriaWeb.Repositories
{
    public interface IPedidoRepository
    {
        Task<IEnumerable<Pedido>> ListarPedidos();
        Task RegistrarVentaTransaccion(Pedido pedido, List<DetallePedido> detalles);
        Task AnularPedido(int idPedido);
    }
}