using Microsoft.AspNetCore.Mvc;

namespace CafeteriaWeb.Controllers
{
    public class MenuController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}