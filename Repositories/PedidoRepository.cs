using CafeteriaWeb.Data;
using CafeteriaWeb.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace CafeteriaWeb.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly AppDbContext _context;

        public PedidoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Pedido>> ListarPedidos()
        {
            var pedidos = await _context.Pedidos
                .FromSqlRaw("EXEC sp_ListarPedidos")
                .ToListAsync();
            await _context.Clientes.ToListAsync();
            return pedidos;
        }

        public async Task RegistrarVentaTransaccion(Pedido pedido, List<DetallePedido> detalles)
        {
            using var transaccion = await _context.Database.BeginTransactionAsync();
            try
            {
                var pIdCliente = new SqlParameter("@IdCliente", pedido.IdCliente);

                // PROTECCION: SI EL ID DEL USUARIO LLEGA EN CERO O VACIO, LE ASIGNAMOS EL 1 (ADMINISTRADOR)
                int idUsuarioReal = pedido.IdUsuario <= 0 ? 1 : pedido.IdUsuario;
                var pIdUsuario = new SqlParameter("@IdUsuario", idUsuarioReal);

                var pTotal = new SqlParameter("@Total", pedido.Total);
                var pEstado = new SqlParameter("@Estado", pedido.Estado);
                var pIdPedidoOut = new SqlParameter("@IdPedido", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };

                // EJECUTAMOS EL PROCEDIMIENTO CON LOS PARAMETROS SEGUROS
                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC sp_RegistrarPedido @IdCliente, @IdUsuario, @Total, @Estado, @IdPedido OUTPUT",
                    pIdCliente, pIdUsuario, pTotal, pEstado, pIdPedidoOut);

                int nuevoIdPedido = (int)pIdPedidoOut.Value;

                foreach (var det in detalles)
                {
                    await _context.Database.ExecuteSqlRawAsync(
                        "EXEC sp_RegistrarDetallePedido @IdPedido, @IdProducto, @Cantidad, @Precio, @Subtotal",
                        new SqlParameter("@IdPedido", nuevoIdPedido),
                        new SqlParameter("@IdProducto", det.IdProducto),
                        new SqlParameter("@Cantidad", det.Cantidad),
                        new SqlParameter("@Precio", det.Precio),
                        new SqlParameter("@Subtotal", det.Cantidad * det.Precio));

                    await _context.Database.ExecuteSqlRawAsync(
                        "EXEC sp_ActualizarStockProducto @IdProducto, @CantidadRestar",
                        new SqlParameter("@IdProducto", det.IdProducto),
                        new SqlParameter("@CantidadRestar", det.Cantidad));
                }

                await transaccion.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaccion.RollbackAsync();
                throw new Exception("Error al procesar la venta multi-producto. Se cancelo toda la operacion por seguridad transaccional.", ex);
            }
        }

        public async Task AnularPedido(int idPedido)
        {
            await _context.Database.ExecuteSqlRawAsync(
                "UPDATE Pedido SET Estado = 'ANULADO' WHERE IdPedido = {0}", idPedido);
        }
    }
}