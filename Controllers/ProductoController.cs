using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using CafeteriaWeb.Dtos;
using CafeteriaWeb.Services;

namespace CafeteriaWeb.Controllers
{
    public class ProductoController : Controller
    {
        private readonly IProductoService _productoService;
        private readonly ICategoriaService _categoriaService;

        public ProductoController(IProductoService productoService, ICategoriaService categoriaService)
        {
            _productoService = productoService;
            _categoriaService = categoriaService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var lista = await _productoService.Listar();
            return View(lista);
        }

        [HttpGet]
        public async Task<IActionResult> Crear()
        {
            var categorias = await _categoriaService.Listar();
            var categoriasActivas = categorias.Where(c => c.Estado == true).ToList();

            ViewBag.Categorias = new SelectList(categoriasActivas, "IdCategoria", "Nombre");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(ProductoDto dto)
        {
            dto.Estado = true;
            await _productoService.Registrar(dto);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var producto = await _productoService.ObtenerPorId(id);
            if (producto == null) return RedirectToAction("Index");

            var categorias = await _categoriaService.Listar();
            ViewBag.Categorias = new SelectList(categorias, "IdCategoria", "Nombre", producto.IdCategoria);

            return View(producto);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(ProductoDto dto)
        {
            if (ModelState.IsValid)
            {
                await _productoService.Editar(dto);
                return RedirectToAction("Index");
            }

            // RECARGAMOS LA LISTA SI EL MODELO TIENE ERRORES PARA QUE NO TRONE LA VISTA
            var categorias = await _categoriaService.Listar();
            ViewBag.Categorias = new SelectList(categorias, "IdCategoria", "Nombre", dto.IdCategoria);
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int id)
        {
            var producto = await _productoService.ObtenerPorId(id);
            if (producto != null)
            {
                producto.Estado = false;
                await _productoService.Editar(producto);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Activar(int id)
        {
            var producto = await _productoService.ObtenerPorId(id);
            if (producto != null)
            {
                producto.Estado = true;
                await _productoService.Editar(producto);
            }
            return RedirectToAction("Index");
        }
    }
}