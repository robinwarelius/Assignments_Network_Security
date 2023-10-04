using IoT_FrontEnd.Models.Dtos;
using IoT_FrontEnd.Services.IServices;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using IoT_FrontEnd.Utilities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IdentityModel.Tokens.Jwt;

namespace IoT_FrontEnd.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ITokenProvider _tokenProvider;
        public AuthController(IAuthService authService, ITokenProvider tokenProvider)
        {
            _authService = authService;
            _tokenProvider = tokenProvider;
        }
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        // Loggar in användaren & placerar token som cookie 
        public async Task<IActionResult> Login(LoginRequestDto model)
        {
            ResponseDto? response = await _authService.LoginAsync(model);

            if (response.IsSuccess && response.Result != null && ModelState.IsValid)
            {
                LoginResponseDto loginResponseDto = JsonConvert.DeserializeObject<LoginResponseDto>(Convert.ToString(response.Result));
                await SignInUser(loginResponseDto);
                _tokenProvider.SetToken(loginResponseDto.Token);
                return RedirectToAction("Index", "Unit");
            }

            return View(model);
        }

        // Följande två metoder registrar en användare och ger hen en roll (admin eller customer)
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            var roleList = new List<SelectListItem>()
            {
                new SelectListItem() {Text = SD.Role_Admin, Value = SD.Role_Admin},
                new SelectListItem() {Text = SD.Role_Customer, Value = SD.Role_Customer}
            };

            ViewBag.RoleList = roleList;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationRequestDto model)
        {
            ResponseDto response = await _authService.RegisterAsync(model);

            if (ModelState.IsValid && response.IsSuccess && response != null)
            {                             
                  if (string.IsNullOrEmpty(model.Role))
                    {
                        model.Role = SD.Role_Customer;
                    }

                    ResponseDto assignRole = await _authService.AssignRoleAsync(model);

                    if (assignRole.IsSuccess && assignRole != null)
                    {   
                        return RedirectToAction(nameof(Login));
                    }              
            }        
                                      
            var roleList = new List<SelectListItem>()
            {
                new SelectListItem() {Text = SD.Role_Admin, Value = SD.Role_Admin},
                new SelectListItem() {Text = SD.Role_Customer, Value = SD.Role_Customer}
            };

            ViewBag.RoleList = roleList;
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            _tokenProvider.ClearToken();
            return RedirectToAction("Index", "Unit");
        }
      
        // Loggar in användaren med .net inbyggda system
        private async Task SignInUser(LoginResponseDto model)
        {
            // Läser token
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(model.Token);

            // Addar claims från token
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email, jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub, jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub).Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name, jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Name).Value));
            identity.AddClaim(new Claim(ClaimTypes.Name, jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));
            identity.AddClaim(new Claim(ClaimTypes.Role, jwt.Claims.FirstOrDefault(u => u.Type == "role").Value));

            // Loggar in användaren
            var pricipal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, pricipal);
        }
    }
}
