using System.Collections.Generic;
using System.Threading.Tasks;

namespace VacationsTracker.Core.Data
{
    public interface IVacationApi
    {
        Task<IEnumerable<VacationDto>> GetVacationsAsync();

        Task<VacationDto> GetVacationAsync(string id);

        Task AddOrUpdateAsync(VacationDto vacation);

        Task DeleteAsync(string id);
    }
}
