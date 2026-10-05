using CafeteriaWeb.Data;
using CafeteriaWeb.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CafeteriaWeb.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _context;

        public UsuarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario?> ValidarLogin(string correo, string clave)
        {
            var pCorreo = new SqlParameter("@Correo", correo);
            var pClave = new SqlParameter("@Clave", clave);

            var resultado = await _context.Usuarios
                .FromSqlRaw("EXEC sp_LoginUsuario @Correo, @Clave", pCorreo, pClave)
                .ToListAsync();

            return resultado.FirstOrDefault();
        }
    }
}