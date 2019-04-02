using System.Net.Http;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using IdentityModel.Client;
using VacationsTracker.Core.Application;
using VacationsTracker.Core.Data;
using VacationsTracker.Core.Domain;

namespace VacationsTracker.Core.DataAccess
{
    public class UserRepository : IUserRepository
    {
        private readonly ISecureStorage _storage;

        private readonly IVacationApi _vacationApi;

        public UserRepository(ISecureStorage storage, IVacationApi vacationApi)
        {
            _storage = storage;
            _vacationApi = vacationApi;
        }

        public async Task AuthorizeAsync(User user, CancellationToken token = default)
        {
            var httpClient = new HttpClient();

            var discoveryClient = await httpClient.GetDiscoveryDocumentAsync(
                new DiscoveryDocumentRequest()
                {
                    Address = Constants.IdentityServiceUrl,
                    Policy = {RequireHttps = false}
                },
                token);

            if (discoveryClient.IsError)
            {
                throw new AuthenticationException();
            }

            var userTokenResponse = await httpClient.RequestPasswordTokenAsync(
                new PasswordTokenRequest
                {
                    Address = discoveryClient.TokenEndpoint,
                    ClientId = Constants.ClientId,
                    ClientSecret = Constants.ClientSecret,
                    Scope = Constants.Scope,
                    UserName = user.Login,
                    Password = user.Password
                },
                token);

            if (userTokenResponse.IsError || userTokenResponse.AccessToken == null)
            {
                throw new AuthenticationException();
            }

            await _storage.SetAsync(
                Constants.TokenStorageKey,
                userTokenResponse.AccessToken);

            _vacationApi.SetToken();
        }
    }
}
