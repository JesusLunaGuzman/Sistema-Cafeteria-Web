using AutoMapper;
using CafeteriaWeb.Dtos;
using CafeteriaWeb.Models;
using CafeteriaWeb.Repositories;

namespace CafeteriaWeb.Services
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _repository;
        private readonly IMapper _mapper;

        public ProductoService(IProductoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductoDto>> Listar()
        {
            var entidades = await _repository.Listar();
            return _mapper.Map<IEnumerable<ProductoDto>>(entidades);
        }

        public async Task Registrar(ProductoDto dto)
        {
            var entidad = _mapper.Map<Producto>(dto);
            await _repository.Registrar(entidad);
        }

        public async Task<ProductoDto?> ObtenerPorId(int id)
        {
            var entidad = await _repository.ObtenerPorId(id);
            return entidad == null ? null : _mapper.Map<ProductoDto>(entidad);
        }

        public async Task Editar(ProductoDto dto)
        {
            var entidad = _mapper.Map<Producto>(dto);
            await _repository.Editar(entidad);
        }
    }
}