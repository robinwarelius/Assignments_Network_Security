namespace IoT_FrontEnd.Utilities
{
    public class SD
    {
        public static string AuthApiUrl;

        public const string Role_Admin = "ADMIN";

        public const string Role_Customer = "CUSTOMER";

        public const string Token_Cookie = "JWTToken";
        public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE
        }
    }
}
