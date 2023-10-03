using IoT_Frontend.Services.IServices;
using IoT_FrontEnd.Models;
using IoT_FrontEnd.Models.Dtos;
using IoT_FrontEnd.Services.IServices;
using IoT_FrontEnd.Utilities;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace IoT_FrontEnd.Services
{
    public class AuthService : IAuthService
    {
        private readonly IBaseService _baseService;
        public AuthService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> AssignRoleAsync(RegistrationRequestDto registrationRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = registrationRequestDto,
                Url = $"{SD.AuthApiUrl}/api/auth/AssignRole"
            }, withBearer:false);                     
        }

        public async Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = loginRequestDto,
                Url = $"{SD.AuthApiUrl}/api/auth/login"
            }, withBearer: false);
        }

        public async Task<ResponseDto?> RegisterAsync(RegistrationRequestDto registrationRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = registrationRequestDto,
                Url = $"{SD.AuthApiUrl}/api/auth/register"
            }, withBearer: false);
        }
    }
}
