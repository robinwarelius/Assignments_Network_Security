using IoT_BackEnd.Models;
using IoT_BackEnd.Services.IServices;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IoT_BackEnd.Services
{
    public class TokenJwtGenerator : ITokenJwtGenerator
    {
        private readonly JwtOptions _jwtOptions;

        public TokenJwtGenerator(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }

        public string GenerateJwtToken(ApplicationUser applicationUser, IEnumerable<string> roles)
        {
            var token_handler = new JwtSecurityTokenHandler();
            var token_key = Encoding.ASCII.GetBytes(_jwtOptions.Secret);

            var token_claim_list = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Email, applicationUser.Email),
                new Claim(JwtRegisteredClaimNames.Sub, applicationUser.Id),
                new Claim(JwtRegisteredClaimNames.Name, applicationUser.UserName)
            };
         
            token_claim_list.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var token_descriptor = new SecurityTokenDescriptor
            {
                Audience = _jwtOptions.Audience,
                Issuer = _jwtOptions.Issuer,
                Subject = new ClaimsIdentity(token_claim_list),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(token_key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = token_handler.CreateToken(token_descriptor);
            return token_handler.WriteToken(token);
        }

    }
}
