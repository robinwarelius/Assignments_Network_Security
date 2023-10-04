using IoT_FrontEnd.Models.Dtos;
using IoT_FrontEnd.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace IoT_FrontEnd.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdvertService _advertService;

        public AdminController(IAdvertService advertService)
        {
            _advertService = advertService;
        }

        public async Task <IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        // Admin kan skapa en annons som hamnar på första sidan
        public async Task <IActionResult> Index(AdvertDto model)
        {
            ResponseDto result = await _advertService.CreateAdvertAsync(model);
            if (ModelState.IsValid && result != null && result.IsSuccess)
            {
                return RedirectToAction("Index", "Admin");
            }
      
            return View(model);
        }
    }
}
