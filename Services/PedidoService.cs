using AutoMapper;
using CafeteriaWeb.Dtos;
using CafeteriaWeb.Models;
using CafeteriaWeb.Repositories;

namespace CafeteriaWeb.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _repository;
        private readonly IMapper _mapper;

        public PedidoService(IPedidoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PedidoDto>> ListarPedidos()
        {
            var pedidos = await _repository.ListarPedidos();
            return _mapper.Map<IEnumerable<PedidoDto>>(pedidos);
        }

        public async Task RegistrarVenta(PedidoDto pedidoDto)
        {
            var pedido = _mapper.Map<Pedido>(pedidoDto);
            var detalles = _mapper.Map<List<DetallePedido>>(pedidoDto.Detalles);

            await _repository.RegistrarVentaTransaccion(pedido, detalles);
        }

        public async Task AnularPedido(int idPedido)
        {
            await _repository.AnularPedido(idPedido);
        }
    }
}