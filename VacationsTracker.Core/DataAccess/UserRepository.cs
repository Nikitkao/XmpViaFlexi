using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using IdentityModel.Client;
using VacationsTracker.Core.Application;
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
            var discoveryClient = new DiscoveryClient(Constants.IdentityServiceUrl);

            discoveryClient.Policy.RequireHttps = false;

            var identityServer = await discoveryClient.GetAsync(token);

            if (identityServer.IsError)
            {
                throw new AuthenticationException();
            }
            
            var authClient = new TokenClient(
                identityServer.TokenEndpoint,
                Constants.ClientId,
                Constants.ClientSecret);

            var userTokenResponse = await authClient.RequestResourceOwnerPasswordAsync(
                user.Login,
                user.Password,
                Constants.Scope,
                cancellationToken: token);

            if (userTokenResponse.IsError || userTokenResponse.AccessToken == null)
            {
                throw new AuthenticationException();
            }

            await _storage.SetAsync(Constants.TokenStorageKey, userTokenResponse.AccessToken);
        }
    }
}
