using System.Linq;
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
