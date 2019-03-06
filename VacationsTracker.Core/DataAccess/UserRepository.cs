using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using IdentityModel.Client;
using VacationsTracker.Core.Domain;

namespace VacationsTracker.Core.DataAccess
{
    public class UserRepository : IUserRepository
    {
        private readonly ISecureStorage _storage;
        
        public UserRepository(ISecureStorage storage)
        {
            _storage = storage;
        }

        public async Task AuthorizeAsync(User user, CancellationToken token = default)
        {
            var discoveryClient = new DiscoveryClient(Settings.IdentityServiceUrl);

            discoveryClient.Policy.RequireHttps = false;

            var identityServer = await discoveryClient.GetAsync(token);

            if (identityServer.IsError)
            {
                throw new AuthenticationException();
            }
            
            var authClient = new TokenClient(
                identityServer.TokenEndpoint,
                Settings.ClientId,
                Settings.ClientSecret);

            var userTokenResponse = await authClient.RequestResourceOwnerPasswordAsync(
                user.Login,
                user.Password,
                Settings.Scope,
                cancellationToken: token);

            if (userTokenResponse.IsError || userTokenResponse.AccessToken == null)
            {
                throw new AuthenticationException();
            }

            await _storage.SetAsync(Settings.TokenStorageKey, userTokenResponse.AccessToken);
        }
    }
}
