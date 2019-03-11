using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VacationsTracker.Core.Data;
using VacationsTracker.Core.Domain;

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
                vacancyList.Add(VacanciesDtoToVacanciesViewModel(vacationDto));
            }

            return vacancyList;
        }

        private Vacation VacanciesDtoToVacanciesViewModel(VacationDto vac)
        {
            return vac.ToVacation();
        }
    }
}
