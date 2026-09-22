using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CafeteriaWeb.Dtos
{
    public class PedidoDto
    {
        public int IdPedido { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un cliente para registrar la venta.")]
        public int IdCliente { get; set; }

        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "El total de la venta no puede estar vacío.")]
        [Range(0.10, 5000.00, ErrorMessage = "El total debe ser un monto mayor a cero.")]
        public decimal Total { get; set; }

        public string Estado { get; set; } = "COMPLETADO";

        public string? NombreCliente { get; set; }

        [Required(ErrorMessage = "El pedido debe contener al menos un producto en el carrito.")]
        public List<DetallePedidoDto> Detalles { get; set; } = new List<DetallePedidoDto>();
    }
}