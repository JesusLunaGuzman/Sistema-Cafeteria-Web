using AutoMapper;
using CafeteriaWeb.Dtos;
using CafeteriaWeb.Repositories;

namespace CafeteriaWeb.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repository;
        private readonly IMapper _mapper;

        public UsuarioService(IUsuarioRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<LoginDto?> ValidarLogin(string correo, string clave)
        {
            var usuario = await _repository.ValidarLogin(correo, clave);
            return usuario == null ? null : _mapper.Map<LoginDto>(usuario);
        }
    }
}