using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VacationsTracker.Core.Data;
using VacationsTracker.Core.Domain;

namespace VacationsTracker.Core.DataAccess
{
    public class VacationsRepository
    {
        public VacationsRepository(VacationsApi vacationsApi)
        {

        }

        public Task<IEnumerable<Vacation>> GetVacationAsync()
        {
            return Task.FromResult(Enumerable.Empty<Vacation>());
        }
    }
}
