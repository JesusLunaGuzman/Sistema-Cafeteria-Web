using Microsoft.AspNetCore.Mvc;
using CafeteriaWeb.Dtos;
using CafeteriaWeb.Services;

namespace CafeteriaWeb.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var lista = await _categoriaService.Listar();
            return View(lista);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(CategoriaDto dto)
        {
            dto.Estado = true;
            await _categoriaService.Registrar(dto);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var categoria = await _categoriaService.ObtenerPorId(id);
            if (categoria == null) return RedirectToAction("Index");

            return View(categoria);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(CategoriaDto dto)
        {
            // ASEGURAMOS QUE EL MODELO SEA VALIDO ANTES DE ENVIAR
            if (ModelState.IsValid)
            {
                await _categoriaService.Editar(dto);
                return RedirectToAction("Index");
            }
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int id)
        {
            var categoria = await _categoriaService.ObtenerPorId(id);
            if (categoria != null)
            {
                categoria.Estado = false;
                await _categoriaService.Editar(categoria);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Activar(int id)
        {
            var categoria = await _categoriaService.ObtenerPorId(id);
            if (categoria != null)
            {
                categoria.Estado = true;
                await _categoriaService.Editar(categoria);
            }
            return RedirectToAction("Index");
        }
    }
}