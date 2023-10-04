using IoT_FrontEnd.Services.IServices;
using IoT_FrontEnd.Utilities;

namespace IoT_FrontEnd.Services
{
    public class TokenProvider : ITokenProvider
    {
       
        private readonly IHttpContextAccessor _contextAccessor;
        public TokenProvider(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        // Ta bort token (utloggning tex)
        public void ClearToken()
        {
            _contextAccessor.HttpContext?.Response.Cookies.Delete(SD.Token_Cookie);
        }

        // Hämta token (via restanrop tex)
        public string? GetToken()
        {
            string? token = null;
            bool? hasToken = _contextAccessor.HttpContext?.Request.Cookies.TryGetValue(SD.Token_Cookie, out token);
            return hasToken is true ? token : null;
        }

        // Appenda token
        public void SetToken(string token)
        {
            _contextAccessor.HttpContext?.Response.Cookies.Append(SD.Token_Cookie, token);
        }
    }
}
