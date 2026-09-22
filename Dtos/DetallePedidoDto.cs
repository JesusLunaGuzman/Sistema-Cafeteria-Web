using System.ComponentModel.DataAnnotations;

namespace CafeteriaWeb.Dtos
{
    public class DetallePedidoDto
    {
        [Required(ErrorMessage = "El producto es obligatorio.")]
        public int IdProducto { get; set; }

        [Required(ErrorMessage = "La cantidad es obligatoria.")]
        [Range(1, 50, ErrorMessage = "La cantidad por producto debe estar entre 1 y 50 unidades.")]
        public int Cantidad { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio.")]
        [Range(0.10, 500.00, ErrorMessage = "El precio del producto debe ser válido.")]
        public decimal Precio { get; set; }

        public decimal Subtotal { get; set; }

        public string? NombreProducto { get; set; }
    }
}