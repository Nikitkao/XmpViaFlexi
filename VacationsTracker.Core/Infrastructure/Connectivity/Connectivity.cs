using System;
using System.Collections.Generic;
using Xamarin.Essentials;

namespace VacationsTracker.Core.Infrastructure.Connectivity
{
    public sealed class Connectivity : IConnectivity
    {
        public event EventHandler<ConnectivityChangedEventArgs> ConnectivityChanged
        {
            add => Xamarin.Essentials.Connectivity.ConnectivityChanged += value;
            remove => Xamarin.Essentials.Connectivity.ConnectivityChanged -= value;
        }

        public bool IsConnected => Xamarin.Essentials.Connectivity.NetworkAccess == NetworkAccess.Internet;

        public NetworkAccess NetworkAccess => Xamarin.Essentials.Connectivity.NetworkAccess;

        public IEnumerable<ConnectionProfile> ConnectionProfiles => Xamarin.Essentials.Connectivity.ConnectionProfiles;
    }
}
