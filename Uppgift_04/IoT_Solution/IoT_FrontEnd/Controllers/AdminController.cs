using Microsoft.AspNetCore.Mvc;

namespace IoT_FrontEnd.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
