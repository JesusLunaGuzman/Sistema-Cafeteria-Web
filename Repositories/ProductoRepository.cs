using CafeteriaWeb.Data;
using CafeteriaWeb.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CafeteriaWeb.Repositories
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly AppDbContext _context;

        public ProductoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Producto>> Listar()
        {
            var productos = await _context.Productos
                .FromSqlRaw("EXEC sp_ListarProductos")
                .ToListAsync();
            await _context.CategoriaProductos.ToListAsync();
            return productos;
        }

        public async Task Registrar(Producto producto)
        {
            var pNombre = new SqlParameter("@Nombre", producto.Nombre);
            var pPrecio = new SqlParameter("@Precio", producto.Precio);
            var pStock = new SqlParameter("@Stock", producto.Stock);
            var pEstado = new SqlParameter("@Estado", producto.Estado);
            var pIdCat = new SqlParameter("@IdCategoria", producto.IdCategoria);

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_RegistrarProducto @Nombre, @Precio, @Stock, @Estado, @IdCategoria",
                pNombre, pPrecio, pStock, pEstado, pIdCat);
        }

        public async Task<Producto?> ObtenerPorId(int id)
        {
            var pId = new SqlParameter("@IdProducto", id);
            var resultado = await _context.Productos
                .FromSqlRaw("EXEC sp_ObtenerProducto @IdProducto", pId)
                .ToListAsync();
            return resultado.FirstOrDefault();
        }

        public async Task Editar(Producto producto)
        {
            var pId = new SqlParameter("@IdProducto", producto.IdProducto);
            var pNombre = new SqlParameter("@Nombre", producto.Nombre);
            var pPrecio = new SqlParameter("@Precio", producto.Precio);
            var pStock = new SqlParameter("@Stock", producto.Stock);
            var pEstado = new SqlParameter("@Estado", producto.Estado);
            var pIdCat = new SqlParameter("@IdCategoria", producto.IdCategoria);

            await _context.Database.ExecuteSqlRawAsync(
                "EXEC sp_EditarProducto @IdProducto, @Nombre, @Precio, @Stock, @Estado, @IdCategoria",
                pId, pNombre, pPrecio, pStock, pEstado, pIdCat);
        }
    }
}