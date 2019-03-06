
namespace VacationsTracker.Core.DataAccess
{
    internal class Settings
    {
        public const string TokenStorageKey = "token_id";
        
        public const string VacationApiUrl = "https://vts-v2.azurewebsites.net/api/vts/workflow";

        public const string IdentityServiceUrl = "https://vts-token-issuer-v2.azurewebsites.net";
        
        public const string ClientIdForSwagger = "VTS-Swagger-v1";

        public const string ClientId = "VTS-Mobile-v1";

        public const string ClientSecret = "VTS123456789";

        public const string Scope = "VTS-Server-v2";
    }
}
