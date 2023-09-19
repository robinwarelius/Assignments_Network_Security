using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Uppgift_01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet("ReturnSometing")]
        public async Task <IActionResult> ReturnSomething()
        {
            return null;
        } 
    }
}
