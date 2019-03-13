using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VacationsTracker.Core.Data;
using VacationsTracker.Core.Domain;
using VacationsTracker.Core.Presentation.ViewModels;

namespace VacationsTracker.Core.DataAccess
{
    public class VacationsRepository : IVacationRepository
    {
        private readonly IVacationApi _vacationsApi;

        public VacationsRepository(IVacationApi vacationApi)
        {
            _vacationsApi = vacationApi;
        }

        public async Task<IEnumerable<Vacation>> GetVacationsAsync(CancellationToken token = default)
        {
            var vacations = await _vacationsApi.GetVacationsAsync();

            var vacancyList = new List<Vacation>();
            foreach (var vacationDto in vacations)
            {
                vacancyList.Add(vacationDto.ToVacation());
            }

            return vacancyList;
        }

        public async Task<Vacation> GetVacationAsync(string id, CancellationToken token = default)
        {
            var vacationDto = await _vacationsApi.GetVacationAsync(id);
            return vacationDto.ToVacation();
        }

        public Task AddOrUpdateAsync(Vacation vacation, CancellationToken token = default)
        {
            return _vacationsApi.AddOrUpdateAsync(vacation.ToVacationDto());
        }

        public Task DeleteVacationAsync(string id, CancellationToken token = default)
        {
            return _vacationsApi.DeleteAsync(id);
        }
    }
}
