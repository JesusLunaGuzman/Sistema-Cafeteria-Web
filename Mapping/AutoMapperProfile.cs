using AutoMapper;
using CafeteriaWeb.Dtos;
using CafeteriaWeb.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CafeteriaWeb.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Mapeo de Categorías
            CreateMap<CategoriaProducto, CategoriaDto>().ReverseMap();

            // Mapeo de Usuarios y Login
            CreateMap<Usuario, LoginDto>().ReverseMap();
            CreateMap<Usuario, UsuarioDto>().ReverseMap();

            // Mapeo de Productos
            CreateMap<Producto, ProductoDto>()
                .ForMember(dest => dest.NombreCategoria,
                           opt => opt.MapFrom(src => src.IdCategoriaNavigation != null ? src.IdCategoriaNavigation.Nombre : null))
                .ReverseMap();

            // Mapeo de Clientes
            CreateMap<Cliente, ClienteDto>().ReverseMap();

            // Mapeo de Pedidos y Detalles Transaccionales
            CreateMap<Pedido, PedidoDto>()
                .ForMember(dest => dest.NombreCliente,
                           opt => opt.MapFrom(src => src.IdClienteNavigation != null ? src.IdClienteNavigation.Nombre : null))
                .ReverseMap();

            CreateMap<DetallePedido, DetallePedidoDto>().ReverseMap();
        }
    }
}