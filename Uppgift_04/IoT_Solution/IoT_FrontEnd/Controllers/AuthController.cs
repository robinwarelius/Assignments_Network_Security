using Microsoft.AspNetCore.Mvc;

namespace IoT_FrontEnd.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
