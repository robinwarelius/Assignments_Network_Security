using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Uppgift_01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet("ReturnSometing")]
        public async Task <List<object>> ReturnSomething(string random)
        {
            var data = new List<object>
            {
               new { Id = 1, Namn = "Objekt 1" },
               new { Id = 2, Namn = "Objekt 2" },
               new { Id = 3, Namn = "Objekt 3" }
            };

            return data;
        } 
    }
}
