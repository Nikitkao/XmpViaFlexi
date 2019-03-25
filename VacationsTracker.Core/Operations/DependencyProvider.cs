using System;
using System.Collections.Generic;
using System.Text;
using FlexiMvvm.Ioc;
using VacationsTracker.Core.Infrastructure.Connectivity;

namespace VacationsTracker.Core.Operations
{
    internal class DependencyProvider : IDependencyProvider
    {
        private readonly IConnectivity _connectivityService;

        public DependencyProvider(IConnectivity connectivityService)
        {
            _connectivityService = connectivityService;
        }

        public T Get<T>() where T : class
        {
            if (typeof(T) == typeof(IConnectivity))

                return (T)_connectivityService;

            throw new NotSupportedException($"Type \"{typeof(T)}\" is not registered.");

        }
    }
}
