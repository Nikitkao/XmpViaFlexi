using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace VacationsTracker.Core.Data
{
    public interface IVacationApi
    {
        Task<IEnumerable<VacationDto>> GetVacationsAsync();
    }
}
