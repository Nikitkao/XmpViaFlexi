using System.Threading;
using System.Threading.Tasks;
using FlexiMvvm;
using FlexiMvvm.Operations;
using VacationsTracker.Core.Exceptions;
using VacationsTracker.Core.Infrastructure.Connectivity;

namespace VacationsTracker.Core.Operations
{
    public class InternetConnectionCondition : OperationConditionBase
    {
        public override Task<bool> CheckAsync(OperationContext context, CancellationToken cancellationToken)
        {
            var connectivityService = context.DependencyProvider.NotNull().Get<IConnectivity>();
            
            if (connectivityService.IsConnected)
            {
                return Task.FromResult(true);
            }
            throw new InternetConnectionException("No internet connection. TODO localize.");
        }
    }
}
