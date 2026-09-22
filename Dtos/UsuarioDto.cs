using System.ComponentModel.DataAnnotations;

namespace CafeteriaWeb.Dtos
{
    public class UsuarioDto
    {
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "Formato de correo inválido.")]
        public string Correo { get; set; } = null!;

        public bool Estado { get; set; }
    }
}