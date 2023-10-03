using IoT_BackEnd.Models;

namespace IoT_BackEnd.Services.IServices
{
    public interface ITokenJwtGenerator
    {
        string GenerateJwtToken(ApplicationUser applicationUser, IEnumerable<string> roles);
    }
}
