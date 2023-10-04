using IoT.ServiceBus;
using IoT_BackEnd.Models.Dto;
using IoT_BackEnd.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IoT_BackEnd.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ResponseDto _response;
        private readonly IAuthService _authService;
        private readonly ServiceBus _serviceBus;
        private readonly IConfiguration _configuration;

        public AuthController(IAuthService authService, ServiceBus serviceBus, IConfiguration configuration)
        {
            _response = new ResponseDto();
            _authService = authService;
            _serviceBus = serviceBus;
            _configuration = configuration;
        }

        // Registrerar användare
        [HttpPost("register")]
        public async Task<IActionResult> Register ([FromBody]RegistrationRequestDto model)
        {
            var errorMessage = await _authService.Register(model);
            if(!string.IsNullOrEmpty(errorMessage))
            {
                _response.IsSuccess = false;
                _response.Message = errorMessage;
                return BadRequest(errorMessage);
            }
            _response.Result = model;
            _response.Message = "User successfully created";

            // Skickar registrerad användare till min service bus på Azure (ville prova på)
            PublishMessage(model, _configuration.GetValue<string>("TopicAndQueueNames:EmailUserInformationQueue")!, _configuration.GetValue<string>("ConnectionStringServiceBus:EmailUserInformationQueue")!);

            return Ok(_response);                   
        }

        // Användaren loggar in
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
        {
            var loginResult = await _authService.Login(model);

            if (loginResult.User == null)
            {
                _response.IsSuccess = false;
                _response.Message = "Username or password is incorrect";
                return BadRequest(_response);
            }

            _response.Result = loginResult;
            return Ok(_response);
        }

        // Ger användaren en roll
        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRole([FromBody] RegistrationRequestDto model)
        {
            var assign_role_successful = await _authService.AssignRole(model.Email, model.Role.ToUpper());
            if (!assign_role_successful)
            {
                _response.IsSuccess = false;
                _response.Message = "Error encountered";
                return BadRequest(_response);
            }
            return Ok(_response);
        }

        // Service Bus
        private async void PublishMessage(object content, string queue_topic_name, string connectionString)
        {
            await _serviceBus.PublishContent(content, queue_topic_name, connectionString);
        }
    }
}
