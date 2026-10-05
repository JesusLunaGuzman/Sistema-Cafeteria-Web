using CafeteriaWeb.Dtos;

namespace CafeteriaWeb.Services
{
    public interface IUsuarioService
    {
        Task<LoginDto?> ValidarLogin(string correo, string clave);
    }
}