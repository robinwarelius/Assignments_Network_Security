using Microsoft.AspNetCore.Mvc;

namespace IoT_FrontEnd.Controllers
{
    public class UnitController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
