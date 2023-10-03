using IoT_BackEnd.Models.Dto;

namespace IoT_BackEnd.Services.IServices
{
    public interface IAuthService
    {
        Task<string> Register(RegistrationRequestDto registrationRequestDto);
        Task<LoginResponseDto> Login (LoginRequestDto loginRequestDto);
        Task<bool> AssignRole(string email, string roleName);
    }
}
