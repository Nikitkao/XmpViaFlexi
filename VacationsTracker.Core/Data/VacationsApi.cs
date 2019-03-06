using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VacationsTracker.Core.Data
{
    public class VacationsApi
    {
        public Task<IEnumerable<VacationDto>> GetVacationsAsync()
        {
            return Task.FromResult(Enumerable.Empty<VacationDto>());
        }
    }
}
