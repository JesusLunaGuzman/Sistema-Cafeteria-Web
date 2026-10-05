using Microsoft.AspNetCore.Mvc;
using CafeteriaWeb.Dtos;
using CafeteriaWeb.Services;

namespace CafeteriaWeb.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var lista = await _clienteService.Listar();
            return View(lista);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(ClienteDto dto)
        {
            dto.Estado = true;
            await _clienteService.Registrar(dto);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var cliente = await _clienteService.ObtenerPorId(id);
            if (cliente == null) return RedirectToAction("Index");

            return View(cliente);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(ClienteDto dto)
        {
            if (ModelState.IsValid)
            {
                await _clienteService.Editar(dto);
                return RedirectToAction("Index");
            }
            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int id)
        {
            var cliente = await _clienteService.ObtenerPorId(id);
            if (cliente != null)
            {
                cliente.Estado = false;
                await _clienteService.Editar(cliente);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Activar(int id)
        {
            var cliente = await _clienteService.ObtenerPorId(id);
            if (cliente != null)
            {
                cliente.Estado = true;
                await _clienteService.Editar(cliente);
            }
            return RedirectToAction("Index");
        }
    }
}