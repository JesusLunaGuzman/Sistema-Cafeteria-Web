using CafeteriaWeb.Data;
using CafeteriaWeb.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CafeteriaWeb.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly AppDbContext _context;

        public ClienteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cliente>> Listar()
        {
            return await _context.Clientes
                .FromSqlRaw("EXEC sp_ListarClientes")
                .ToListAsync();
        }

        public async Task Registrar(Cliente cliente)
        {
            var pNombre = new SqlParameter("@Nombre", cliente.Nombre);
            var pEmail = new SqlParameter("@Email", (object?)cliente.Email ?? DBNull.Value);
            var pTelefono = new SqlParameter("@Telefono", (object?)cliente.Telefono ?? DBNull.Value);
            var pEstado = new SqlParameter("@Estado", cliente.Estado);

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_RegistrarCliente @Nombre, @Email, @Telefono, @Estado",
                pNombre, pEmail, pTelefono, pEstado);
        }

        public async Task<Cliente?> ObtenerPorId(int id)
        {
            var pId = new SqlParameter("@IdCliente", id);
            var resultado = await _context.Clientes
                .FromSqlRaw("EXEC sp_ObtenerCliente @IdCliente", pId)
                .ToListAsync();
            return resultado.FirstOrDefault();
        }

        public async Task Editar(Cliente cliente)
        {
            var pId = new SqlParameter("@IdCliente", cliente.IdCliente);
            var pNombre = new SqlParameter("@Nombre", cliente.Nombre);
            var pEmail = new SqlParameter("@Email", (object?)cliente.Email ?? DBNull.Value);
            var pTelefono = new SqlParameter("@Telefono", (object?)cliente.Telefono ?? DBNull.Value);
            var pEstado = new SqlParameter("@Estado", cliente.Estado);

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_EditarCliente @IdCliente, @Nombre, @Email, @Telefono, @Estado",
                pId, pNombre, pEmail, pTelefono, pEstado);
        }
    }
}