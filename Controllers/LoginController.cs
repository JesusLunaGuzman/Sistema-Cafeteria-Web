using Microsoft.AspNetCore.Mvc;
using CafeteriaWeb.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace CafeteriaWeb.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioService _usuarioService;

        public LoginController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Ingresar(string correo, string clave)
        {
            if (string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(clave))
            {
                ViewBag.Error = "Debe ingresar correo y contraseña.";
                return View("Index");
            }

            var usuarioValido = await _usuarioService.ValidarLogin(correo, clave);

            if (usuarioValido != null)
            {
                // USAMOS LOS CLAIMS CLASICOS EN LÍNEA SIN REVENTAR EL COMPILADOR
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, correo),
                    new Claim(ClaimTypes.Name, "Administrador"),
                    new Claim(ClaimTypes.Email, correo)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("Index", "Menu");
            }
            else
            {
                ViewBag.Error = "Correo o contraseña incorrectos.";
                return View("Index");
            }
        }

        public async Task<IActionResult> CerrarSesion()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login");
        }
    }
}