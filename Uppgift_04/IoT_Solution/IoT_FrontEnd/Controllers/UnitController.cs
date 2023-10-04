using IoT_FrontEnd.Models.Dtos;
using IoT_FrontEnd.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace IoT_FrontEnd.Controllers
{
    public class UnitController : Controller
    {
        private readonly IAdvertService _advertService;
        public UnitController(IAdvertService advertService)
        {
            _advertService = advertService;
        }

        // Hämtar upp senaste annonsen och skickar till startvy
        public async Task <IActionResult> Index()
        {          
                ResponseDto? result = await _advertService.GetLatestAdvertAsync();
                AdvertDto? model = JsonConvert.DeserializeObject<AdvertDto>(result.Result.ToString());

                if (result != null)
                {
                    return View(model);
                }

                return View();
                     
        }
    }
}
