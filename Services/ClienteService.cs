using AutoMapper;
using CafeteriaWeb.Dtos;
using CafeteriaWeb.Models;
using CafeteriaWeb.Repositories;

namespace CafeteriaWeb.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _repository;
        private readonly IMapper _mapper;

        public ClienteService(IClienteRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ClienteDto>> Listar()
        {
            var entidades = await _repository.Listar();
            return _mapper.Map<IEnumerable<ClienteDto>>(entidades);
        }

        public async Task Registrar(ClienteDto dto)
        {
            var entidad = _mapper.Map<Cliente>(dto);
            await _repository.Registrar(entidad);
        }

        public async Task<ClienteDto?> ObtenerPorId(int id)
        {
            var entidad = await _repository.ObtenerPorId(id);
            return entidad == null ? null : _mapper.Map<ClienteDto>(entidad);
        }

        public async Task Editar(ClienteDto dto)
        {
            var entidad = _mapper.Map<Cliente>(dto);
            await _repository.Editar(entidad);
        }
    }
}