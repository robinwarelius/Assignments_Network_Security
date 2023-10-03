namespace IoT_FrontEnd.Services.IServices
{
    public interface ITokenProvider
    {
        string? GetToken();
        void SetToken(string token);
        void ClearToken();
    }
}
