using IoT_BackEnd.Hubs;
using IoT_BackEnd.Models;
using IoT_BackEnd.Models.Dto;
using IoT_BackEnd.Services;
using IoT_BackEnd.Services.IServices;
using IoT_BackEnd.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.SignalR;

namespace IoT_BackEnd.Controllers
{
    [Route("api/unit")]
    [ApiController]
    public class UnitController : ControllerBase
    {
        private readonly IUnitService _unitService;
        private readonly IHubContext<UnitHub> _hubContext;
        UnitDto _unitDto;
       
        public UnitController(IUnitService unitService, IHubContext<UnitHub> hubContext)
        {
            _unitService = unitService;
            _hubContext = hubContext;
            _unitDto = new UnitDto();
        }


        [HttpPost("CreateUnit")]
        public async Task<IActionResult> CreateUnit([FromBody] EncryptedDto encryptedDto)
        {
            try
            {
                if(encryptedDto != null && ModelState.IsValid)
                {
                    string decryptedSecretValue = await Decryption.DecryptData(encryptedDto);
                    string[] decryptedSecretValues = decryptedSecretValue.Split(",");
                    UnitDto unitDto = await Factory.Create_UnitDto(decryptedSecretValues);
                    var response = await _unitService.CreateUnit(unitDto);

                    if (response != null)
                    {
                        _unitDto = await _unitService.GetUnitById(response.UnitId);
                        await _hubContext.Clients.All.SendAsync("updateUnit", _unitDto.Name, _unitDto.Description, _unitDto.Temperature);
                        return Ok(response);                                        
                    }         
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpPost("DeleteUnitById")]
        public async Task <IActionResult> DeleteUnitById (int id)
        {
            try
            {
                if (id != null)
                {
                    bool isSuccess = await _unitService.DeleteUnitById(id);
                    if (isSuccess)
                    {
                        return Ok(isSuccess);
                    }                  
                }
                return BadRequest();
            }
            catch (Exception ex) 
            { 
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpGet("GetUnitById")]
        public async Task<IActionResult> GetUnitById(int id)
        {
            try
            {
                if (id != null)
                {
                    UnitDto unit = await _unitService.GetUnitById(id);
                    if (unit != null)
                    {
                        return Ok(unit);
                    }
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpGet("GetUnitByName")]
        public async Task<IActionResult> GetUnitByName(string identifier)
        {
            try
            {
                if (identifier != null)
                {
                    UnitDto unit = await _unitService.GetUnitByName(identifier);
                    if (unit != null)
                    {
                        return Ok(unit);
                    }
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }


    } 
}
