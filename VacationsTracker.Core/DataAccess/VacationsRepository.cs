using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VacationsTracker.Core.Data;
using VacationsTracker.Core.Domain;
using VacationsTracker.Core.Exceptions;

namespace VacationsTracker.Core.DataAccess
{
    public class VacationsRepository : IVacationRepository
    {
        private readonly IVacationApi _vacationsApi;
        private readonly IDbService _dbService;

        public VacationsRepository(IVacationApi vacationApi, IDbService dbService)
        {
            _vacationsApi = vacationApi;
            _dbService = dbService;
        }

        public async Task<IEnumerable<Vacation>> GetVacationsAsync(CancellationToken token = default)
        {
            try
            {
                await UpdateOfflineVacations();

                return await GetOfflineVacations();
            }
            catch (InternetConnectionException)
            {
                return await GetOfflineVacations();
            }
        }

        private async Task UpdateOfflineVacations()
        {
            var vacationsDto = await _vacationsApi.GetVacationsAsync();

            var serverVacations = new List<Vacation>();
            foreach (var vacationDto in vacationsDto)
            {
                serverVacations.Add(vacationDto.ToVacation());
            }

            var offlineVacations = new List<Vacation>();
            var dbVacations = await _dbService.GetItems();

            var upToDateOfflineVacations = dbVacations.Where(x => x.Type == OperationType.UpToDate);

            foreach (var offlineVacation in upToDateOfflineVacations)
            {
                offlineVacations.Add(offlineVacation.ToVacation());
            }

            var vacationsToRemove = offlineVacations.Except(serverVacations);

            foreach (var vac in vacationsToRemove)
            {
                await _dbService.RemoveItem(vac.ToOffline());
            }

            foreach (var a in serverVacations)
            {
                var itemToUpdate = await _dbService.GetItem(Guid.Parse(a.Id));

                if (itemToUpdate == null || itemToUpdate.Type == OperationType.UpToDate)
                {
                    await _dbService.InsertOrReplace(a.ToOffline());
                }
            }
        }

        private async Task<IEnumerable<Vacation>> GetOfflineVacations()
        {
            var vacations = await _dbService.GetItems();

            var filteredVacancies = vacations.Where(x => x.Type != OperationType.Delete);

            var vacancyList = new List<Vacation>();
            foreach (var offlineVacation in filteredVacancies)
            {
                vacancyList.Add(offlineVacation.ToVacation());
            }

            return vacancyList;
        }

        public async Task<Vacation> GetVacationAsync(string id, CancellationToken token = default)
        {
            try
            {
                var vacationDto = await _vacationsApi.GetVacationAsync(id);
                return vacationDto.ToVacation();
            }
            catch (InternetConnectionException)
            {
                var vacation = await _dbService.GetItem(Guid.Parse(id));
                return vacation.ToVacation();
            }
        }

        public async Task AddOrUpdateAsync(Vacation vacation, CancellationToken token = default)
        {
            try
            {
                await _vacationsApi.AddOrUpdateAsync(vacation.ToVacationDto());
            }
            catch (InternetConnectionException)
            {
                var item = new OfflineVacation()
                {
                    Id = Guid.Parse(vacation.Id),
                    Start = vacation.Start,
                    End = vacation.End,
                    VacationStatus = vacation.VacationStatus,
                    VacationType = vacation.VacationType,
                    Created = vacation.Created,
                    CreatedBy = vacation.CreatedBy,
                    Type = OperationType.AddOrUpdate
                };

                await _dbService.InsertOrReplace(item);
            }
        }

        public async Task DeleteVacationAsync(string id, CancellationToken token = default)
        {
            try
            {
                await _vacationsApi.DeleteAsync(id);
            }
            catch (InternetConnectionException)
            {
                var vacation = await _dbService.GetItem(Guid.Parse(id));

                vacation.Type = OperationType.Delete;

                await _dbService.InsertOrReplace(vacation);
            }
        }
    }
}
