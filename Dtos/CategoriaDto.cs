using System.ComponentModel.DataAnnotations;

namespace CafeteriaWeb.Dtos
{
    public class CategoriaDto
    {
        public int IdCategoria { get; set; }

        [Required(ErrorMessage = "El nombre de la categoría es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
        public string Nombre { get; set; } = null!;

        public bool Estado { get; set; }
    }
}