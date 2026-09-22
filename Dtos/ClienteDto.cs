using System.ComponentModel.DataAnnotations;

namespace CafeteriaWeb.Dtos
{
    public class ClienteDto
    {
        public int IdCliente { get; set; }

        [Required(ErrorMessage = "El nombre del cliente es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres.")]
        public string Nombre { get; set; } = null!;

        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
        [StringLength(100, ErrorMessage = "El correo no puede tener más de 100 caracteres.")]
        public string? Email { get; set; }

        [RegularExpression(@"^[0-9]{9}$", ErrorMessage = "El teléfono debe tener exactamente 9 dígitos numéricos.")]
        public string? Telefono { get; set; }

        public bool Estado { get; set; }
    }
}