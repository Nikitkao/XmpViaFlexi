using System;
using System.Collections.Generic;
using System.Text;
using FlexiMvvm.Ioc;
using VacationsTracker.Core.Application.Connectivity;

namespace VacationsTracker.Core.Operations
{
    internal class DependencyProvider : IDependencyProvider
    {
        private readonly IConnectivityService _connectivityService;

        public DependencyProvider(IConnectivityService connectivityService)
        {
            _connectivityService = connectivityService;
        }

        public T Get<T>() where T : class
        {
            if (typeof(T) == typeof(IConnectivityService))

                return (T)_connectivityService;

            throw new NotSupportedException($"Type \"{typeof(T)}\" is not registered.");

        }
    }
}
