using IoT_BackEnd.Models;
using IoT_BackEnd.Models.Dto;
using IoT_BackEnd.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IoT_BackEnd.Controllers
{
    [Route("api/admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly ResponseDto _response;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
            _response = new ResponseDto();
        }

        [HttpPost("CreateAdvert")]
        [Authorize]
        public async Task <IActionResult> CreateAdvert(AdvertDto model)
        {
            var result = await _adminService.CreateAdvert(model);
            try
            {
                if (result)
                {
                    _response.Result = model;
                    _response.Message = "Advert created successfully";
                    return Ok(_response);
                }
                else
                {
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }
            }
            catch (Exception ex)
            {
                _response.Message += ex.ToString();
                return BadRequest(_response);
            }
                       
        }

        [HttpGet("GetLatestAdvert")]
        public async Task <IActionResult> GetLatestAdvert()
        {
            try
            {
                var result = await _adminService.GetLatestAdvert();

                if(result != null)
                {
                    _response.Result = result;
                    return Ok(_response);
                }
                else
                {
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }                            
            }
            catch(Exception ex)
            {
                _response.Message+= ex.ToString();
                return BadRequest(_response);
            }
        }
    }
}
