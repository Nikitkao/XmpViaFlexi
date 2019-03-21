using VacationsTracker.Core.DataAccess;
using VacationsTracker.Core.Infrastructure.Connectivity;
using Xamarin.Essentials;

namespace VacationsTracker.Core.Application.Connectivity
{
    public class ConnectivityService : IConnectivityService
    {
        private readonly IConnectivity _connectivity;
        private readonly ISynchronizationService _synchronizationService;

        public ConnectivityService(IConnectivity connectivity, ISynchronizationService synchronizationService)
        {
            _connectivity = connectivity;
            _synchronizationService = synchronizationService;
            _connectivity.ConnectivityChanged += ConnectivityOnConnectivityChanged;
        }

        private void ConnectivityOnConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            if (e.NetworkAccess == NetworkAccess.Internet)
            {
                _synchronizationService.TrySynchronize();
            }
        }

        public bool IsConnected => _connectivity.NetworkAccess != NetworkAccess.None;
    }
}
