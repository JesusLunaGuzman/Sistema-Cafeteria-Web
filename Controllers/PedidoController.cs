using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using CafeteriaWeb.Dtos;
using CafeteriaWeb.Services;

namespace CafeteriaWeb.Controllers
{
    public class PedidoController : Controller
    {
        private readonly IPedidoService _pedidoService;
        private readonly IClienteService _clienteService;
        private readonly IProductoService _productoService;

        public PedidoController(IPedidoService pedidoService, IClienteService clienteService, IProductoService productoService)
        {
            _pedidoService = pedidoService;
            _clienteService = clienteService;
            _productoService = productoService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var pedidos = await _pedidoService.ListarPedidos();
            return View(pedidos);
        }

        [HttpGet]
        public async Task<IActionResult> Crear()
        {

            var clientes = await _clienteService.Listar();
            ViewBag.Clientes = new SelectList(clientes.Where(c => c.Estado), "IdCliente", "Nombre");


            var productos = await _productoService.Listar();
            ViewBag.Productos = productos.Where(p => p.Estado && p.Stock > 0).ToList();

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> GuardarVenta([FromBody] PedidoDto pedido)
        {
            if (pedido == null || pedido.Detalles == null || !pedido.Detalles.Any())
            {
                return Json(new { exito = false, mensaje = "El carrito de compras está completamente vacío." });
            }

            try
            {
                pedido.Estado = "COMPLETADO";
                pedido.Fecha = DateTime.Now;

                await _pedidoService.RegistrarVenta(pedido);

                return Json(new { exito = true, mensaje = "¡La venta multi-producto ha sido procesada y registrada con éxito!" });
            }
            catch (Exception ex)
            {
                return Json(new { exito = false, mensaje = "Error Transaccional: " + ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Anular(int id)
        {

            await _pedidoService.AnularPedido(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> ExportarReportePDF()
        {
            // OBTENEMOS EL HISTORIAL COMPLETO DE VENTAS DESDE EL SERVICIO
            var pedidos = await _pedidoService.ListarPedidos();

            // PASAMOS LA LISTA COMPLETA A UNA VISTA ESPECIAL DE IMPRESION
            return View(pedidos);
        }
    }
}