using System.ComponentModel.DataAnnotations;

namespace CafeteriaWeb.Dtos
{
    public class ProductoDto
    {
        public int IdProducto { get; set; }

        [Required(ErrorMessage = "El nombre del producto es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "El precio es obligatorio.")]
        [Range(0.10, 500.00, ErrorMessage = "El precio debe estar entre S/ 0.10 y S/ 500.00.")]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "El stock inicial es obligatorio.")]
        [Range(0, 1000, ErrorMessage = "El stock no puede ser negativo ni mayor a 1000 unidades.")]
        public int Stock { get; set; }

        public bool Estado { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una categoría válida.")]
        public int IdCategoria { get; set; }

        public string? NombreCategoria { get; set; }
    }
}