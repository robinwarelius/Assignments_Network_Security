using IoT_BackEnd.Models.Dto;
using IoT_BackEnd.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IoT_BackEnd.Controllers
{
    [Route("api/Unit")]
    [ApiController]
    public class Unit : ControllerBase
    {
        [HttpPost("UnitValues")]
        public async Task<IActionResult> UnitValues([FromBody] EncryptedDto encryptedDto)
        {
            string decryptedSecretValue = await Decryption.DecryptData(encryptedDto);
            string[] decryptedSecretValues = decryptedSecretValue.Split(",");

            // Skicka med signalR till frontend
            UnitDto unitDto = await Factory.Create_UnitDto(decryptedSecretValues);
            
            return Ok();
        }
    }
}
