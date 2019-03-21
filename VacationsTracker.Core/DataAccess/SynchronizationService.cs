using System.Threading.Tasks;
using VacationsTracker.Core.Domain;

namespace VacationsTracker.Core.DataAccess
{
    public class SynchronizationService : ISynchronizationService
    {
        private readonly IDbService _dbService;
        private readonly IVacationRepository _vacationRepository;

        private bool _isRunning;

        public SynchronizationService(IDbService dbService, IVacationRepository vacationRepository)
        {
            _dbService = dbService;
            _vacationRepository = vacationRepository;
        }

        public async Task TrySynchronize()
        {
            try
            {
                if (_isRunning)
                    return;

                _isRunning = true;

                while (true)
                {
                    var firstAwaitingOperation = await _dbService.GetFirstOrDefault().ConfigureAwait(false);
                    if (firstAwaitingOperation == null)
                    {
                        break;
                    }

                    switch (firstAwaitingOperation.Type)
                    {
                        case OperationType.AddOrUpdate:
                            await _vacationRepository.AddOrUpdateAsync(firstAwaitingOperation.ToVacation()).ConfigureAwait(false);
                            break;
                        case OperationType.Delete:
                            await _vacationRepository.DeleteVacationAsync(firstAwaitingOperation.Id.ToString()).ConfigureAwait(false);
                            break;
                    }

                    await _dbService.RemoveItem(firstAwaitingOperation).ConfigureAwait(false);
                }
            }
            finally
            {
                _isRunning = false;
            }
        }
    }
}
