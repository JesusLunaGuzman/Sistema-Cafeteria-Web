using AutoMapper;
using CafeteriaWeb.Dtos;
using CafeteriaWeb.Models;
using CafeteriaWeb.Repositories;

namespace CafeteriaWeb.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _repository;
        private readonly IMapper _mapper;

        public CategoriaService(ICategoriaRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoriaDto>> Listar()
        {
            var entidades = await _repository.Listar();
            return _mapper.Map<IEnumerable<CategoriaDto>>(entidades);
        }

        public async Task Registrar(CategoriaDto dto)
        {
            var entidad = _mapper.Map<CategoriaProducto>(dto);
            await _repository.Registrar(entidad);
        }

        public async Task<CategoriaDto?> ObtenerPorId(int id)
        {
            var entidad = await _repository.ObtenerPorId(id);
            return entidad == null ? null : _mapper.Map<CategoriaDto>(entidad);
        }

        public async Task Editar(CategoriaDto dto)
        {
            var entidad = _mapper.Map<CategoriaProducto>(dto);
            await _repository.Editar(entidad);
        }
    }
}