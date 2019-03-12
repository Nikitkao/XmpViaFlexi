using System.Collections.Generic;
using System.Threading.Tasks;

namespace VacationsTracker.Core.Data
{
    public interface IVacationApi
    {
        Task<IEnumerable<VacationDto>> GetVacationsAsync();
        Task<VacationDto> GetVacationAsync(string id);
    }
}
