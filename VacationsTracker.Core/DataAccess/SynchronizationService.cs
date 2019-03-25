using System;
using System.Linq;
using VacationsTracker.Core.Domain;
using VacationsTracker.Core.Infrastructure.Connectivity;
using Xamarin.Essentials;

namespace VacationsTracker.Core.DataAccess
{
    public class SynchronizationService : ISynchronizationService
    {
        private readonly IDbService _dbService;
        private readonly IVacationRepository _vacationRepository;
        private readonly IConnectivity _connectivity;
        private bool _isRunning;

        public SynchronizationService(IDbService dbService, IVacationRepository vacationRepository, IConnectivity connectivity)
        {
            _dbService = dbService;
            _vacationRepository = vacationRepository;
            _connectivity = connectivity;

            //_connectivity.ConnectivityChangedWeakSubscribe(ConnectivityOnConnectivityChanged);
            _connectivity.ConnectivityChanged += ConnectivityOnConnectivityChanged;
        }

        private void ConnectivityOnConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            TrySynchronize();
        }

        public async void TrySynchronize()
        {
            try
            {
                if (!_connectivity.IsConnected || _isRunning)
                {
                    return;
                }

                _isRunning = true;

                var allVacations = await _dbService.GetItems().ConfigureAwait(false);

                var vacationsToUpdate = allVacations.Where(x => x.Type != OperationType.UpToDate);
                
                foreach (var awaitingOperation in vacationsToUpdate)
                {
                    switch (awaitingOperation.Type)
                    {
                        case OperationType.AddOrUpdate:
                            await _vacationRepository.AddOrUpdateAsync(awaitingOperation.ToVacation())
                                .ConfigureAwait(false);
                            break;
                        case OperationType.Delete:
                            await _vacationRepository.DeleteVacationAsync(awaitingOperation.Id.ToString())
                                .ConfigureAwait(false);
                            break;
                    }

                    awaitingOperation.Type = OperationType.UpToDate;
                    await _dbService.InsertOrReplace(awaitingOperation).ConfigureAwait(false);
                }
            }
            finally
            {
                _isRunning = false;
            }
        }
    }
}
