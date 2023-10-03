using IoT_BackEnd.Data;
using IoT_BackEnd.Models;
using IoT_BackEnd.Models.Dto;
using IoT_BackEnd.Services.IServices;
using Microsoft.AspNetCore.Identity;

namespace IoT_BackEnd.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ITokenJwtGenerator _tokenGenerator;

        public AuthService(AppDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ITokenJwtGenerator tokenGenerator)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<bool> AssignRole(string email, string roleName)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(item => item.Email.ToLower() == email.ToLower());

            if (user != null)
            {             
                if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                {                 
                    _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
                }             
                await _userManager.AddToRoleAsync(user, roleName);
                return true;
            }
            return false;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var user = _db.ApplicationUsers.FirstOrDefault(item => item.UserName.ToLower() == loginRequestDto.UserName.ToLower());
            bool is_valid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);
            if (user == null || is_valid == false)
            {
                return new LoginResponseDto() { User = null, Token = null };              
            }

            var roles = await _userManager.GetRolesAsync(user);
            var token = _tokenGenerator.GenerateJwtToken(user, roles);

            UserDto userDto = new UserDto()
            {
                UserId = user.Id,
                Email = user.Email,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber
            };

            LoginResponseDto loginResponseDto = new LoginResponseDto()
            {
                User = userDto,
                Token = token
            };

            return loginResponseDto;
        }

        public async Task<string> Register(RegistrationRequestDto registrationRequestDto)
        {
            ApplicationUser user = new ApplicationUser()
            {
                UserName = registrationRequestDto.Email,
                Name = registrationRequestDto.Name,
                Email = registrationRequestDto.Email,
                NormalizedEmail = registrationRequestDto.Email.ToUpper(),
                PhoneNumber = registrationRequestDto.PhoneNumber
            };

            try
            {
                var result = await _userManager.CreateAsync(user, registrationRequestDto.Password);
                if (result.Succeeded)
                {
                    var userToReturn = _db.ApplicationUsers.First(item => item.UserName == registrationRequestDto.Email);
                    UserDto userDto = new UserDto()
                    {
                        UserId = userToReturn.Id,
                        Name = userToReturn.Name,
                        Email = userToReturn.Email,
                        PhoneNumber = userToReturn.PhoneNumber
                    };
                    return ""; 
                }
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                }

            }
            catch (Exception ex)
            {
                return "error encounter";
            }

        }
    }
}
