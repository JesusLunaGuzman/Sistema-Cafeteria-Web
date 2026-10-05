using CafeteriaWeb.Data;
using CafeteriaWeb.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CafeteriaWeb.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContext _context;

        public CategoriaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CategoriaProducto>> Listar()
        {
            return await _context.CategoriaProductos
                .FromSqlRaw("EXEC sp_ListarCategorias")
                .ToListAsync();
        }

        public async Task Registrar(CategoriaProducto categoria)
        {
            var pNombre = new SqlParameter("@Nombre", categoria.Nombre);
            var pEstado = new SqlParameter("@Estado", categoria.Estado);

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_RegistrarCategoria @Nombre, @Estado", pNombre, pEstado);
        }

        public async Task<CategoriaProducto?> ObtenerPorId(int id)
        {
            var pId = new SqlParameter("@IdCategoria", id);
            var resultado = await _context.CategoriaProductos
                .FromSqlRaw("EXEC sp_ObtenerCategoria @IdCategoria", pId)
                .ToListAsync();
            return resultado.FirstOrDefault();
        }

        public async Task Editar(CategoriaProducto categoria)
        {
            var pId = new SqlParameter("@IdCategoria", categoria.IdCategoria);
            var pNombre = new SqlParameter("@Nombre", categoria.Nombre);
            var pEstado = new SqlParameter("@Estado", categoria.Estado);

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_EditarCategoria @IdCategoria, @Nombre, @Estado",
                pId, pNombre, pEstado);
        }
    }
}