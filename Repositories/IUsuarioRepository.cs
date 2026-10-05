using CafeteriaWeb.Models;

namespace CafeteriaWeb.Repositories
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> ValidarLogin(string correo, string clave);
    }
}